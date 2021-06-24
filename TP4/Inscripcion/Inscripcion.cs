using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    static class Inscripcion
    {
        private static readonly Dictionary<int, Asignacion> entradas;
        const string nombreArchivo = "Inscripcion.txt";

        static Inscripcion()
        {
            entradas = new Dictionary<int, Asignacion>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var asignacion = new Asignacion(linea);
                        entradas.Add(asignacion.NRegistro, asignacion);
                    }
                }
            }
        }

        public static void Agregar(Asignacion asignacion)
        {
            entradas.Add(asignacion.NRegistro, asignacion);
            Grabar();
        }

        public static void MostrarDatos()
        {
            string Mensaje = "";
            foreach (var materias in entradas.Values)
            {
                Mensaje += "Usted a sido asignado a: \n" + " - " + $"{materias.NombreMateria1}\n" + " - " + $"{materias.NombreMateria2}\n" + " - " + $"{ materias.NombreMateria3}\n";
            }
            if (Mensaje != "")
            {
                Console.WriteLine(System.Environment.NewLine + Mensaje);
            }
            if (Mensaje == "")
            {
                Console.WriteLine("No ha sido asignado");
            }

        }



        public static void Grabar()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var asignacion in entradas.Values)
                {
                    var linea = asignacion.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }
    }
}
