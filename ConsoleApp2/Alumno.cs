using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Alumno
    {

        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Dni { get; set; }


        public override string ToString()
        {
            return Id.ToString() + ", " + Nombre + ", " + Apellido + ", " + Dni;
        }

    }
}
