using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.DocData;

namespace Telemedicine.Domain.Collection
{
    public static class Disease
    {

        public static string FindDocByDisease(string disease)
        {
            var unsplit = disease.Split(' ');
            foreach (var param in unsplit)
            {
                var matchOculist = _oculist
                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(param));
                if (matchOculist != null)
                {
                    return "Oculist";
                }

                var matchPediatru = _pediatru
                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(param));
                if (matchPediatru != null)
                {
                    return "Pediatru";
                }


                var matchChirurg = _chirurg
                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(param));
                if (matchChirurg != null)
                {
                    return "Chirurg";
                }

                var matchTerapeut = _terapeut
                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(param));
                if (matchTerapeut != null)
                {
                    return "Terapeut";
                }
            }

            return "N/A";
        }



        private static List<string> _oculist = new List<string>
        {
            "ochi", "ochiul", "ochelari","ochii", "vederea", "vedere", "vad"
        };

        private static List<string> _pediatru = new List<string>
        {
            "copilul", "copil", "baiatul", "fetita"
        };

        private static List<string> _chirurg = new List<string>
        {
            "operatie", "operat", "interventie", "chirurgical", "chirurgicala"
        };

        private static List<string> _terapeut = new List<string>
        {
            "picior", "mina", "coloana", "vertebrala", "muschi", "ligamente", "fractura", "fracturat","spinare", "vertebre", "dislocat", "dislocate"
        };
    }
}
