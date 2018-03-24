using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    interface DocumentsFactory
    {
        void writeTxtFile(Alumno nuevoAlumno);
        void writeJsonFile(Alumno nuevoAlumno);
    }
}
