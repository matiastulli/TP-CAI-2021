using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class InscripcionesPorAlumno
    {

        public int NRegistro { get; set; }
        public double RankingAlumno { get; set; }
        public int CodigoMateria { get; set; }
        public string NombreMateria { get; set; }
        public int CantidadInscriptos { get; set; }

        public InscripcionesPorAlumno() { }

        public InscripcionesPorAlumno(string linea)
        {
            var datos = linea.Split('-');
            NRegistro = int.Parse(datos[0]);
            RankingAlumno = double.Parse(datos[1]);
            CodigoMateria = int.Parse(datos[2]);
            NombreMateria = (datos[3]);
        }

        public string ObtenerLineaDatosAlumno() => $"{NRegistro}-{CodigoMateria}-{NombreMateria}";

        public static List<InscripcionesPorAlumno> inscripcionesPorAlumno = new List<InscripcionesPorAlumno>();
        public static List<InscripcionesPorAlumno> ValidacionInscripciones = new List<InscripcionesPorAlumno>();

        public static void EscribirInscripcionEnTXT(int numRegistro, int codMateria, string nomMateria, double rankingAlumno)
        {
            string fileName = "TP4/TXT/InscripcionesPorAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string InscripcionesPorAlumnos = basePath + fileName;
            if (File.Exists(InscripcionesPorAlumnos))
            {
                using (StreamWriter sw = File.AppendText(InscripcionesPorAlumnos))
                {
                    sw.WriteLine(numRegistro + "-" + rankingAlumno + "-" + codMateria + "-" + nomMateria);
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado el archivo TXT. El archivo 'InscripcionesPorAlumnos.txt' debe estar en la carpeta Debug");
            }
        }


        public static void AgregarInscripcion(int numRegistro, int codMateria, string nomMateria, double rankingAlumno)
        {
            inscripcionesPorAlumno.Add(new InscripcionesPorAlumno()
            {
                NRegistro = numRegistro,
                CodigoMateria = codMateria,
                NombreMateria = nomMateria,
                RankingAlumno = rankingAlumno,
            });

            EscribirInscripcionEnTXT(numRegistro, codMateria, nomMateria, rankingAlumno);
        }

        public static void MostrarInscripciones(int CodigoPersona)
        {
            var materiasInscriptas = inscripcionesPorAlumno.Where(inscripcionesGeneral => inscripcionesPorAlumno.All(inscripto => inscripto.NRegistro == inscripcionesGeneral.NRegistro));
            Console.WriteLine($"Materias en las que se encuentra inscripto:");
            foreach (var val in materiasInscriptas)
            {
                Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
            }
        }

        public static void LeerInscripciones()
        {
            string nombreArchivo = "TP4/TXT/InscripcionesPorAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string validacionInscripciones = basePath + nombreArchivo;

            if (File.Exists(validacionInscripciones))
            {
                using (var reader = new StreamReader(validacionInscripciones))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var carrera = new InscripcionesPorAlumno(linea);
                        ValidacionInscripciones.Add(new InscripcionesPorAlumno()
                        {
                            NRegistro = carrera.NRegistro,
                            RankingAlumno = carrera.RankingAlumno,
                            CodigoMateria = carrera.CodigoMateria,
                            NombreMateria = carrera.NombreMateria,
                        });
                    }
                }
            }
        }

        public static bool ValidarInscripcionesAlumno(int CodigoPersona)
        {
            LeerInscripciones();
            bool Estado = true;

            foreach (var item in ValidacionInscripciones)
            {
                if (item.NRegistro == CodigoPersona)
                {
                    Console.WriteLine("No puede avanzar con el registro de inscripcion ya que usted se encuentra inscripto en: ");
                    MostrarInscri(CodigoPersona);
                    Estado = false;
                    return Estado;
                }
            }


            return Estado;
        }

        public static void MostrarInscri(int CodigoPersona)
        {
            Console.WriteLine($"Materias en las que se encuentra inscripto:");
            foreach (var val in ValidacionInscripciones)
            {
                if (val.NRegistro == CodigoPersona)
                {
                    Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                }
            }
        }

        public static bool ValidarMateriaInscripta(int CodigoPersona, int CodigoMateria)
        {
            LeerInscripciones();
            bool Estado = true;

            foreach (var item in ValidacionInscripciones)
            {
                if (item.NRegistro == CodigoPersona & item.CodigoMateria == CodigoMateria)
                {
                    MostrarMateriaInscri(CodigoPersona, CodigoMateria);
                    Estado = false;
                    return Estado;
                }
            }
            return Estado;
        }

        public static void MostrarMateriaInscri(int CodigoPersona, int CodigoMateria)
        {
            Console.WriteLine($"Ya se encuentra inscripto en esta materia:");
            foreach (var val in ValidacionInscripciones)
            {
                if (val.NRegistro == CodigoPersona & val.CodigoMateria == CodigoMateria)
                {
                    Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                }
            }
            Console.WriteLine($"\nPor favor seleccione otra");
        }
    }
}
