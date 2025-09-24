using System;
using CrazyRisk.Logic;

class Program
{
    static void Main()
    {
        // 1) Crear juego y preparar partida
        Juego juego = new Juego();
        juego.PrepararPartida();

        Console.WriteLine("=== CrazyRisk - Prueba de combate ===");

        // 2) Iniciar turno del primer jugador (Comandante 1)
        Jugador atacante = juego.Jugadores.Obtener(0);
        Jugador defensor = juego.Jugadores.Obtener(1);
        juego.IniciarTurno(atacante);

        // 3) Buscar dos territorios adyacentes de distinto propietario
        Territorio territorioAtacante = null;
        Territorio territorioDefensor = null;

        int i = 0;
        while (i < atacante.Territorios.Cantidad && territorioAtacante == null)
        {
            Territorio tA = atacante.Territorios.Obtener(i);

            int k = 0;
            while (k < tA.TerritoriosAdyacentes.Cantidad && territorioAtacante == null)
            {
                Territorio vecino = tA.TerritoriosAdyacentes.Obtener(k);
                if (vecino.Propietario == defensor)
                {
                    territorioAtacante = tA;
                    territorioDefensor = vecino;
                }
                k = k + 1;
            }

            i = i + 1;
        }

        // Si no se encontró pareja, forzar una (reasignar el primer vecino al defensor)
        if (territorioAtacante == null)
        {
            // Tomamos el primer territorio del atacante con algún vecino
            if (atacante.Territorios.Cantidad == 0)
            {
                Console.WriteLine("El atacante no tiene territorios. Algo salió mal en la preparación.");
                return;
            }

            Territorio tA = atacante.Territorios.Obtener(0);
            if (tA.TerritoriosAdyacentes.Cantidad == 0)
            {
                Console.WriteLine("No hay adyacencias en el mapa. Revisa la configuración del mapa.");
                return;
            }

            Territorio vecino = tA.TerritoriosAdyacentes.Obtener(0);

            // Quitar al vecino de su dueño actual
            Jugador duenoAnterior = vecino.Propietario;
            if (duenoAnterior != null)
            {
                int z = 0;
                while (z < duenoAnterior.Territorios.Cantidad)
                {
                    if (duenoAnterior.Territorios.Obtener(z) == vecino)
                    {
                        duenoAnterior.Territorios.Remover(z);
                        break;
                    }
                    z = z + 1;
                }
            }

            // Asignar el vecino al defensor
            vecino.Propietario = defensor;
            defensor.Territorios.Agregar(vecino);

            territorioAtacante = tA;
            territorioDefensor = vecino;
        }

        // 4) Asegurar tropas mínimas para poder atacar (≥2 en atacante, y para mover ≥3 si hace falta)
        if (territorioAtacante.Tropas < 4) territorioAtacante.Tropas = 4; // 3 dados + dejar 1
        if (territorioDefensor.Tropas < 2) territorioDefensor.Tropas = 2;

        // 5) Configurar dados del defensor y ejecutar ataque
        juego.dadosDefensor = 2; // la regla permite 1 o 2; aquí probamos con 2
        int tarjetasAntes = atacante.Tarjetas.Cantidad;

        Console.WriteLine();
        Console.WriteLine("ANTES DEL ATAQUE");
        Console.WriteLine("Atacante: " + atacante.Alias);
        Console.WriteLine("Territorio Atacante: " + territorioAtacante.Nombre + " | Tropas: " + territorioAtacante.Tropas);
        Console.WriteLine("Defensor: " + defensor.Alias);
        Console.WriteLine("Territorio Defensor: " + territorioDefensor.Nombre + " | Tropas: " + territorioDefensor.Tropas);
        Console.WriteLine("Tarjetas atacante: " + tarjetasAntes);

        // Atacamos con 3 (máximo) — internamente se limita si no se puede
        juego.ResolverAtaque(territorioAtacante, territorioDefensor, 3);

        Console.WriteLine();
        Console.WriteLine("DESPUES DEL ATAQUE");
        Console.WriteLine("Territorio Atacante: " + territorioAtacante.Nombre + " | Tropas: " + territorioAtacante.Tropas);
        Console.WriteLine("Territorio Defensor: " + territorioDefensor.Nombre + " | Tropas: " + territorioDefensor.Tropas);
        Console.WriteLine("Propietario del territorio defensor ahora: " + territorioDefensor.Propietario.Alias);

        int tarjetasDespues = atacante.Tarjetas.Cantidad;
        Console.WriteLine("Tarjetas atacante: " + tarjetasDespues);

        if (tarjetasDespues > tarjetasAntes)
        {
            Console.WriteLine("El atacante recibió una tarjeta por conquistar.");
        }
        else
        {
            Console.WriteLine("No hubo conquista o no se otorgó tarjeta.");
        }

        Console.WriteLine();
        Console.WriteLine("Prueba finalizada.");
    }
}
