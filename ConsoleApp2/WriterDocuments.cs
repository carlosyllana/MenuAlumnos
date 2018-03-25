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
        private static String JSONPATH = "alumno.json";
        private static String TXTPATH = "alumno.txt";

        public void WriteJsonFile(Alumno nuevoAlumno)
        {
          
            if (!File.Exists(JSONPATH))
            {

                using (StreamWriter file = File.CreateText(JSONPATH))
                {

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, nuevoAlumno);
                    file.Write("\n");
                }
            }
            else
            {
                using (StreamWriter file = new StreamWriter(JSONPATH, true))
                {

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, nuevoAlumno);
                    file.Write("\n");
                }

            }
        }

        public void WriteTxtFile(Alumno nuevoAlumno)
        {
            if (!File.Exists(TXTPATH))
            {
                using (var tw = File.CreateText(TXTPATH))
                {
                    tw.WriteLine(nuevoAlumno.ToString());
                }
            }
            else
            {
                using (var tw = new StreamWriter(TXTPATH, true))
                {
                    tw.WriteLine(nuevoAlumno.ToString());
                }
            }
        }
    }
}
