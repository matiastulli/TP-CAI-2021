using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class MateriasBase
    {
        public int CodigoMateria { get; set; }
        public string NombreMateria { get; set; }
        public string ProfesorMateria { get; set; }
        public string HorarioMateria {get; set; }
        public int CapacidadMateria {get; set; }
        public double CorteDeRankingMateria {get; set; }
        public int Correlativa1 { get; set; }
        public int Correlativa2 { get; set; }
        public int Correlativa3 { get; set; }
        public int Correlativa4 { get; set; }




        public MateriasBase() { }

        public MateriasBase(string linea)
        {
            var datos = linea.Split('-');
            CodigoMateria = int.Parse(datos[0]);
            NombreMateria = (datos[1]);
            ProfesorMateria = (datos[2]);
            HorarioMateria = (datos[3]);
            CapacidadMateria = int.Parse(datos[4]);
            CorteDeRankingMateria = double.Parse(datos[5]);
            Correlativa1 = int.Parse(datos[6]);
            Correlativa2 = int.Parse(datos[7]);
            Correlativa3 = int.Parse(datos[8]);
            Correlativa4 = int.Parse(datos[9]);
        }

        public void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine($"Codigo de Materia: {CodigoMateria}" 
                + " " + $"Nombre: {NombreMateria}" 
                + " " + $"Horario: {HorarioMateria}" 
                + " " + $"Profesor: {ProfesorMateria}" 
                + " " + $"{Correlativa1}"
                + " " + $"{Correlativa2}"
                + " " + $"{Correlativa3}"
                + " " + $"{Correlativa4}");
            Console.WriteLine();
        }

        public static List<MateriasBase> materiasDisponibles = new List<MateriasBase>();

        public static MateriasBase CrearModeloBusqueda()
        {
            var modelo = new MateriasBase();
            modelo.CodigoMateria = IngresarCodigoMateria();
            return modelo;
        }

        public static MateriasBase CrearModeloBusquedaAsignacion(int CantidadMax)
        {
            var modelo = new MateriasBase();
            modelo.CodigoMateria = IngresarCodigoMateriaAsignacion(CantidadMax);
            return modelo;
        }

        public bool CoincideCon(MateriasBase modelo)
        {
            if (modelo.CodigoMateria != 0 && modelo.CodigoMateria != CodigoMateria)
            {
                return false;
            }

            return true;

        }


        //Validaciones e ingresos

        private static int IngresarCodigoMateriaAsignacion(int CantidadMax)
        {
            var titulo = "Ingrese el codigo de materia a inscribirse numero " + CantidadMax + "/" + Program.CantidadMaxRepetida;

            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("No ha ingresado un codigo de materia válido");
                    continue;
                }

                if (int.TryParse(ingreso, out var codigomateria) == false)
                {
                    Console.WriteLine("No ha ingresado un codigo de materia válido");
                    continue;
                }

                return codigomateria;

            } while (true);
        }

        private static int IngresarCodigoMateria()
        {
            var titulo = "Ingrese el codigo de materia";

            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("No ha ingresado un codigo de materia válido");
                    continue;
                }

                if (int.TryParse(ingreso, out var codigomateria) == false)
                {
                    Console.WriteLine("No ha ingresado un codigo de materia válido");
                    continue;
                }

                return codigomateria;

            } while (true);
        }


    }
}
