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
                        EscribirFichero();
                        
                        break;
                    case Opciones.CONFIGURAR:
                        EscogerConfiguracion();                   
                        break;
                }
            }
        }


        private static void EscribirFichero()
        {
            DocumentsFactory docFact = new WriterDocuments();
            Console.WriteLine("********Crear Usuario********");
            Console.WriteLine("Introduce Id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Introduce el Nombre:");
            String nombre = Console.ReadLine();
            Console.WriteLine("Introduce el apellido:");
            String apellido = Console.ReadLine();
            Console.WriteLine("Introduce el DNI:");
            String dni = Console.ReadLine();
            Alumno nuevoAlumno = new Alumno
            {
                Id = id,
                Nombre = nombre.ToString(),
                Apellido = apellido.ToString(),
                Dni = dni.ToString()
            };

            ConfigurationManager.RefreshSection("appSettings");
            int tipo = Int32.Parse( ConfigurationManager.AppSettings["tipoFichero"]);
            switch ((TipoFichero)tipo)
            {
                case TipoFichero.TXT:
                    docFact.WriteTxtFile(nuevoAlumno);

                    break;
                case TipoFichero.JSON:
                    docFact.WriteJsonFile(nuevoAlumno);
                    break;
            }
                    
        }

        private static void  EscogerConfiguracion(){

            int value = 0;
            string[] acciones = { "1.TXT", "2.JSON" };
            Console.WriteLine("********Guardar en formato:******");
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
                    config.AppSettings.Settings["tipoFichero"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    break;
                case TipoFichero.JSON:
                     value = (int)TipoFichero.JSON;
                    config.AppSettings.Settings["tipoFichero"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    break;
                default:
                    value = (int)TipoFichero.TXT;
                    config.AppSettings.Settings["tipoFichero"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    break;
            }

        }

    }
}

