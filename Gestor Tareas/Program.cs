using System;
using System.Net;
using System.Collections.Generic; 
using System.Threading;
using System.IO; 

class Program{

    static List<String> Tareas = new List<string>();
    static string archivoTareas = @"Tareas.txt";
    static string historialTareas = @"Historial de tareas completadas.txt";
    static bool continuar = true;

    static void Main(){

        CargarArchivoTareas();
        Console.Title = "Gestor de Tareas";

        while (continuar){

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("      Gestor de Tareas                     ");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("1. Agregar Tarea");
        Console.WriteLine("2. Marcar Tarea como completada");
        Console.WriteLine("3. Editar Tarea");
        Console.WriteLine("4. Visualizar Tareas Pendientes");
        Console.WriteLine("5. Tareas Completadas");
        Console.WriteLine("6. Salir");

        string opciones = Console.ReadLine()!;

            switch (opciones)
            {
                case "1":
                    AgregarTareas(); 
                break;

                case "2":
                    EliminarTareaCompletada(); 
                break;

                case "3":
                    EditarTarea(); 
                break;

                case "4":
                    TareasPendientes(); 
                break;

                case "5":
                    TareasCompletadas(); 
                break;

                case "6":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Esta seguro que desea salir? (Si / Otra tecla para cancelar)");
                    string confirmar = Console.ReadLine()?.ToLower() ?? "";
                    if(confirmar == "si" || confirmar == "s"){
                        Console.WriteLine("Saliendo del programa... Hasta pronto...");
                        Thread.Sleep(1000);
                        continuar = false;
                    }else{
                        Thread.Sleep(2000);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("|X| Accion cancelada");
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                break;

                default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("|X| Opción no válida. Inténtalo de nuevo.");
                Console.ResetColor();
                Thread.Sleep(1000);
               
                break;
            }
        }
    }

    static void AgregarTareas(){

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("      Agregar Tareas                       ");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bienvenido, porfavor ingrese una tarea para agregarla al sistema");
            Console.ResetColor();
            string tareagregada = Console.ReadLine() ?? "";
            {
            if (string.IsNullOrEmpty(tareagregada))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: El nombre de la tarea NO puede estar vacio. Volvera al menu principal");
                    Console.ResetColor();
                    continue;    
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bien! ahora porfavor ingrese la fecha de finalizacion de la tarea. FORMATO: DD/MM/AAAA o DD de MM");
            Console.Write("EJEMPLO: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("12/2/2025 o 12 de Febrero");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;

            string fechatarea = Console.ReadLine() ?? "";
            {
            
            if (string.IsNullOrEmpty(fechatarea))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: La fecha de la tarea NO puede estar vacio");
                    Console.ResetColor();
                    continue;
                }
            }
        string fechamastarea = ($"{tareagregada} | Fecha de finalizacion: {fechatarea}");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Agregando tarea, porfavor espere...");
        Thread.Sleep(3000);
        Console.WriteLine("Terminando de agregar la tarea");
        Thread.Sleep(2000);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"Tarea: ");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{tareagregada}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine (" fue agregada con exito!");
        Console.WriteLine($"La fecha de finalizacion de la tarea es el: ");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{fechatarea}");
        File.AppendAllText(archivoTareas, fechamastarea + Environment.NewLine);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Desea agregar otra tarea? ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("SI");
        Console.ResetColor();
        Console.Write("/");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("NO");
        Console.ResetColor();

        while (true){

        string siono = Console.ReadLine()?.ToLower() ?? "";

            if (siono == "si" || siono == "s")
            {
                Console.Clear(); 
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("      Agregar Tareas                       ");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine();
                break;
                
                
            }
            else if(siono == "no" || siono == "n")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Volviendo al menu principal, porfavor espere unos segundos!");
                Thread.Sleep(1000);
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR: ");
                Console.ResetColor();
                Console.WriteLine("Porfavor, ponga 'SI' o 'S' si quiere agregar otra tarea. Si no, ponga 'NO' o 'N' para salir.");
                Console.WriteLine();
                Console.WriteLine("Desea agregar otra tarea? ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("SI");
                Console.ResetColor();
                Console.Write("/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NO");
                Console.ResetColor();
                }
            }
        }

    }


