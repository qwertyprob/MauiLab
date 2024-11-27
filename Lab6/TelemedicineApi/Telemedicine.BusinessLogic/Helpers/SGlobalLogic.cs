using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.BusinessLogic.DataBaseModel;

namespace Telemedicine.BusinessLogic.Helpers
{
    static class SGlobalLogic
    {
        public static string HashGen(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password + "pamlab6");
            var encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
        public static string TokenGenerator(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string ConvertImage(string base64Image, string serverpath)
        {
            base64Image = base64Image.Replace(" ", "+");

            byte[] image = Convert.FromBase64String(base64Image);
            if (image.Length > 15000)
            {
                throw new Exception("IMAGE LENGTH EXPECTED");
            }

            string path;
            var filename = $@"images\{DateTime.Now.Ticks}.";
            using (var im = Image.FromStream(new MemoryStream(image)))
            {
                ImageFormat frmt;
                if (ImageFormat.Png.Equals(im.RawFormat))
                {
                    filename += "png";
                    frmt = ImageFormat.Png;
                }
                else
                {
                    filename += "jpg";
                    frmt = ImageFormat.Jpeg;
                }
                path = serverpath + filename;
                im.Save(path, frmt);
            }

            return path;
        }
        public static string GenerateImage(string path)
        {
            using (var image = Image.FromFile(path))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }


        public static bool TokenValidator(string token)
        {
            using (var db = new SessionContext())
            {
                var session = db.Tokens.FirstOrDefault(s => s.TokenString == token);
                return session != null && session.ExpireTime > DateTime.Now;
            }
        }
    }
}
