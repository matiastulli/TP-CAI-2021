using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Asignacion
    {
        public static List<InscripcionesPorAlumno> inscripcionesAsignacion = new List<InscripcionesPorAlumno>();
        public static List<InscripcionesPorAlumno> inscriptosPorMateria = new List<InscripcionesPorAlumno>();
        public static List<InscripcionesPorAlumno> cortePorRanking = new List<InscripcionesPorAlumno>();
        public static List<InscripcionesPorAlumno> cortePorRegistro = new List<InscripcionesPorAlumno>();
        public static List<InscripcionesPorAlumno> asignaciones = new List<InscripcionesPorAlumno>();
        public static List<Asignacion> materiasFCE = new List<Asignacion>();

        public int CodigoMateria { get; set; }
        public string NombreMateria { get; set; }
        public int CapacidadMateria { get; set; }
        public string HorarioMateria { get; set; }
        public Asignacion() { }

        public Asignacion(string linea)
        {
            var datos = linea.Split('-');
            CodigoMateria = int.Parse(datos[0]);
            NombreMateria = (datos[1]);
            HorarioMateria = (datos[2]);
            CapacidadMateria = int.Parse(datos[3]);        
        }
        public static void LeerMateriasFCE()
        {
            string fileName = "TP4/TXT/MateriasFCE.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string MateriasFCE = basePath + fileName ;

            if (File.Exists(MateriasFCE))
            {
                using (var reader = new StreamReader(MateriasFCE))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var carrera = new Asignacion(linea);
                        materiasFCE.Add(new Asignacion()
                        {
                            CodigoMateria = carrera.CodigoMateria,
                            NombreMateria = carrera.NombreMateria,
                            CapacidadMateria = carrera.CapacidadMateria,
                        });
                    }
                }
            }
        }

        public static void LeerInscripciones()
        {
            string nombreArchivo = "TP4/TXT/InscripcionesPorAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string InscripcionesPorAlumnos = basePath + nombreArchivo;

            if (File.Exists(InscripcionesPorAlumnos))
            {
                using (var reader = new StreamReader(InscripcionesPorAlumnos))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var carrera = new InscripcionesPorAlumno(linea);
                        inscripcionesAsignacion.Add(new InscripcionesPorAlumno()
                        {
                            NRegistro = carrera.NRegistro,
                            CodigoMateria = carrera.CodigoMateria,
                            NombreMateria = carrera.NombreMateria,
                            RankingAlumno = carrera.RankingAlumno,
                        });
                    }
                }
            }
        }

        public static void LeerAsignaciones()
        {
            string nombreArchivo = "TP4/TXT/AsignacionesPorAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string AsignacionesPorAlumnos = basePath + nombreArchivo;

            if (File.Exists(AsignacionesPorAlumnos))
            {
                using (var reader = new StreamReader(AsignacionesPorAlumnos))
                {
                    asignaciones.Clear();
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var carrera = new InscripcionesPorAlumno(linea);

                        asignaciones.Add(new InscripcionesPorAlumno()
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

        public static void SumatoriaCantidadInscriptos()
        {
            foreach (var val in Asignacion.materiasFCE)
            {
                int contadorMateria = 0;
                foreach (var val2 in inscripcionesAsignacion)
                {
                    if(val.CodigoMateria == val2.CodigoMateria)
                    {
                        contadorMateria++;
                    }
                }
                inscriptosPorMateria.Add(new InscripcionesPorAlumno()
                {
                    CodigoMateria = val.CodigoMateria,
                    NombreMateria = val.NombreMateria,
                    CantidadInscriptos = contadorMateria,
                });
            }
        }

        public static void CorteDeRanking()
        {
            asignaciones.Clear();
            foreach (var val in Asignacion.materiasFCE)
            {
                foreach (var val2 in Asignacion.inscriptosPorMateria)
                {
                    if (val.CodigoMateria == val2.CodigoMateria)
                    {
                        if (val2.CantidadInscriptos != 0)
                        {
                            if (val.CapacidadMateria < val2.CantidadInscriptos)
                            {
                                int capacidadRanking = (int)(val.CapacidadMateria * 0.70);
                                int capacidadRegistro = (int)(val.CapacidadMateria * 0.30);
                                cortePorRanking = inscripcionesAsignacion.OrderByDescending(o => o.RankingAlumno).ToList();
                                cortePorRegistro = inscripcionesAsignacion.OrderByDescending(o => o.RankingAlumno).ToList();

                                for (int i = 0; i < capacidadRanking; i++)
                                {
                                    asignaciones.Add(cortePorRanking[i]);
                                }
                                cortePorRegistro = Asignacion.inscripcionesAsignacion.Where(inscri => asignaciones.All(asig => asig.NRegistro != inscri.NRegistro)).ToList();
                                cortePorRegistro = cortePorRegistro.OrderBy(o => o.NRegistro).ToList();
                                for (int i = 0; i < capacidadRegistro; i++)
                                {
                                    asignaciones.Add(cortePorRegistro[i]);
                                }
                            }

                            else
                            {
                                foreach (var val4 in inscripcionesAsignacion)
                                {
                                    if(val4.CodigoMateria == val.CodigoMateria)
                                    {
                                        asignaciones.Add(new InscripcionesPorAlumno()
                                        {
                                            NRegistro = val4.NRegistro,
                                            CodigoMateria = val4.CodigoMateria,
                                            NombreMateria = val4.NombreMateria,
                                        });
                                    }
                                }

                            }
                        }

                    }
                }

            }
        }
            
        
        public static void EscribirAsignacionEnTXT()
        {
            string fileName = "TP4/TXT/AsignacionesPorAlumnos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string AsignacionesPorAlumnos = basePath + fileName;

            if (File.Exists(AsignacionesPorAlumnos))
            {
                using (StreamWriter sw = File.AppendText(AsignacionesPorAlumnos))
                {
                    foreach( var val in asignaciones)
                    {
                        sw.WriteLine(val.NRegistro + "-" + val.RankingAlumno + "-" + val.CodigoMateria + "-" + val.NombreMateria);
                    }

                }
            }
        }

        public static void MostrarAsignaciones(int Nregistro)
        {
            Asignacion.LeerAsignaciones();
            Console.WriteLine("Tenes las siguientes materias asignadas:");
            int contador = 0;
            foreach (var val in asignaciones)
            {
                if (val.NRegistro == Nregistro)
                {
                    Console.WriteLine("Numero de registro:" + val.NRegistro + " | Codigo de materia:" + val.CodigoMateria + " | Nombre de materia:" + val.NombreMateria);
                    contador++;
                }
            }
            if (contador == 0)
            {
                Console.WriteLine("Aun no tenes materias asignadas. Debe esperar a que la secretaria asigne las materias");
            }

        }
    }
}
