using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Administracion
    {
        public static List<MateriasBase> administracion = new List<MateriasBase>();

        static Administracion()
        {
            string fileName = "TP4/TXT/MateriasPorCarrera/Administracion.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");



            string Administracion = basePath + fileName;

            if (File.Exists(Administracion))
            {
                using (var reader = new StreamReader(Administracion))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var carrera = new MateriasBase(linea);
                        administracion.Add(new MateriasBase()
                        {
                            CodigoMateria = carrera.CodigoMateria,
                            NombreMateria = carrera.NombreMateria,
                            ProfesorMateria = carrera.ProfesorMateria,
                            HorarioMateria = carrera.HorarioMateria,
                            CapacidadMateria = carrera.CapacidadMateria,
                            CorteDeRankingMateria = carrera.CorteDeRankingMateria,
                            Correlativa1 = carrera.Correlativa1,
                            Correlativa2 = carrera.Correlativa2,
                            Correlativa3 = carrera.Correlativa3,
                            Correlativa4 = carrera.Correlativa4,
                        });
                    }
                }
            }       
        }

        
        public static void MostrarTXT()
        {
            string Mensaje = "";
            foreach (var materias in administracion)
            {
                Mensaje += $"\nCodigo Materia: {materias.CodigoMateria}" + " - " + $"Nombre: {materias.NombreMateria}\n" + $"Profesor:{materias.ProfesorMateria}" + " - " + $"Horario:{materias.HorarioMateria}\n";
            }
            if (Mensaje != "")
            {
                Console.WriteLine(System.Environment.NewLine + Mensaje);
            }
            if (Mensaje == "")
            {
                Console.WriteLine("No hay materias");
            }

        }

        public static MateriasBase Seleccionar()
        {
            var modelo = MateriasBase.CrearModeloBusqueda();
            foreach (var persona in administracion)
            {
                if (persona.CoincideCon(modelo))
                {
                    return persona;
                }
            }

            Console.WriteLine("No se ha encontrado una materia que coincida");
            return null;
        }

        public static MateriasBase SeleccionarAsignacion(int CantidadMax)
        {
            var modelo = MateriasBase.CrearModeloBusquedaAsignacion(CantidadMax);
            foreach (var persona in administracion)
            {
                if (persona.CoincideCon(modelo))
                {
                    return persona;
                }
            }

            Console.WriteLine("No se ha encontrado una materia que coincida");
            return null;
        }
    }
}
