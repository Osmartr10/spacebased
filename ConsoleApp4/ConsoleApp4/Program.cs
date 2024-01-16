using System;
using System.Collections.Generic;

public class Jugador
{
    public string Nombre { get; set; }
    public int Nivel { get; set; }
    public string Serv { get; set; }

    public Jugador(string nombre, int nivel, string serv)
    {
        Nombre = nombre;
        Nivel = nivel;
        Serv = serv;
    }
}


public class EspacioJuego
{
    private Dictionary<int, List<Jugador>> particionesDatosJugador;//lista adj con tabla hash y lista de jugadores -> Central

    public EspacioJuego(int numJugadores)
    {
        particionesDatosJugador = new Dictionary<int, List<Jugador>>();
        Console.WriteLine("Ingrese Servidores disponibles");
        int serv=int.Parse(Console.ReadLine());
       
        for (int i = 1; i <= serv; i++)
        {
            Console.WriteLine($"Ingrese Servidor numero {i}");
            string t=Console.ReadLine();
            particionesDatosJugador[i] = new List<Jugador>();
        }
    }

    public void AgregarDatosJugador(int nodo,Jugador jugador) // Instancia en los nodos(servidores)
    {
        if (!particionesDatosJugador.ContainsKey(nodo))
        {
            // no esta asi que agregamos al jugador al diccionario
            particionesDatosJugador[nodo] = new List<Jugador>();
        }
        particionesDatosJugador[nodo].Add(jugador);
        Console.WriteLine($"Jugador '{jugador.Nombre}' agregó datos de nivel: '{jugador.Nivel}'");
    }

    public IEnumerable<Jugador> ObtenerTodosLosDatosJugador()
    {
        List<Jugador> todosLosDatosJugador = new List<Jugador>();

        foreach (var datosJugador in particionesDatosJugador.Values)
        {
            todosLosDatosJugador.AddRange(datosJugador);
        }

        return todosLosDatosJugador;
    }
}

class Programa
{
    static void Main()
    {
        Console.WriteLine("Ingrese la cantidad de jugadores para crear el espacio compartido:");
        int numJugadores = int.Parse(Console.ReadLine());

        // Crear espacio compartido con la cantidad de particiones
        EspacioJuego gameSpace = new EspacioJuego(numJugadores);
        // Ejemplos de interacción con el espacio compartido del juego
        for (int i = 1; i <= numJugadores; i++)
        {
            Console.WriteLine($"Ingrese el nombre del Jugador{i}: ");
            string nombre = Console.ReadLine();
            Console.WriteLine($"Ingrese Nivel del jugador{i}:");
            int nivel = int.Parse(Console.ReadLine());
            Console.WriteLine($"Ingrese nombre del servidor que se encuentra");
            string nombreserv = Console.ReadLine();
            gameSpace.AgregarDatosJugador(i, new Jugador(nombre,nivel,nombreserv));
            
        }

        // Simular otro jugador accediendo a los datos del juego
        var allPlayerData = gameSpace.ObtenerTodosLosDatosJugador();

        Console.WriteLine("\nDatos de juego recuperados desde el espacio compartido:");
        foreach (var gameData in allPlayerData)
        {
            Console.WriteLine($"Jugador: {gameData.Nombre} con nivel {gameData.Nivel} en {gameData.Serv}");
        }

        Console.ReadLine();
    }
}
