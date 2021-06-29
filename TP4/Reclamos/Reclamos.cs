using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    class Reclamos
    {

        public static List<Reclamos> AllreclamosAlumnos = new List<Reclamos>();
        public static int variableNReclamo = 0;

        public int NReclamo { get; set; }
        public int NRegistro { get; set; }
        public string Reclamo { get; set; }
        public string Estado { get; set; }
        public string Resolucion { get; set; }


        public Reclamos() { }

        public Reclamos(string linea)
        {
            var datos = linea.Split('-');
            NReclamo = int.Parse(datos[0]);
            NRegistro = int.Parse(datos[1]);
            Reclamo = (datos[2]);
            Estado = (datos[3]);
            Resolucion = (datos[4]);
        }

        static Reclamos()
        {
            string fileName = "TP4/TXT/Reclamos/Reclamos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string Reclamos = basePath + fileName;
            AllreclamosAlumnos.Clear();

            if (File.Exists(Reclamos))
            {
                using (var reader = new StreamReader(Reclamos))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var reclamo = new Reclamos(linea);

                        AllreclamosAlumnos.Add(new Reclamos()
                        {
                            NReclamo = reclamo.NReclamo,
                            NRegistro = reclamo.NRegistro,
                            Reclamo = reclamo.Reclamo,
                            Estado = reclamo.Estado,
                            Resolucion = reclamo.Resolucion,
                        });
                    }
                }
            }
        }

        
        public static bool VerReclamosAdministrador()
        {
            string Reclamos = "";
            bool Estado = true;

            foreach (var val in AllreclamosAlumnos)
            {
                Reclamos += "Numero de reclamo: " + val.NReclamo + "| Numero de registro: " + val.NRegistro + " | Descripcion reclamo: " + val.Reclamo + " | Estado: " + val.Estado + "\n";
            }
            if (Reclamos == "")
            {
                Console.WriteLine("\n¡No hay reclamos!");
            }
            else
            {
                Console.WriteLine(Reclamos);
                Estado = false;
                return Estado;
            }

            return Estado;

        }

        //VER
        public static void VerReclamosAlumno(int NRegistro)
        {
            Console.WriteLine("Tiene los siguientes reclamos a su nombre: ");
            int Acum = 0;

            foreach (var val in AllreclamosAlumnos)
            {
                if(val.NRegistro == NRegistro)
                {
                    if (!string.IsNullOrEmpty(val.Resolucion))
                    {
                        Console.WriteLine("Numero de reclamo: " + val.NReclamo + "| Numero de registro: " + val.NRegistro + " | Resolución del reclamo: " + val.Resolucion + " | Estado: " + val.Estado);
                        Acum++;
                    }
                    else
                    {
                        Console.WriteLine("Numero de reclamo: " + val.NReclamo + "| Numero de registro: " + val.NRegistro + " | Descripcion reclamo: " + val.Reclamo + " | Estado: " + val.Estado);
                        Acum++;
                    }
                }
            }

            if (Acum == 0)
            {
                Console.WriteLine("¡No tiene reclamos a su nombre!");
            }
        }

        public static void RealizarReclamo(int CodigoPersona)
        {
            string reclamo;
            bool Validacion;

            do
            {
                Validacion = true;
                Console.WriteLine("Describa el reclamo que desea realizar: ");
                reclamo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(reclamo))
                {
                    Console.WriteLine("No ha ingresado un reclamo válido");
                    Validacion = false;
                }

            } while (Validacion == false);
            
            int numeroReclamo = 0;
            string estado = "PENDIENTE";
            AgregarReclamo(numeroReclamo, CodigoPersona, reclamo, estado);


            Console.WriteLine("Reclamo registrado. Puede ver el estado del mismo desde el menu principal");
        }


        public static void AgregarReclamo(int NReclamo, int NRegistro, string reclamo, string estado)
        {
            foreach (var item in AllreclamosAlumnos)
            {
                NReclamo++;
            }

            int numeroReclamo = NReclamo + 1;

            AllreclamosAlumnos.Add(new Reclamos()
            {
                NRegistro = NRegistro,
                NReclamo = numeroReclamo,
                Reclamo = reclamo,
                Estado = estado,
            });
        }

        public static Reclamos Seleccionar()
        {
            var modelo = CrearModeloBusqueda();
            foreach (var reclamo in AllreclamosAlumnos)
            {
                if (reclamo.CoincideCon(modelo))
                {
                    return reclamo;
                }
            }

            Console.WriteLine("No se ha encontrado un reclamo que coincida");
            return null;
        }

        public static Reclamos CrearModeloBusqueda()
        {
            var modelo = new Reclamos();
            modelo.NReclamo = NumeroReclamo(obligatorio: false);
            return modelo;
        }

        public bool CoincideCon(Reclamos modelo)
        {
            if (modelo.NReclamo != 0 && modelo.NReclamo != NReclamo)
            {
                return false;
            }
            return true;

        }


        public static void ActualizarEstadoReclamo()
        {
            var reclamo = Seleccionar();
            if (reclamo == null)
            {
                ActualizarEstadoReclamo();
            }

            Console.WriteLine("\nNumero de reclamo: " + $"{reclamo.NReclamo}" + " Numero de registro: " + $"{reclamo.NRegistro}" + " Reclamo: " + $"{reclamo.Reclamo}" + " Estado: " + $"{reclamo.Estado}");

            string resolucion;
            bool Validacion;

            do
            {
                Validacion = true;
                Console.WriteLine($"\n¿Cual es su respuesta?");
                resolucion = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(resolucion))
                {
                    Console.WriteLine("No ha ingresado una respuesta válida");
                    Validacion = false;
                }

            } while (Validacion == false);

            Console.WriteLine($"Presionar S para marcar como solucionado el reclamo {reclamo.NReclamo}, o N para volver al menu\n");
            var key = Console.ReadLine();
            if (key.ToUpper() == "S")
            {
                Console.WriteLine("\nNumero de reclamo: " + $"{reclamo.NReclamo}" + " Numero de registro: " + $"{reclamo.NRegistro}" + " Reclamo: " + $"{reclamo.Reclamo}" + " Estado: " + "SOLUCIONADO");


                foreach (var item in AllreclamosAlumnos)
                {
                    if (item.NReclamo == reclamo.NReclamo)
                    {
                        item.Estado = "SOLUCIONADO";
                        item.Resolucion = resolucion;
                    }
                }

                Console.WriteLine("\nNumero de reclamo cambiado con exito.");
            }
            else if (key.ToUpper() == "N")
            {
                Console.WriteLine($"\n{reclamo.NReclamo} NO ha sido marcado como solucionado");
            }
            else
            {
                Console.WriteLine($"\n{reclamo.NReclamo} NO ha sido marcado como solucionado");
            }
        }

        public static void EscribirReclamosEnTXT()
        {
            string fileName = "TP4/TXT/Reclamos/Reclamos.txt";
            string basePath = Environment.CurrentDirectory;
            string PathCortada = Strings.Right(basePath, 13);
            basePath = basePath.Replace(PathCortada, "");
            string Reclamos = basePath + fileName;

            if (File.Exists(Reclamos))
            {
                File.WriteAllText(Reclamos, String.Empty);
                foreach (var item in AllreclamosAlumnos)
                {
                    using (StreamWriter sw = File.AppendText(Reclamos))
                    {
                        sw.WriteLine(item.NReclamo + "-" + item.NRegistro + "-" + item.Reclamo + "-" + item.Estado + "-" + item.Resolucion);
                    }                       
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado el archivo TXT. El archivo 'Reclamos.txt' debe estar en la carpeta TXT");
            }
        }


        // Validaciones
        private static int NumeroReclamo(bool obligatorio = true)
        {
            var titulo = "\n¿Cual es el numero de reclamo a actualizar?";

            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("No ha ingresado un numero de reclamo válido");
                    continue;
                }

                if (!int.TryParse(ingreso, out var reclamo))
                {
                    Console.WriteLine("No ha ingresado un numero de reclamo válido");
                    continue;
                }

                return reclamo;

            } while (true);
        }
    }
}


