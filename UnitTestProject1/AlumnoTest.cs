using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp2;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class AlumnoTest
    {

        Alumno alumno = null;
        DocumentsFactory docFact = new DocumentsManager();
        List<Alumno> alumnoList = new List<Alumno>();

        [DataRow(1, "Carlos", "Yllana", "72134")]
        [DataTestMethod]
        public void TxtTest(int id , string nombre, string apellido, string dni) {

            Alumno alumno1 = new Alumno(id, nombre, apellido, dni);
            docFact.WriteTxtFile(alumno1);
            alumnoList = docFact.ReaderTxtFile();
            Assert.IsTrue(alumnoList.Contains(alumno1));

        }

        [DataRow(1, "Carlos", "Yllana", "72134")]
        [DataTestMethod]
        public  void JsonTest(int id, string nombre, string apellido, string dni)
        {

            Alumno alumno1 = new Alumno(id, nombre, apellido, dni);
            docFact.WriteJsonFile(alumno1);
            alumnoList = docFact.ReaderJsonFile();

            Assert.IsTrue(alumnoList.Contains(alumno1));

        }
       

    }
}
