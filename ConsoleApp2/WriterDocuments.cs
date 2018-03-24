using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class WriterDocuments : DocumentsFactory
    {
        public void writeJsonFile(Alumno nuevoAlumno)
        {
            string path = "alumno.json";
            if (!File.Exists(path))
            {

                using (StreamWriter file = File.CreateText(path))
                {

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, nuevoAlumno);
                    file.Write("\n");
                }
            }
            else
            {
                using (StreamWriter file = new StreamWriter(path, true))
                {

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, nuevoAlumno);
                    file.Write("\n");
                }

            }
        }

        public void writeTxtFile(Alumno nuevoAlumno)
        {
            string path = "alumnos.txt";
            if (!File.Exists(path))
            {
                using (var tw = File.CreateText(path))
                {
                    tw.WriteLine(nuevoAlumno.salidaInformacion());
                }
            }
            else
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(nuevoAlumno.salidaInformacion());
                }
            }
        }
    }
}
