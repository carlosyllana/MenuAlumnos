using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class DocumentsManager : DocumentsFactory
    {
        private static String JSONPATH = "alumnos.json";
        private static String TXTPATH = "alumnos.txt";
        
        public List<Alumno> ReaderJsonFile()
        {
            List<Alumno> alumnosList = new List<Alumno>(); 
            string json = File.ReadAllText(@JSONPATH);
            if (json.Equals(String.Empty))
            {
                alumnosList = new List<Alumno>();
            }
            else
            {
                alumnosList = JsonConvert.DeserializeObject<List<Alumno>>(json);

            }

            return alumnosList;
        }

        public List<Alumno> ReaderTxtFile()
        {
            string line;
            List<Alumno> alumnosList = new List<Alumno>();
            using (StreamReader file =new StreamReader(@TXTPATH))
            {
                while ((line = file.ReadLine()) != null)
                {
                    String[] alumneString = line.Split(',');
                    Alumno nuevoAlumno = new Alumno(Int32.Parse(alumneString[0]), alumneString[1], alumneString[2], alumneString[3]); ;
                    alumnosList.Add(nuevoAlumno);
                }

                file.Close();
            }
                
           
            return alumnosList;
        }

        public void WriteJsonFile(Alumno nuevoAlumno)
        {
            List<Alumno> alumnosList = null;

            if (!File.Exists(JSONPATH))
            {
                alumnosList = new List<Alumno>();
                alumnosList.Add(nuevoAlumno);
                using (StreamWriter file = File.CreateText(@JSONPATH))
                {
                   

                    JsonSerializer serializer = new JsonSerializer
                    {
                        Formatting = Formatting.None
                    };
                   
                    serializer.Serialize(file, alumnosList);
                }



            }
            else
            {
                alumnosList = ReaderJsonFile();
                using (StreamWriter file = new StreamWriter(@JSONPATH))
                {
                   
                    JsonSerializer serializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented
                    };
                    alumnosList.Add(nuevoAlumno);
                    serializer.Serialize(file, alumnosList);
                }

            }
            
        }

        public void WriteTxtFile(Alumno nuevoAlumno)
        {
            if (!File.Exists(TXTPATH))
            {
                using (var tw = File.CreateText(@TXTPATH))
                {
                    tw.WriteLine(nuevoAlumno.ToString());
                }
            }
            else
            {
                using (var tw = new StreamWriter(@TXTPATH, true))
                {
                    tw.WriteLine(nuevoAlumno.ToString());
                }
            }
        }

        public string GetJsonPath()
        {

            //jsonFile
            string value = Environment.GetEnvironmentVariable("jsonFile", EnvironmentVariableTarget.User);
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + " \\" + value;
;
        }

        public string GetTxtPath()
        {
            //txtFile
            string value;

            value = Environment.GetEnvironmentVariable("txtFile", EnvironmentVariableTarget.User);
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + value;
;
        }
    }
}
