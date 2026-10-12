using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso_Empleado
{
    internal class Program
    {
        static int max_emp = 50;
        static string[]nombres_emp= new string[max_emp];
        static string[]correo_emp=new string[max_emp];
        static int cont_emp = 0;
        static string ruta_archivo = "empleados.txt";

        static public void Titulo()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("             --SISTEMA DE EMPLEADOS ARREGLOS Y CADENAS--             ");
            Console.WriteLine("-----------------------------------------------------------------------");
        }

        static public void Registrar_Empleado()
        {
            Console.WriteLine("------------REGISTRO DE EMPLEADO---------------");
            if(cont_emp >= max_emp)
            {
                Console.WriteLine("Capacidad maxima alcanzada");
                return;
            }
            Console.WriteLine("Ingrese el nombre y apellido del empleado: ");
            string nombre = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("Error: El nombre no puede estar vacío......!");
                return;
            }
            string nombres_Mayus= nombre.ToUpper();
            string[] partes = nombres_Mayus.Split(' ');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "Usuario";
            string correo = $"{primer_nombre.ToLower()}@upn.edu.pe";
            nombres_emp[cont_emp] = nombres_Mayus;
            correo_emp[cont_emp] = correo;
            cont_emp++;

            Console.WriteLine("Empleado registrado exitosamente");
        }

        static public void buscar_emp()
        {
            Console.WriteLine("------------BUSCAR EMPLEADO---------------");
            Console.WriteLine("---------------------------------------------------");
            if (cont_emp == 0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            Console.Write("Ingrese el nombre del empleado a buscar: ");
            string buscar_nombre = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(buscar_nombre))
                return;
            bool encontrado = false;
            for (int i = 0; i < cont_emp; i++)
            {
                if (nombres_emp[i].Contains(buscar_nombre))
                {
                    Console.WriteLine("Nombre: " + nombres_emp[i] + "Correo: " +  correo_emp[i] );
                    encontrado = true;
                }
            }
            if (!encontrado)
                Console.WriteLine("Empleado no encontrado");
        }

        static public void ordenar_Alfabeticamente()
        {
            Console.WriteLine("------------ORDENAR EMPLEADOS ALFABETICAMENTE---------------");
            if (cont_emp <= 1)
            {
                Console.WriteLine("No hay suficientes empleados para ordenar");
                return;
            }
            for (int i = 0; i < cont_emp - 1; i++)
            {
                for (int j = 0; j < cont_emp - 1 - i; j++)
                {
                    if (string.Compare(nombres_emp[j], nombres_emp[j + 1]) > 0)
                    {
                        string temp_nombre = nombres_emp[j];
                        nombres_emp[j] = nombres_emp[j + 1];
                        nombres_emp[j + 1] = temp_nombre;

                        string temp_correo = correo_emp[j];
                        correo_emp[j] = correo_emp[j + 1];
                        correo_emp[j + 1] = temp_correo;
                    }
                }
            }
        }

        static public void mostrar()
        {
            Console.WriteLine("------------LISTA DE EMPLEADOS---------------");
            Console.WriteLine("---------------------------------------------------");
            if (cont_emp == 0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            Console.WriteLine("Nombres y Apellidos\t\tCorreo corporativo");
            for (int i = 0; i < cont_emp; i++)
            {
                Console.WriteLine(nombres_emp[i] + "\t\t" + correo_emp[i]);
            }
        }
        static public void Guardar_datos()
        {
            Console.WriteLine("------------GUARDAR DATOS EN ARCHIVO---------------");
            Console.WriteLine("---------------------------------------------------");
            if (cont_emp == 0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            using (StreamWriter escribir = new StreamWriter(ruta_archivo))
            {
                for (int i = 0; i < cont_emp; i++)
                {
                    escribir.WriteLine(nombres_emp[i] + "," + correo_emp[i]);
                }
            }
            Console.WriteLine(cont_emp + "Datos guardados exitosamente en el archivo");
        }

        static public void Leer_datos()
        {
            Console.WriteLine("------------LEER DATOS DEL ARCHIVO---------------");
            Console.WriteLine("---------------------------------------------------");
            if (!File.Exists(ruta_archivo))
            {
                Console.WriteLine("El archivo no existe");
                return;
            }
            using (StreamReader leer = new StreamReader(ruta_archivo))
            {
                string linea;
                while ((linea = leer.ReadLine()) != null)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine("Nombre: " + partes[0] + " Correo: " + partes[1]);
                    }
                }
            }
        }

        static public void Cargar_datos()
        {
         Console.WriteLine("------------CARGAR DATOS DESDE EL ARCHIVO---------------");
         Console.WriteLine("---------------------------------------------------");
            if (!File.Exists(ruta_archivo))
            {
                Console.WriteLine("El archivo no existe");
                return;
            }
            string[] lineas= File.ReadAllLines(ruta_archivo);
            cont_emp = 0;
            for(int i=0; i<lineas.Length && i < max_emp; i++)
            {
                string[] partes = lineas[i].Split(',');
                if (partes.Length == 2)
                {
                    nombres_emp[cont_emp] = partes[0];
                    correo_emp[cont_emp] = partes[1];
                    cont_emp++;
                }
            }
            Console.WriteLine("Datos cargados exitosamente desde el archivo");
        }
        static void Main(string[] args)
        {
            int opc=0;
            while (opc !=8)
            {
                Console.Clear();
                Console.WriteLine("[1]Registrar empleado");
                Console.WriteLine("[2]Buscar Empleado");
                Console.WriteLine("[3]Ordenar Alfabeticamente");
                Console.WriteLine("[4]Mostrar Resultados");
                Console.WriteLine("[5]Guardar datos en archivos");
                Console.WriteLine("[6]Leer datos del archivos");
                Console.WriteLine("[7]Cargar datos desde el archivo");
                Console.WriteLine("[8]Salir");
                Console.Write("Ingresar opcion: ");
                if(!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("Error: Ingrese una opcion numerica");
                    continue;
                }
                switch (opc)
                {
                    case 1:
                        Registrar_Empleado(); break;
                    case 2:
                        buscar_emp(); break;
                    case 3:
                        ordenar_Alfabeticamente(); break;
                    case 4:
                        mostrar(); break;
                    case 5:
                        Guardar_datos(); break;
                    case 6:
                        Leer_datos(); break;
                    case 7:
                        Cargar_datos(); break;
                    case 8:
                        Console.WriteLine("Gracias por usar el programa"); break;
                    default:
                        Console.WriteLine("Opcion invalida"); break;
                }
                Console.ReadKey();
            }
        }
    }
}
