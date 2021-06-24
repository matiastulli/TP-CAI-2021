using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    static class Persona
    {
        private static readonly Dictionary<int, Alumno> entradas;
        const string nombreArchivo = "Alumno.txt";

        static Persona()
        {
            entradas = new Dictionary<int, Alumno>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var alumno = new Alumno(linea);
                        entradas.Add(alumno.NRegistro, alumno);
                    }
                }
            }
        }

        public static Alumno Seleccionar()
        {
            var modelo = Alumno.CrearModeloBusqueda();
            foreach (var persona in entradas.Values)
            {
                if (persona.CoincideCon(modelo))
                {
                    return persona;
                }
            }

            Console.WriteLine("No se ha encontrado un alumno que coincida");
            return null;
        }
    }
}
