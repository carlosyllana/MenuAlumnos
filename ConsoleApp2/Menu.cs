using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        enum Day { TXT=1, JSON=2};
        static void Main(string[] args)
        {
            string[] acciones = { "0.Salir", "1.Crear Usuario", "2.Configurar" };
            int identificador;
          
            int opc = -1;
            String tipoFichero = ConfigurationManager.AppSettings["tipoFichero"];
            while (opc != 0)
            {
                Console.WriteLine("\\\\\\\\MENU\\\\\\\\");
                for (int i = 0; i < acciones.Length; i++)
                {
                    Console.WriteLine(acciones[i].ToString());

                }
                Console.WriteLine("--------------------");
                Console.WriteLine("Escoge opcion:");
                opc = Int32.Parse(Console.ReadLine());
                switch (opc)
                {


                    case 1:
                        escribirFichero(1);

                        break;
                    case 2:
                        escogerConfiguracion();
                       
                        break;
                }
            }


        }


        private static void escribirFichero(int metodo)
        {

            //Console.WriteLine("********Crear Usuario******");
            //Console.WriteLine("Introduce Id:");
            //int id = Int32.Parse(Console.ReadLine());
            //Console.WriteLine("Introduce el Nombre:");
            //String nombre = Console.ReadLine();
            //Console.WriteLine("Introduce el apellido");
            //String apellido = Console.ReadLine();
            //Console.WriteLine("Introduce el dni");
            //String dni = Console.ReadLine();
            //Alumno nuevoAlumno = new Alumno(id, nombre, apellido, dni);
            //Alumno nuevoAlumno = new Alumno(1, "nombre", "apellido", "dni");
            Alumno nuevoAlumno = new Alumno
            {
                Id = 1,
                Nombre = "carlos",
                Apellido = "yllana",
                Dni = "yyyyy"
            };

            escribirJSON(nuevoAlumno);
        }

        private static int  escogerConfiguracion(){
            string[] acciones = { "1.TXT", "2.JSON" };
            Console.WriteLine("********En que formato quieres serializar el alumno?******");
            for (int i = 0; i < acciones.Length; i++)
            {
                Console.WriteLine(acciones[i].ToString());

            }
            Console.WriteLine("--------------------");
            Console.WriteLine("Escoge opcion:");
            int tipo = Int32.Parse(Console.ReadLine());
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            switch (tipo)
            {
                case 1:
                   
                    config.AppSettings.Settings["miParametro"].Value = "TXT";
                    config.Save(ConfigurationSaveMode.Modified);
                    return 1;
                    break;
                case 2:
                    config.AppSettings.Settings["tipoFichero"].Value = "JSON";
                    config.Save(ConfigurationSaveMode.Modified);
                    return 2;
                    break;
                default:
                    config.AppSettings.Settings["tipoFichero"].Value = "TXT";
                    config.Save(ConfigurationSaveMode.Modified);
                    return 1;
                    break;
            }

        }

        private static void escribirJSON(Alumno miAlumno)
        {


            /*JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("alumno.json", true ))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, miAlumno);
               // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }*/

            // serialize JSON to a string and then write string to a file


            // serialize JSON directly to a file
            Console.WriteLine(miAlumno.salidaInformacion());
            // serialize JSON directly to a file

            //using (StreamWriter file = File.CreateText("alumno.json"))
            using (StreamWriter file = new StreamWriter("alumno.json", true))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, miAlumno);
            }
        }
        
        private  static void escribirTxt(Alumno nuevoAlumno)
        {
            string path = "alumnos.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(nuevoAlumno.salidaInformacion());
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(nuevoAlumno.salidaInformacion());
                }
            }
        }


    }
}

