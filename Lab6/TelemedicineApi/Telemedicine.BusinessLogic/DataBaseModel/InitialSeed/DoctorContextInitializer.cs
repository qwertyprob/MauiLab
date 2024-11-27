using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.DocData;

namespace Telemedicine.BusinessLogic.DataBaseModel.InitialSeed
{
    public class DoctorContextInitializer : DropCreateDatabaseIfModelChanges<DoctorContext>
    {
        protected override void Seed(DoctorContext context)
        {
            List<DocProfile> doc = new List<DocProfile>
            {
                new DocProfile { FullName = "Arseni Andrei",Specs = "Oculist", Address = "str. Vasile Alecsandri 45A", About = "Medic Oculist cu experienta de 12 ani in domeniu.", Stars = 4.2, IsActive = true, Photo = "images\\doc\\arseni.png"},
                new DocProfile { FullName = "Stratila Alina",Specs = "Pediatru", Address = "str. Mihai Eminescu 12/3", About = "Medic Pediatru cu experienta de 7 ani in domeniu.", Stars = 4.8, IsActive = true, Photo = "images\\doc\\stratila.png"},
                new DocProfile { FullName = "Baltag Victoria",Specs = "Chirurg", Address = "str. Columna 25/1", About = "Medic Chirurg cu experienta de 19 ani in domeniu.", Stars = 5.00, IsActive = true, Photo = "images\\doc\\baltag.png"},
                new DocProfile { FullName = "Rusu Mihail",Specs = "Terapeut", Address = "str. Grenoble 18/6 ", About = "Medic Terapeut cu experienta de 6 ani in domeniu.", Stars = 3.8, IsActive = true, Photo = "images\\doc\\rusu.png"},
                new DocProfile { FullName = "Spatari Ion",Specs = "Oculist", Address = "str. Vasile Alecsandri 45A", About = "Medic Oculist cu experienta de 8 ani in domeniu.", Stars = 4.2, IsActive = true, Photo = "images\\doc\\spatari.png"},
                new DocProfile { FullName = "Micheuta Vasile",Specs = "Chirurg", Address = "str. Columna 25/1", About = "Medic Chirurg cu experienta de 12 ani in domeniu.", Stars = 4.1, IsActive = true, Photo = "images\\doc\\micheuta.png"},
                new DocProfile { FullName = "Gomoja Ana",Specs = "Terapeut", Address = "str. Grenoble 18/6 ", About = "Medic Terapeut cu experienta de 3 ani in domeniu.", Stars = 2.5, IsActive = true, Photo = "images\\doc\\gomoja.png"}
            };

            context.Docs.AddRange(doc);
            context.SaveChanges();
        }
    }
}
