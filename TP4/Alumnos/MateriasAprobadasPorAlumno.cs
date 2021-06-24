using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class MateriasAprobadasPorAlumno
    {
        public int NRegistro { get; set; }
        public int CodigoMateria { get; set; }
        public string NombreMateria { get; set; }

        public MateriasAprobadasPorAlumno() { }

        public MateriasAprobadasPorAlumno(string linea)
        {
            var datos = linea.Split('-');
            NRegistro = int.Parse(datos[0]);
            CodigoMateria = int.Parse(datos[1]);
            NombreMateria = (datos[2]);
        }

        public string ObtenerLineaDatosAlumno() => $"{NRegistro}-{CodigoMateria}-{NombreMateria}";

        public static List<MateriasAprobadasPorAlumno> materiasAprobadas = new List<MateriasAprobadasPorAlumno>();
        public static List<MateriasAprobadasPorAlumno> materiasDisponibles = new List<MateriasAprobadasPorAlumno>();
        public static List<MateriasAprobadasPorAlumno> ValidacionMateriasAprobadas = new List<MateriasAprobadasPorAlumno>();

        public static void EscribirAprobadasEnTXT()
        {
            string fileName = "TP4/TXT/MateriasAprobadasAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");

            string MateriasAprobadasAlumnos = basePath + fileName;
            if (File.Exists(MateriasAprobadasAlumnos))
            {
                using (TextWriter tw = new StreamWriter(MateriasAprobadasAlumnos))
                {
                    foreach (var materiaAprobada in materiasAprobadas)
                    {
                        tw.WriteLine(materiaAprobada.NRegistro + " - " + materiaAprobada.CodigoMateria + " - " + materiaAprobada.NombreMateria);
                    }

                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado el archivo TXT");
            }
        }
        public static void AgregarMateria(int numRegistro, int codMateria, string nomMateria)
        {
            materiasAprobadas.Add(new MateriasAprobadasPorAlumno()
            {
                NRegistro = numRegistro,
                CodigoMateria = codMateria,
                NombreMateria = nomMateria,
            });
        }

        public static void AgregarMateriaDisponible(int numRegistro, int codMateria, string nomMateria)
        {
            materiasDisponibles.Add(new MateriasAprobadasPorAlumno()
            {
                NRegistro = numRegistro,
                CodigoMateria = codMateria,
                NombreMateria = nomMateria,
            });
        }


        public static void MostrarMateriasDisponibles(string eleccionCarrera, int numRegistro)
        {
            switch (eleccionCarrera)
            {
                case "1":
                    var materiasDisponiblesEcon = Economia.economia.Where(econ => MateriasAprobadasPorAlumno.materiasAprobadas.All(aprob => aprob.CodigoMateria != econ.CodigoMateria));
                    Console.WriteLine($"Materias disponibles para inscripcion:");
                    foreach (var val in materiasDisponiblesEcon)
                    {
                        //Si no tiene correlativas, la agrego como disponible
                        if (val.Correlativa1 == 0 && val.Correlativa2 == 0 && val.Correlativa3 == 0 && val.Correlativa4 == 0)
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }

                        else if (materiasDisponiblesEcon.All(n => (n.CodigoMateria != val.Correlativa1) && ((n.CodigoMateria != val.Correlativa2) || val.Correlativa2 == 0) && ((n.CodigoMateria != val.Correlativa3) || val.Correlativa3 == 0) && ((n.CodigoMateria != val.Correlativa4) || val.Correlativa4 == 0)))
                        {
                                Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                                AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);                            
                        }                  
                    }
                    break;

                case "2":
                    var materiasDisponiblesSist = Sistemas.sistemas.Where(sist => MateriasAprobadasPorAlumno.materiasAprobadas.All(aprob => aprob.CodigoMateria != sist.CodigoMateria));
                    Console.WriteLine($"Materias disponibles para inscripcion:");
                    foreach (var val in materiasDisponiblesSist)
                    {
                        if (val.Correlativa1 == 0 && val.Correlativa2 == 0 && val.Correlativa3 == 0 && val.Correlativa4 == 0)
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }

                        else if (materiasDisponiblesSist.All(n => (n.CodigoMateria != val.Correlativa1) && ((n.CodigoMateria != val.Correlativa2) || val.Correlativa2 == 0) && ((n.CodigoMateria != val.Correlativa3) || val.Correlativa3 == 0) && ((n.CodigoMateria != val.Correlativa4) || val.Correlativa4 == 0)))
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }
                    }
                    break;

                case "3":
                    var materiasDisponiblesCont = Contador.contador.Where(cont => MateriasAprobadasPorAlumno.materiasAprobadas.All(aprob => aprob.CodigoMateria != cont.CodigoMateria));
                    Console.WriteLine($"Materias disponibles para inscripcion:");
                    foreach (var val in materiasDisponiblesCont)
                    {
                        if (val.Correlativa1 == 0 && val.Correlativa2 == 0 && val.Correlativa3 == 0 && val.Correlativa4 == 0)
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }

                        else if (materiasDisponiblesCont.All(n => (n.CodigoMateria != val.Correlativa1) && ((n.CodigoMateria != val.Correlativa2) || val.Correlativa2 == 0) && ((n.CodigoMateria != val.Correlativa3) || val.Correlativa3 == 0) && ((n.CodigoMateria != val.Correlativa4) || val.Correlativa4 == 0)))
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }
                    }
                    break;

                case "4":
                    var materiasDisponiblesActAdm = ActuarioAdministracion.actuarioAdministracion.Where(actAdm => MateriasAprobadasPorAlumno.materiasAprobadas.All(aprob => aprob.CodigoMateria != actAdm.CodigoMateria));
                    Console.WriteLine($"Materias disponibles para inscripcion:");
                    foreach (var val in materiasDisponiblesActAdm)
                    {
                        if (val.Correlativa1 == 0 && val.Correlativa2 == 0 && val.Correlativa3 == 0 && val.Correlativa4 == 0)
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }

                        else if (materiasDisponiblesActAdm.All(n => (n.CodigoMateria != val.Correlativa1) && ((n.CodigoMateria != val.Correlativa2) || val.Correlativa2 == 0) && ((n.CodigoMateria != val.Correlativa3) || val.Correlativa3 == 0) && ((n.CodigoMateria != val.Correlativa4) || val.Correlativa4 == 0)))
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }
                    }
                    break;

                case "5":
                    var materiasDisponiblesActEcon = ActuarioEconomia.actuarioEconomia.Where(actEcon => MateriasAprobadasPorAlumno.materiasAprobadas.All(aprob => aprob.CodigoMateria != actEcon.CodigoMateria));
                    Console.WriteLine($"Materias disponibles para inscripcion:");
                    foreach (var val in materiasDisponiblesActEcon)
                    {
                        if (val.Correlativa1 == 0 && val.Correlativa2 == 0 && val.Correlativa3 == 0 && val.Correlativa4 == 0)
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }

                        else if (materiasDisponiblesActEcon.All(n => (n.CodigoMateria != val.Correlativa1) && ((n.CodigoMateria != val.Correlativa2) || val.Correlativa2 == 0) && ((n.CodigoMateria != val.Correlativa3) || val.Correlativa3 == 0) && ((n.CodigoMateria != val.Correlativa4) || val.Correlativa4 == 0)))
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }
                    }
                    break;

                case "6":
                    var materiasDisponiblesAdmin = ActuarioEconomia.actuarioEconomia.Where(admin => MateriasAprobadasPorAlumno.materiasAprobadas.All(aprob => aprob.CodigoMateria != admin.CodigoMateria));
                    Console.WriteLine($"Materias disponibles para inscripcion:");
                    foreach (var val in materiasDisponiblesAdmin)
                    {
                        if (val.Correlativa1 == 0 && val.Correlativa2 == 0 && val.Correlativa3 == 0 && val.Correlativa4 == 0)
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }

                        else if (materiasDisponiblesAdmin.All(n => (n.CodigoMateria != val.Correlativa1) && ((n.CodigoMateria != val.Correlativa2) || val.Correlativa2 == 0) && ((n.CodigoMateria != val.Correlativa3) || val.Correlativa3 == 0) && ((n.CodigoMateria != val.Correlativa4) || val.Correlativa4 == 0)))
                        {
                            Console.WriteLine($"Codigo de materia: " + val.CodigoMateria + $" | Nombre de materia: " + val.NombreMateria);
                            AgregarMateriaDisponible(numRegistro, val.CodigoMateria, val.NombreMateria);
                        }
                    }
                    break;

            }
        }

        public static int contarMateriasAprobadas(int numRegistro)
        {
            int intMateriasAprob = 0;
            foreach (var val in materiasAprobadas)
            {
                if(numRegistro == val.NRegistro)
                {
                    intMateriasAprob++;
                }
            }
            return intMateriasAprob;
        }

        public static int contarMateriasDisp(int numRegistro)
        {
            int intMateriasDispon = 0;
            foreach (var val in materiasDisponibles)
            {
                if (numRegistro == val.NRegistro)
                {
                    intMateriasDispon++;
                }
            }
            return intMateriasDispon;
        }



        public static MateriasAprobadasPorAlumno CrearModeloBusquedaAlumno(int CodigoPersona)
        {
            var modelo = new MateriasAprobadasPorAlumno();
            modelo.CodigoMateria = CodigoPersona;
            return modelo;
        }

        public bool CoincideConAlumno(MateriasAprobadasPorAlumno modelo)
        {
            if (modelo.NRegistro != 0 && modelo.NRegistro != NRegistro)
            {
                return false;
            }

            return true;

        }

        public static void LeerAprobadas()
        {
            string nombreArchivo = "TP4/TXT/MateriasAprobadasAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string validacionAprobadas = basePath + nombreArchivo;

            if (File.Exists(validacionAprobadas))
            {
                using (var reader = new StreamReader(validacionAprobadas))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var carrera = new MateriasAprobadasPorAlumno(linea);
                        ValidacionMateriasAprobadas.Add(new MateriasAprobadasPorAlumno()
                        {
                            NRegistro = carrera.NRegistro,
                            CodigoMateria = carrera.CodigoMateria,
                            NombreMateria = carrera.NombreMateria,
                        });
                    }
                }
            }
        }

        public static bool ValidarMateriaAprobada(int CodigoPersona, int CodigoMateria)
        {
            LeerAprobadas();
            bool Estado = true;

            foreach (var item in ValidacionMateriasAprobadas)
            {
                if (item.NRegistro == CodigoPersona & item.CodigoMateria == CodigoMateria)
                {
                    MostrarMateriaApro(CodigoPersona, CodigoMateria);
                    Estado = false;
                    return Estado;
                }
            }
            return Estado;
        }

        public static void MostrarMateriaApro(int CodigoPersona, int CodigoMateria)
        {
            Console.WriteLine($"Ya marco como aprobada esta materia:");
            foreach (var val in ValidacionMateriasAprobadas)
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
