using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    interface DocumentsFactory
    {
        void WriteTxtFile(Alumno nuevoAlumno);
        void WriteJsonFile(Alumno nuevoAlumno);
        List<Alumno> ReaderTxtFile();
        List<Alumno> ReaderJsonFile();
    }
}
