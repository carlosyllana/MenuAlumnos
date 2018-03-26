using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.Enums;

namespace ConsoleApp2
{
    class Menu
    {


        public void IniciarMenu()
        {
            string[] acciones = { "0.Salir", "1.Crear Usuario", "2.Configurar" };

            int opc = -1;
            DocumentsFactory docFact = new DocumentsManager();



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
                switch ((Opciones)opc)
                {
                    case Opciones.SALIR:
                        Console.WriteLine("Hasta pronto");
                        Console.ReadKey();
                        break;
                    case Opciones.CREAR:
                        CrearUsuario();

                        break;
                    case Opciones.CONFIGURAR:
                        EscogerConfiguracion();
                        break;
                }
            }


        }


        private static void CrearUsuario()
        {
            DocumentsFactory docFact = new DocumentsManager();
            Console.WriteLine("********Crear Usuario********");
            Console.WriteLine("Introduce Id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Introduce el Nombre:");
            String nombre = Console.ReadLine();
            Console.WriteLine("Introduce el apellido:");
            String apellido = Console.ReadLine();
            Console.WriteLine("Introduce el DNI:");
            String dni = Console.ReadLine();
            Alumno nuevoAlumno = new Alumno(id, nombre.ToString(), apellido.ToString(), dni.ToString());

            ConfigurationManager.RefreshSection("appSettings");
            int tipo = Int32.Parse(ConfigurationManager.AppSettings["tipoFichero"]);
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

        private static void EscogerConfiguracion()
        {

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
            switch ((TipoFichero)tipo)
            {
                case TipoFichero.TXT:
                    value = (int)TipoFichero.TXT;
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