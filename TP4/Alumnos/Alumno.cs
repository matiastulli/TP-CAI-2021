using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    class Alumno
    {
        public int NRegistro { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public double Ranking { get; set; }
        public int Password { get; set; }

        public Alumno() { }

        public Alumno(string linea)
        {
            var datos = linea.Split('-');
            NRegistro = int.Parse(datos[0]);
            NombreAlumno = (datos[1]);
            ApellidoAlumno = (datos[2]);
            Ranking = double.Parse(datos[3]);
            Password = int.Parse(datos[4]);
        }

        public static List<Alumno> alumnos = new List<Alumno>();

        static Alumno()
        {
            string fileName = "TP4/TXT/Alumno.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string Alumno = basePath + fileName;

            if (File.Exists(Alumno))
            {
                using (var reader = new StreamReader(Alumno))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var alumno = new Alumno(linea);
                        alumnos.Add(new Alumno()
                        {
                            NRegistro = alumno.NRegistro,
                            NombreAlumno = alumno.NombreAlumno,
                            ApellidoAlumno = alumno.ApellidoAlumno,
                            Ranking = alumno.Ranking,
                            Password = alumno.Password,
                        });
                    }
                }
            }
        }

        
        public void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine("Hola! " + $"{NombreAlumno.Trim()}{ApellidoAlumno}");
            Console.WriteLine();
        }

        //Registro
        public static Alumno SeleccionarAlumno()
        {
            var modelo = CrearModeloBusqueda();
            foreach (var persona in alumnos)
            {
                if (persona.CoincideCon(modelo))
                {
                    return persona;
                }
            }

            Console.WriteLine("No se ha encontrado un alumno que coincida");
            return null;
        }

        public static Alumno CrearModeloBusqueda()
        {
            var modelo = new Alumno();
            modelo.NRegistro = IngresarRegistro(obligatorio: false);
            return modelo;
        }

        public bool CoincideCon(Alumno modelo)
        {
            if (modelo.NRegistro != 0 && modelo.NRegistro != NRegistro)
            {
                return false;
            }
            return true;

        }


        //Password
        public static Alumno VerificarPassword(int Id)
        {
            Alumno Verificar;

            foreach (var id in alumnos)
            {
                if (Id == id.NRegistro)
                {
                    bool Check = false;
                    do
                    {
                        Verificar = Contraseña();

                        if (Verificar.Password == id.Password)
                        {
                            Check = true;
                            return Verificar;
                        }
                        else
                        {
                            Console.WriteLine("Contraseña incorrecta");
                            Check = false;
                        }

                    } while (Check == false);

                }
            }

            Console.WriteLine("Contraseña incorrecta");
            return null;


        }

        public static Alumno Contraseña()
        {
            var modelo = new Alumno();
            modelo.Password = IngresarPassword(obligatorio: true);
            return modelo;
        }
        public bool CoincideConContraseña(Alumno modelo)
        {
            if (modelo.Password != 0 && modelo.Password != Password)
            {
                return false;
            }
            return true;

        }

        //Validaciones e ingresos

        private static int IngresarRegistro(bool obligatorio = true)
        {
            var titulo = "¿Cual es su numero de registro?";

            do
            {
                Console.WriteLine();
                Console.WriteLine("-------------");
                Console.WriteLine("INGRESAR");
                Console.WriteLine("-------------");
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("No ha ingresado un numero de registro válido");
                    continue;
                }

                if (!int.TryParse(ingreso, out var numeroRegistro))
                {
                    Console.WriteLine("No ha ingresado un numero de registro válido");
                    continue;
                }

                return numeroRegistro;

            } while (true);
        }

        private static int IngresarPassword(bool obligatorio = true)
        {
            var titulo = "Contraseña: ";

            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("No ha ingresado una contraseña válida");
                    continue;
                }

                if (!int.TryParse(ingreso, out var Password))
                {
                    Console.WriteLine("No ha ingresado una contraseña válida");
                    continue;
                }

                return Password;

            } while (true);
        }
    }
}