    static void EliminarTareaCompletada(){

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("      Marcar Tareas Completadas          ");
        Console.WriteLine("-------------------------------------------");
        Console.ResetColor();

        CargarArchivoTareas();

        if (Tareas.Count == 0)

        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No hay tareas pendientes para eliminar.");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Seleccione el número de la tarea a eliminar:");
        for (int i = 0; i < Tareas.Count; i++)

        {   
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{i + 1}. {Tareas[i]}");
        }

        if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= Tareas.Count){

        Console.ForegroundColor = ConsoleColor.Red;  
        Console.WriteLine("Esta seguro que desea eliminar/marcar la tarea como lista? (Si / Otra tecla para cancelar)");
            
        string confirmar = Console.ReadLine()?.ToLower() ?? "";
            
            if(confirmar == "si" || confirmar == "s"){

                Thread.Sleep(1000);

            }else{
                Thread.Sleep(2000);
                Console.WriteLine("⚠ Accion cancelada");
                return;
            }
        
            string tareaEliminada = Tareas[indice - 1];
            Tareas.RemoveAt(indice - 1);

            File.WriteAllLines(archivoTareas, Tareas);
            File.AppendAllText(historialTareas, $"{DateTime.Now} - {tareaEliminada}{Environment.NewLine}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tarea eliminada y marcada como lista en el historial.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Entrada no válida. Intente de nuevo.");
        }
        Console.ResetColor();
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();

    }

    static void EditarTarea(){

        Console.Clear();
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("      Editar Tarea                         ");
        Console.WriteLine("-------------------------------------------");

        if (Tareas.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No hay tareas pendientes para editar.");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\nSeleccione el número de la tarea a editar:");

        for (int i = 0; i < Tareas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Tareas[i]}");
        }

        if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= Tareas.Count)
        {
            Console.Write("\nIngrese la nueva descripción de la tarea: ");
            string nuevaDescripcion = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(nuevaDescripcion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: La descripción no puede estar vacía.");
                Console.ResetColor();
                return;
            }

            Console.Write("\nIngrese la nueva fecha de finalización (DD/MM/AAAA o 12 de Febrero): ");
            string nuevaFecha = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(nuevaFecha))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: La fecha no puede estar vacía.");
                Console.ResetColor();
                return;
            }

            Tareas[indice - 1] = $"{nuevaDescripcion} | Fecha de finalización: {nuevaFecha}";
            File.WriteAllLines(archivoTareas, Tareas);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Tarea editada con éxito.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠ Entrada no válida.");
        }
        Console.ResetColor();
        Console.ReadKey();
    }


    static void TareasPendientes(){

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("      Tareas Pendientes                    ");
        Console.WriteLine("-------------------------------------------");
        Console.ForegroundColor = ConsoleColor.White;
        CargarArchivoTareas();
        
        Console.ForegroundColor = ConsoleColor.Red;
        if (Tareas.Count == 0){
            Console.WriteLine("No hay tareas pendientes");
        }else{

        
        for(int i = 0; i < Tareas.Count; i++){
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{i + 1}. {Tareas[i]}");
        }

        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
        
    }

    static void TareasCompletadas(){

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("      Historial de Tareas Completadas      ");
        Console.WriteLine("-------------------------------------------");

        if (!File.Exists(historialTareas)) 
        {
            using (StreamWriter sw = File.CreateText(historialTareas)) {}
        }

        string[] historialTareasCompletadas = File.ReadAllLines(historialTareas);
        if(historialTareasCompletadas.Length == 0){

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El historial esta vacio");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (var hisTareas in historialTareasCompletadas)
        {

                Console.WriteLine(@$"\(ºvº)/ {hisTareas}");
        }

        Console.ResetColor();
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();

    }

    static void CargarArchivoTareas(){

        
        if (!File.Exists(archivoTareas)){

            using (StreamWriter sw = File.CreateText(archivoTareas)){}
        }

        var TareasDesdeArchivo = File.ReadAllLines(archivoTareas);
        Tareas.Clear();

        foreach (var tarea in TareasDesdeArchivo){

            if(!string.IsNullOrEmpty(tarea))
            {
                Tareas.Add(tarea.Trim());
            }
        }

        
    }

}
