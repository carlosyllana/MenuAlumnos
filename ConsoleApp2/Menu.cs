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

        enum Opciones {SALIR, CREAR, CONFIGURAR};
        enum TipoFichero { TXT=1, JSON=2};
        static void Main(string[] args)
        {
            string[] acciones = { "0.Salir", "1.Crear Usuario", "2.Configurar" };          
         
            int opc = -1;

            while (opc != 0)
            {
                Console.WriteLine("\\\\\\\\MENU\\\\\\\\");
                for (int i = 0; i < acciones.Length; i++)
                {
                    Console.WriteLine(acciones[i].ToString());

                }
                Console.WriteLine("--------------------");
                Console.Write("Escoge opcion: ");
                opc = Int32.Parse(Console.ReadLine());
                switch ((Opciones) opc)
                {
                    case Opciones.SALIR:
                        Console.WriteLine("Hasta pronto");
                        Console.ReadKey();
                        break;
                    case Opciones.CREAR:
                        escribirFichero();
                        
                        break;
                    case Opciones.CONFIGURAR:
                        escogerConfiguracion();                   
                        break;
                }
            }
        }


        private static void escribirFichero()
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


            int tipo = Int32.Parse( ConfigurationManager.AppSettings["tipoFichero"]);
            switch ((TipoFichero)tipo)
            {
                case TipoFichero.TXT:
                    escribirTxt(nuevoAlumno);

                    break;
                case TipoFichero.JSON:
                    escribirJSON(nuevoAlumno);
                    break;
            }
                    
        }

        private static void  escogerConfiguracion(){

            int value = 0;
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
            switch ((TipoFichero) tipo)
            {
                case TipoFichero.TXT:
                    value =(int) TipoFichero.TXT;
                    config.AppSettings.Settings["miParametro"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    break;
                case TipoFichero.JSON:
                     value = (int)TipoFichero.JSON;
                    config.AppSettings.Settings["tipoFichero"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    break;
                default:
                    value = (int)TipoFichero.TXT;
                    config.AppSettings.Settings["miParametro"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    break;
            }

        }

        private static void escribirJSON(Alumno nuevoAlumno)
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
        
        private  static void escribirTxt(Alumno nuevoAlumno)
        {
            string path = "alumnos.txt";
            if (!File.Exists(path))
            {
                using (var tw = File.CreateText(path))
                {
                    tw.WriteLine(nuevoAlumno.salidaInformacion());
                }
            }
            else {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(nuevoAlumno.salidaInformacion());
                }
            }



        }


    }
}

