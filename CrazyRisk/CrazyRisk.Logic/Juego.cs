using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyRisk.Logic
{
    public class Juego
    {
        public Mapa MapaDelJuego { get; private set; }
        public ListaSimple<Jugador> Jugadores { get; private set; }
        public Jugador? JugadorEnTurno { get; private set; }

        private Random azar = new Random();
        private bool planeacionUsadaEsteTurno = false;
        public int dadosDefensor = 2; 
        // Serie de Fibonacci a partir de 2 para los canjes
        private int[] fibonacci = new int[] { 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };


        public FaseDeJuego FaseActual { get; private set; }
        public int IndiceJugadorColocandoTropas { get; private set; }
        
        public Juego()
        {
            MapaDelJuego = new Mapa();
            Jugadores = new ListaSimple<Jugador>();
            FaseActual = FaseDeJuego.ColocacionInicial;
            IndiceJugadorColocandoTropas = 0; // Empezamos con el primer jugador
        }



        // Este método se encargará de configurar todo al inicio de una partida.
        public void PrepararPartida()
        {
            // ---- PASO 1: CREAR JUGADORES ----
            Jugador jugador1 = new Jugador("Comandante 1", "Azul");
            Jugador jugador2 = new Jugador("Comandante 2", "Rojo");
            Jugador ejercitoNeutral = new Jugador("Neutral", "Gris");

            Jugadores.Agregar(jugador1);
            Jugadores.Agregar(jugador2);
            Jugadores.Agregar(ejercitoNeutral);

            // ---- PASO 2: CREAR EL MAPA (CONTINENTES Y TERRITORIOS) ----

            // Crear continentes

            Continente americaDelNorte = new Continente("América del Norte", 3);
            Continente americaDelSur = new Continente("América del Sur", 2);
            Continente europa = new Continente("Europa", 5);
            Continente africa = new Continente("África", 3);
            Continente asia = new Continente("Asia", 7);
            Continente oceania = new Continente("Oceanía", 2);

            // Crear territorios para america del norte
            Territorio alaska = new Territorio("Alaska") { X = 50, Y = 100 };
            Territorio alberta = new Territorio("Alberta") { X = 160, Y = 180 };
            Territorio americaCentral = new Territorio("América Central") { X = 210, Y = 370 };
            Territorio estadosUnidosOrientales = new Territorio("Estados Unidos Orientales") { X = 270, Y = 290 };
            Territorio groenlandia = new Territorio("Groenlandia") { X = 350, Y = 80 };
            Territorio territorioDelNoroeste = new Territorio("Territorio del Noroeste") { X = 150, Y = 110 };
            Territorio ontario = new Territorio("Ontario") { X = 240, Y = 190 };
            Territorio quebec = new Territorio("Quebec") { X = 320, Y = 195 };
            Territorio estadosUnidosOccidentales = new Territorio("Estados Unidos Occidentales") { X = 180, Y = 280 };

            // Añadir los territorios a america del norte
            americaDelNorte.Territorios.Agregar(alaska);
            americaDelNorte.Territorios.Agregar(alberta);
            americaDelNorte.Territorios.Agregar(americaCentral);
            americaDelNorte.Territorios.Agregar(estadosUnidosOrientales);
            americaDelNorte.Territorios.Agregar(groenlandia);
            americaDelNorte.Territorios.Agregar(territorioDelNoroeste);
            americaDelNorte.Territorios.Agregar(ontario);
            americaDelNorte.Territorios.Agregar(quebec);
            americaDelNorte.Territorios.Agregar(estadosUnidosOccidentales);


            // Crear territorios para america del sur
            Territorio argentina = new Territorio("Argentina") { X = 320, Y = 610 };
            Territorio brasil = new Territorio("Brasil") { X = 380, Y = 510 };
            Territorio peru = new Territorio("Perú") { X = 290, Y = 530 };
            Territorio venezuela = new Territorio("Venezuela") { X = 300, Y = 450 };

            // Añadir los territorios a america del sur
            americaDelSur.Territorios.Agregar(argentina);
            americaDelSur.Territorios.Agregar(brasil);
            americaDelSur.Territorios.Agregar(peru);
            americaDelSur.Territorios.Agregar(venezuela);

            // Crear territorios para europa
            Territorio granBretaña = new Territorio("Gran Bretaña") { X = 490, Y = 240 };
            Territorio islandia = new Territorio("Islandia") { X = 480, Y = 150 };
            Territorio europaNorte = new Territorio("Europa del Norte") { X = 580, Y = 250 };
            Territorio escandinavia = new Territorio("Escandinavia") { X = 580, Y = 160 };
            Territorio europaSur = new Territorio("Europa del Sur") { X = 590, Y = 330 };
            Territorio ucrania = new Territorio("Ucrania") { X = 690, Y = 220 };
            Territorio europaOccidental = new Territorio("Europa Occidental") { X = 490, Y = 330 };

            // Añadir los territorios a europa
            europa.Territorios.Agregar(granBretaña);
            europa.Territorios.Agregar(islandia);
            europa.Territorios.Agregar(europaNorte);
            europa.Territorios.Agregar(escandinavia);
            europa.Territorios.Agregar(europaSur);
            europa.Territorios.Agregar(ucrania);
            europa.Territorios.Agregar(europaOccidental);

            // Crear territorios para africa
            Territorio egipto = new Territorio("Egipto") { X = 630, Y = 440 };
            Territorio congo = new Territorio("Congo") { X = 620, Y = 580 };
            Territorio africaDelNorte = new Territorio("África del Norte") { X = 520, Y = 460 };
            Territorio africaOriental = new Territorio("África Oriental") { X = 690, Y = 510 };
            Territorio madagascar = new Territorio("Madagascar") { X = 750, Y = 650 };
            Territorio sudafrica = new Territorio("Sudáfrica") { X = 640, Y = 670 };

            // Añadir los territorios a africa
            africa.Territorios.Agregar(egipto);
            africa.Territorios.Agregar(congo);
            africa.Territorios.Agregar(africaDelNorte);
            africa.Territorios.Agregar(africaOriental);
            africa.Territorios.Agregar(madagascar);
            africa.Territorios.Agregar(sudafrica);

            // Crear territorios para asia
            Territorio afganistan = new Territorio("Afganistán") { X = 780, Y = 290 };
            Territorio china = new Territorio("China") { X = 900, Y = 330 };
            Territorio india = new Territorio("India") { X = 840, Y = 430 };
            Territorio irkutsk = new Territorio("Irkutsk") { X = 960, Y = 180 };
            Territorio japon = new Territorio("Japón") { X = 1100, Y = 270 };
            Territorio kamchatka = new Territorio("Kamchatka") { X = 1060, Y = 100 };
            Territorio orienteMedio = new Territorio("Oriente Medio") { X = 710, Y = 380 };
            Territorio mongolia = new Territorio("Mongolia") { X = 980, Y = 250 };
            Territorio siam = new Territorio("Siam") { X = 960, Y = 440 };
            Territorio siberia = new Territorio("Siberia") { X = 880, Y = 120 };
            Territorio ural = new Territorio("Ural") { X = 800, Y = 180 };
            Territorio yakutsk = new Territorio("Yakutsk") { X = 970, Y = 90 };

            // Añadir los territorios a asia
            asia.Territorios.Agregar(afganistan);
            asia.Territorios.Agregar(china);
            asia.Territorios.Agregar(india);
            asia.Territorios.Agregar(irkutsk);
            asia.Territorios.Agregar(japon);
            asia.Territorios.Agregar(kamchatka);
            asia.Territorios.Agregar(orienteMedio);
            asia.Territorios.Agregar(mongolia);
            asia.Territorios.Agregar(siam);
            asia.Territorios.Agregar(siberia);
            asia.Territorios.Agregar(ural);
            asia.Territorios.Agregar(yakutsk);

            // Crear territorios para oceania
            Territorio australiaOriental = new Territorio("Australia Oriental") { X = 1120, Y = 650 };
            Territorio australiaOccidental = new Territorio("Australia Occidental") { X = 1020, Y = 660 };
            Territorio nuevaGuinea = new Territorio("Nueva Guinea") { X = 1100, Y = 550 };
            Territorio indonesia = new Territorio("Indonesia") { X = 980, Y = 540 };

            // Añadir los territorios a oceania
            oceania.Territorios.Agregar(australiaOriental);
            oceania.Territorios.Agregar(australiaOccidental);
            oceania.Territorios.Agregar(nuevaGuinea);
            oceania.Territorios.Agregar(indonesia);

            // ---- PASO 3: DEFINIR ADYACENCIAS (VECINOS) ----
            // America del Norte
            alaska.TerritoriosAdyacentes.Agregar(territorioDelNoroeste);
            alaska.TerritoriosAdyacentes.Agregar(alberta);
            alaska.TerritoriosAdyacentes.Agregar(kamchatka); // A través del estrecho de Bering

            territorioDelNoroeste.TerritoriosAdyacentes.Agregar(alaska);
            territorioDelNoroeste.TerritoriosAdyacentes.Agregar(alberta);
            territorioDelNoroeste.TerritoriosAdyacentes.Agregar(ontario);
            territorioDelNoroeste.TerritoriosAdyacentes.Agregar(groenlandia);

            groenlandia.TerritoriosAdyacentes.Agregar(territorioDelNoroeste);
            groenlandia.TerritoriosAdyacentes.Agregar(ontario);
            groenlandia.TerritoriosAdyacentes.Agregar(quebec);
            groenlandia.TerritoriosAdyacentes.Agregar(islandia);

            alberta.TerritoriosAdyacentes.Agregar(alaska);
            alberta.TerritoriosAdyacentes.Agregar(territorioDelNoroeste);
            alberta.TerritoriosAdyacentes.Agregar(ontario);
            alberta.TerritoriosAdyacentes.Agregar(estadosUnidosOccidentales);

            ontario.TerritoriosAdyacentes.Agregar(territorioDelNoroeste);
            ontario.TerritoriosAdyacentes.Agregar(alberta);
            ontario.TerritoriosAdyacentes.Agregar(estadosUnidosOccidentales);
            ontario.TerritoriosAdyacentes.Agregar(estadosUnidosOrientales);
            ontario.TerritoriosAdyacentes.Agregar(quebec);
            ontario.TerritoriosAdyacentes.Agregar(groenlandia);

            quebec.TerritoriosAdyacentes.Agregar(groenlandia);
            quebec.TerritoriosAdyacentes.Agregar(ontario);
            quebec.TerritoriosAdyacentes.Agregar(estadosUnidosOrientales);

            estadosUnidosOccidentales.TerritoriosAdyacentes.Agregar(alberta);
            estadosUnidosOccidentales.TerritoriosAdyacentes.Agregar(ontario);
            estadosUnidosOccidentales.TerritoriosAdyacentes.Agregar(estadosUnidosOrientales);
            estadosUnidosOccidentales.TerritoriosAdyacentes.Agregar(americaCentral);

            estadosUnidosOrientales.TerritoriosAdyacentes.Agregar(ontario);
            estadosUnidosOrientales.TerritoriosAdyacentes.Agregar(quebec);
            estadosUnidosOrientales.TerritoriosAdyacentes.Agregar(estadosUnidosOccidentales);
            estadosUnidosOrientales.TerritoriosAdyacentes.Agregar(americaCentral);

            americaCentral.TerritoriosAdyacentes.Agregar(estadosUnidosOccidentales);
            americaCentral.TerritoriosAdyacentes.Agregar(estadosUnidosOrientales);
            americaCentral.TerritoriosAdyacentes.Agregar(venezuela);

            // America del Sur
            venezuela.TerritoriosAdyacentes.Agregar(americaCentral);
            venezuela.TerritoriosAdyacentes.Agregar(brasil);
            venezuela.TerritoriosAdyacentes.Agregar(peru);

            peru.TerritoriosAdyacentes.Agregar(venezuela);
            peru.TerritoriosAdyacentes.Agregar(brasil);
            peru.TerritoriosAdyacentes.Agregar(argentina);

            argentina.TerritoriosAdyacentes.Agregar(brasil);
            argentina.TerritoriosAdyacentes.Agregar(peru);

            brasil.TerritoriosAdyacentes.Agregar(argentina);
            brasil.TerritoriosAdyacentes.Agregar(peru);
            brasil.TerritoriosAdyacentes.Agregar(venezuela);
            brasil.TerritoriosAdyacentes.Agregar(africaDelNorte); // A través del Atlántico

            // Europa
            islandia.TerritoriosAdyacentes.Agregar(groenlandia);
            islandia.TerritoriosAdyacentes.Agregar(granBretaña);
            islandia.TerritoriosAdyacentes.Agregar(escandinavia);

            escandinavia.TerritoriosAdyacentes.Agregar(islandia);
            escandinavia.TerritoriosAdyacentes.Agregar(ucrania);
            escandinavia.TerritoriosAdyacentes.Agregar(europaNorte);
            escandinavia.TerritoriosAdyacentes.Agregar(granBretaña);

            granBretaña.TerritoriosAdyacentes.Agregar(islandia);
            granBretaña.TerritoriosAdyacentes.Agregar(escandinavia);
            granBretaña.TerritoriosAdyacentes.Agregar(europaOccidental);
            granBretaña.TerritoriosAdyacentes.Agregar(europaNorte);

            europaNorte.TerritoriosAdyacentes.Agregar(granBretaña);
            europaNorte.TerritoriosAdyacentes.Agregar(escandinavia);
            europaNorte.TerritoriosAdyacentes.Agregar(ucrania);
            europaNorte.TerritoriosAdyacentes.Agregar(europaOccidental);
            europaNorte.TerritoriosAdyacentes.Agregar(europaSur);

            europaOccidental.TerritoriosAdyacentes.Agregar(granBretaña);
            europaOccidental.TerritoriosAdyacentes.Agregar(europaNorte);
            europaOccidental.TerritoriosAdyacentes.Agregar(europaSur);
            europaOccidental.TerritoriosAdyacentes.Agregar(africaDelNorte);

            europaSur.TerritoriosAdyacentes.Agregar(europaOccidental);
            europaSur.TerritoriosAdyacentes.Agregar(europaNorte);
            europaSur.TerritoriosAdyacentes.Agregar(ucrania);
            europaSur.TerritoriosAdyacentes.Agregar(egipto);
            europaSur.TerritoriosAdyacentes.Agregar(africaDelNorte);
            europaSur.TerritoriosAdyacentes.Agregar(orienteMedio);

            ucrania.TerritoriosAdyacentes.Agregar(escandinavia);
            ucrania.TerritoriosAdyacentes.Agregar(europaNorte);
            ucrania.TerritoriosAdyacentes.Agregar(europaSur);
            ucrania.TerritoriosAdyacentes.Agregar(orienteMedio);
            ucrania.TerritoriosAdyacentes.Agregar(ural);
            ucrania.TerritoriosAdyacentes.Agregar(afganistan);

            // África
            africaDelNorte.TerritoriosAdyacentes.Agregar(egipto);
            africaDelNorte.TerritoriosAdyacentes.Agregar(europaOccidental);
            africaDelNorte.TerritoriosAdyacentes.Agregar(europaSur);
            africaDelNorte.TerritoriosAdyacentes.Agregar(brasil); // A través del Atlántico
            africaDelNorte.TerritoriosAdyacentes.Agregar(congo);
            africaDelNorte.TerritoriosAdyacentes.Agregar(africaOriental);

            congo.TerritoriosAdyacentes.Agregar(africaDelNorte);
            congo.TerritoriosAdyacentes.Agregar(africaOriental);
            congo.TerritoriosAdyacentes.Agregar(sudafrica);

            egipto.TerritoriosAdyacentes.Agregar(africaDelNorte);
            egipto.TerritoriosAdyacentes.Agregar(europaSur);
            egipto.TerritoriosAdyacentes.Agregar(orienteMedio);
            egipto.TerritoriosAdyacentes.Agregar(africaOriental);

            africaOriental.TerritoriosAdyacentes.Agregar(africaDelNorte);
            africaOriental.TerritoriosAdyacentes.Agregar(egipto);
            africaOriental.TerritoriosAdyacentes.Agregar(congo);
            africaOriental.TerritoriosAdyacentes.Agregar(sudafrica);
            africaOriental.TerritoriosAdyacentes.Agregar(madagascar);
            africaOriental.TerritoriosAdyacentes.Agregar(orienteMedio);

            sudafrica.TerritoriosAdyacentes.Agregar(congo);
            sudafrica.TerritoriosAdyacentes.Agregar(africaOriental);
            sudafrica.TerritoriosAdyacentes.Agregar(madagascar);

            madagascar.TerritoriosAdyacentes.Agregar(africaOriental);
            madagascar.TerritoriosAdyacentes.Agregar(sudafrica);

            // Asia
            orienteMedio.TerritoriosAdyacentes.Agregar(egipto);
            orienteMedio.TerritoriosAdyacentes.Agregar(europaSur);
            orienteMedio.TerritoriosAdyacentes.Agregar(ucrania);
            orienteMedio.TerritoriosAdyacentes.Agregar(afganistan);
            orienteMedio.TerritoriosAdyacentes.Agregar(india);
            orienteMedio.TerritoriosAdyacentes.Agregar(africaOriental);

            afganistan.TerritoriosAdyacentes.Agregar(orienteMedio);
            afganistan.TerritoriosAdyacentes.Agregar(ucrania);
            afganistan.TerritoriosAdyacentes.Agregar(ural);
            afganistan.TerritoriosAdyacentes.Agregar(india);
            afganistan.TerritoriosAdyacentes.Agregar(china);

            ural.TerritoriosAdyacentes.Agregar(ucrania);
            ural.TerritoriosAdyacentes.Agregar(afganistan);
            ural.TerritoriosAdyacentes.Agregar(siberia);
            ural.TerritoriosAdyacentes.Agregar(china);

            siberia.TerritoriosAdyacentes.Agregar(ural);
            siberia.TerritoriosAdyacentes.Agregar(irkutsk);
            siberia.TerritoriosAdyacentes.Agregar(yakutsk);
            siberia.TerritoriosAdyacentes.Agregar(mongolia);
            siberia.TerritoriosAdyacentes.Agregar(china);

            yakutsk.TerritoriosAdyacentes.Agregar(siberia);
            yakutsk.TerritoriosAdyacentes.Agregar(irkutsk);
            yakutsk.TerritoriosAdyacentes.Agregar(kamchatka);

            kamchatka.TerritoriosAdyacentes.Agregar(yakutsk);
            kamchatka.TerritoriosAdyacentes.Agregar(irkutsk);
            kamchatka.TerritoriosAdyacentes.Agregar(mongolia);
            kamchatka.TerritoriosAdyacentes.Agregar(japon);
            kamchatka.TerritoriosAdyacentes.Agregar(alaska); // A través del estrecho de Bering

            irkutsk.TerritoriosAdyacentes.Agregar(siberia);
            irkutsk.TerritoriosAdyacentes.Agregar(yakutsk);
            irkutsk.TerritoriosAdyacentes.Agregar(kamchatka);
            irkutsk.TerritoriosAdyacentes.Agregar(mongolia);

            mongolia.TerritoriosAdyacentes.Agregar(siberia);
            mongolia.TerritoriosAdyacentes.Agregar(irkutsk);
            mongolia.TerritoriosAdyacentes.Agregar(kamchatka);
            mongolia.TerritoriosAdyacentes.Agregar(japon);
            mongolia.TerritoriosAdyacentes.Agregar(china);

            china.TerritoriosAdyacentes.Agregar(mongolia);
            china.TerritoriosAdyacentes.Agregar(siberia);
            china.TerritoriosAdyacentes.Agregar(ural);
            china.TerritoriosAdyacentes.Agregar(afganistan);
            china.TerritoriosAdyacentes.Agregar(india);
            china.TerritoriosAdyacentes.Agregar(siam);

            india.TerritoriosAdyacentes.Agregar(afganistan);
            india.TerritoriosAdyacentes.Agregar(orienteMedio);
            india.TerritoriosAdyacentes.Agregar(china);
            india.TerritoriosAdyacentes.Agregar(siam);

            siam.TerritoriosAdyacentes.Agregar(india);
            siam.TerritoriosAdyacentes.Agregar(china);
            siam.TerritoriosAdyacentes.Agregar(indonesia);

            // Oceanía
            indonesia.TerritoriosAdyacentes.Agregar(siam);
            indonesia.TerritoriosAdyacentes.Agregar(nuevaGuinea);
            indonesia.TerritoriosAdyacentes.Agregar(australiaOccidental);

            nuevaGuinea.TerritoriosAdyacentes.Agregar(indonesia);
            nuevaGuinea.TerritoriosAdyacentes.Agregar(australiaOriental);
            nuevaGuinea.TerritoriosAdyacentes.Agregar(australiaOccidental);

            australiaOccidental.TerritoriosAdyacentes.Agregar(indonesia);
            australiaOccidental.TerritoriosAdyacentes.Agregar(nuevaGuinea);
            australiaOccidental.TerritoriosAdyacentes.Agregar(australiaOriental);

            australiaOriental.TerritoriosAdyacentes.Agregar(australiaOccidental);
            australiaOriental.TerritoriosAdyacentes.Agregar(nuevaGuinea);


            // ---- PASO 4: AÑADIR LOS CONTINENTES AL MAPA ----
            MapaDelJuego.Continentes.Agregar(americaDelSur);
            MapaDelJuego.Continentes.Agregar(americaDelNorte);
            MapaDelJuego.Continentes.Agregar(europa);
            MapaDelJuego.Continentes.Agregar(africa);
            MapaDelJuego.Continentes.Agregar(asia);
            MapaDelJuego.Continentes.Agregar(oceania);
            
            // ---- PASO 5: DISTRIBUIR TERRITORIOS ALEATORIAMENTE [cite: 34] ----
            // Primero, necesitamos una lista con TODOS los territorios del mapa.
            ListaSimple<Territorio> todosLosTerritorios = new ListaSimple<Territorio>();
            for (int i = 0; i < MapaDelJuego.Continentes.Cantidad; i++)
            {
                Continente continente = MapaDelJuego.Continentes.Obtener(i);
                for (int j = 0; j < continente.Territorios.Cantidad; j++)
                {
                    todosLosTerritorios.Agregar(continente.Territorios.Obtener(j));
                }
            }

            Random generadorAleatorio = new Random();
            int indiceJugadorActual = 0;
            while (todosLosTerritorios.Cantidad > 0)
            {
                int indiceTerritorioAleatorio = generadorAleatorio.Next(0, todosLosTerritorios.Cantidad);
                Territorio territorioAsignado = todosLosTerritorios.Obtener(indiceTerritorioAleatorio);

                Jugador jugadorActual = Jugadores.Obtener(indiceJugadorActual);

                territorioAsignado.Propietario = jugadorActual;
                jugadorActual.Territorios.Agregar(territorioAsignado);

                territorioAsignado.Tropas = 1;

                todosLosTerritorios.Remover(indiceTerritorioAleatorio);

                // Pasamos al siguiente jugador en la ronda (0, 1, 2, 0, 1, 2, ...)
                indiceJugadorActual = (indiceJugadorActual + 1) % Jugadores.Cantidad;
            }

        }

        private int TropasInicialesParaColocar(Jugador jugador)
        {
            // Son 40 tropas menos las que ya se pusieron en la distribución
            return 40 - jugador.Territorios.Cantidad;
        }

        // Método principal para que un jugador coloque una tropa
        public void ColocarTropaInicial(Jugador jugador, Territorio territorio)
        {
            // Validaciones
            if (FaseActual != FaseDeJuego.ColocacionInicial) return; // Solo funciona en esta fase
            if (jugador != Jugadores.Obtener(IndiceJugadorColocandoTropas)) return; // No es el turno de este jugador
            if (territorio.Propietario != jugador) return; // El territorio no le pertenece

            // Contamos cuantas tropas ya ha colocado el jugador en esta fase
            int tropasYaColocadas = 0;
            for (int i = 0; i < jugador.Territorios.Cantidad; i++)
            {
                // Sumamos las tropas "extra" (mayores a 1)
                tropasYaColocadas += jugador.Territorios.Obtener(i).Tropas - 1;
            }

            if (tropasYaColocadas >= TropasInicialesParaColocar(jugador)) return; // Ya no le quedan tropas

            // Si todo es válido, añade la tropa
            territorio.Tropas++;

            // Pasa el turno al siguiente jugador
            IndiceJugadorColocandoTropas = (IndiceJugadorColocandoTropas + 1) % Jugadores.Cantidad;

            // Si le toca al ejército neutral, que coloque su tropa aleatoriamente de inmediato
            Jugador siguienteJugador = Jugadores.Obtener(IndiceJugadorColocandoTropas);
            if (siguienteJugador.Alias == "Neutral")
            {
                ColocarTropaNeutralAleatoria();
            }

            if (HanTerminadoDeColocarTodos())
            {
                FaseActual = FaseDeJuego.Jugando;
                // El turno pasa al primer jugador humano (índice 0)
                IniciarTurno(Jugadores.Obtener(0));
            }
        }

        private void ColocarTropaNeutralAleatoria()
        {
            Jugador neutral = BuscarJugador("Neutral");
            if (neutral == null) return;

            int tropasYaColocadas = 0;
            for (int i = 0; i < neutral.Territorios.Cantidad; i++)
            {
                tropasYaColocadas += neutral.Territorios.Obtener(i).Tropas - 1;
            }

            if (tropasYaColocadas < TropasInicialesParaColocar(neutral))
            {
                // Elige un territorio al azar de los suyos y le pone una tropa
                int indiceTerritorioAzar = azar.Next(0, neutral.Territorios.Cantidad);
                Territorio territorioAzar = neutral.Territorios.Obtener(indiceTerritorioAzar);
                territorioAzar.Tropas++;
            }

            // Pasa el turno al siguiente jugador humano
            IndiceJugadorColocandoTropas = (IndiceJugadorColocandoTropas + 1) % Jugadores.Cantidad;
        }

        private Jugador BuscarJugador(string alias)
        {
            for (int i = 0; i < Jugadores.Cantidad; i++)
            {
                if (Jugadores.Obtener(i).Alias == alias)
                    return Jugadores.Obtener(i);
            }
            return null;
        }

        public void IniciarTurno(Jugador jugador)
        {
            // Si la partida acaba de empezar, cambiamos el estado a 'Jugando'.
            if (FaseActual == FaseDeJuego.ColocacionInicial)
            {
                FaseActual = FaseDeJuego.Jugando;
            }

            JugadorEnTurno = jugador;
            planeacionUsadaEsteTurno = false;

            int refuerzosExtra = 0; 

            while (JugadorEnTurno.Tarjetas.Cantidad >= 6)
            {
                int inf = 0;
                int cab = 0;
                int art = 0;

                int i = 0;
                while (i < JugadorEnTurno.Tarjetas.Cantidad)
                {
                    Tarjeta t = JugadorEnTurno.Tarjetas.Obtener(i);
                    if (t.Tipo == TipoTarjeta.Infanteria) inf = inf + 1;
                    if (t.Tipo == TipoTarjeta.Caballeria) cab = cab + 1;
                    if (t.Tipo == TipoTarjeta.Artilleria) art = art + 1;
                    i = i + 1;
                }

                bool canjeado = false;

                // 3 iguales
                if (inf >= 3)
                {
                    int removidas = 0; i = 0;
                    while (i < JugadorEnTurno.Tarjetas.Cantidad && removidas < 3)
                    {
                        if (JugadorEnTurno.Tarjetas.Obtener(i).Tipo == TipoTarjeta.Infanteria)
                        {
                            JugadorEnTurno.Tarjetas.Remover(i);
                            removidas = removidas + 1;
                        }
                        else { i = i + 1; }
                    }
                    canjeado = true;
                }
                else if (cab >= 3)
                {
                    int removidas = 0; i = 0;
                    while (i < JugadorEnTurno.Tarjetas.Cantidad && removidas < 3)
                    {
                        if (JugadorEnTurno.Tarjetas.Obtener(i).Tipo == TipoTarjeta.Caballeria)
                        {
                            JugadorEnTurno.Tarjetas.Remover(i);
                            removidas = removidas + 1;
                        }
                        else { i = i + 1; }
                    }
                    canjeado = true;
                }
                else if (art >= 3)
                {
                    int removidas = 0; i = 0;
                    while (i < JugadorEnTurno.Tarjetas.Cantidad && removidas < 3)
                    {
                        if (JugadorEnTurno.Tarjetas.Obtener(i).Tipo == TipoTarjeta.Artilleria)
                        {
                            JugadorEnTurno.Tarjetas.Remover(i);
                            removidas = removidas + 1;
                        }
                        else { i = i + 1; }
                    }
                    canjeado = true;
                }
                else if (inf >= 1 && cab >= 1 && art >= 1)
                {
                    // 1 de cada tipo
                    i = 0;
                    bool q1 = false, q2 = false, q3 = false;
                    while (i < JugadorEnTurno.Tarjetas.Cantidad && (q1 == false || q2 == false || q3 == false))
                    {
                        TipoTarjeta tt = JugadorEnTurno.Tarjetas.Obtener(i).Tipo;
                        if (q1 == false && tt == TipoTarjeta.Infanteria) { JugadorEnTurno.Tarjetas.Remover(i); q1 = true; }
                        else if (q2 == false && tt == TipoTarjeta.Caballeria) { JugadorEnTurno.Tarjetas.Remover(i); q2 = true; }
                        else if (q3 == false && tt == TipoTarjeta.Artilleria) { JugadorEnTurno.Tarjetas.Remover(i); q3 = true; }
                        else { i = i + 1; }
                    }
                    canjeado = true;
                }

                if (canjeado == true)
                {
                    int indice = MapaDelJuego.ContadorIntercambiosGlobal;
                    if (indice < 0) indice = 0;
                    if (indice >= fibonacci.Length) indice = fibonacci.Length - 1;

                    refuerzosExtra = refuerzosExtra + fibonacci[indice];
                    MapaDelJuego.ContadorIntercambiosGlobal = MapaDelJuego.ContadorIntercambiosGlobal + 1;
                }
                else
                {
                    break;
                }
            }

            int refuerzosBase = CalcularRefuerzos(jugador);
            int refuerzos = refuerzosBase + refuerzosExtra;
            // >>> aquí la UI puede usar 'refuerzos' para colocarlos donde el jugador decida <<<


        }

        private int CalcularRefuerzos(Jugador jugador)
        {
            int refuerzosTotales = 0;

            refuerzosTotales = jugador.Territorios.Cantidad / 3;

            for (int i = 0; i < MapaDelJuego.Continentes.Cantidad; i++)
            {
                Continente continente = MapaDelJuego.Continentes.Obtener(i);
                bool controlaContinente = true;

                for (int j = 0; j < continente.Territorios.Cantidad; j++)
                {
                    Territorio territorio = continente.Territorios.Obtener(j);
                    if (territorio.Propietario != jugador)
                    {
                        controlaContinente = false;
                        break;
                    }
                }

                if (controlaContinente)
                {
                    refuerzosTotales += continente.BonusRefuerzo;
                }
            }

            return refuerzosTotales;
        }

        public void ResolverAtaque(Territorio territorioAtacante, Territorio territorioDefensor, int tropasParaAtaque)
        {
            if (FaseActual != FaseDeJuego.Jugando) return;
            if (territorioAtacante.Propietario != JugadorEnTurno) return;
            if (territorioAtacante.Propietario == territorioDefensor.Propietario) return;
            if (territorioAtacante.Tropas < 2) return;

            // Validar adyacencia (simple)
            bool ady = false;
            int k = 0;
            while (k < territorioAtacante.TerritoriosAdyacentes.Cantidad)
            {
                if (territorioAtacante.TerritoriosAdyacentes.Obtener(k) == territorioDefensor)
                {
                    ady = true; break;
                }
                k = k + 1;
            }
            if (ady == false) return;

            // Limitar dados del atacante a 1..3 y a las tropas disponibles (dejando 1)
            int dadosAtk = tropasParaAtaque;
            if (dadosAtk < 1) dadosAtk = 1;
            int maxAtk = territorioAtacante.Tropas - 1;
            if (maxAtk > 3) maxAtk = 3;
            if (dadosAtk > maxAtk) dadosAtk = maxAtk;

            // Dados del defensor: la UI puede poner 1 o 2 en 'dadosDefensor' (campo público)
            int dadosDef = dadosDefensor;
            if (dadosDef < 1) dadosDef = 1;
            if (dadosDef > 2) dadosDef = 2;
            if (dadosDef > territorioDefensor.Tropas) dadosDef = territorioDefensor.Tropas;

            // Tiradas
            ListaSimple<int> resultadosAtacante = new ListaSimple<int>();
            ListaSimple<int> resultadosDefensor = new ListaSimple<int>();

            int i = 0;
            while (i < dadosAtk)
            {
                resultadosAtacante.Agregar(azar.Next(1, 7));
                i = i + 1;
            }

            i = 0;
            while (i < dadosDef)
            {
                resultadosDefensor.Agregar(azar.Next(1, 7));
                i = i + 1;
            }

            // Ordenar de mayor a menor (tu función actual)
            OrdenarDados(resultadosAtacante);
            OrdenarDados(resultadosDefensor);

            // Comparar por pares
            int comparaciones = resultadosAtacante.Cantidad < resultadosDefensor.Cantidad ? resultadosAtacante.Cantidad : resultadosDefensor.Cantidad;

            i = 0;
            while (i < comparaciones)
            {
                int a = resultadosAtacante.Obtener(i);
                int d = resultadosDefensor.Obtener(i);

                if (a > d) { territorioDefensor.Tropas = territorioDefensor.Tropas - 1; }
                else { territorioAtacante.Tropas = territorioAtacante.Tropas - 1; }

                i = i + 1;
            }

            // Conquista: mover >= dadosAtk
            if (territorioDefensor.Tropas <= 0)
            {
                ConquistarTerritorio(territorioAtacante, territorioDefensor, dadosAtk);
            }
        }

        private void OrdenarDados(ListaSimple<int> lista)
        {
            // Implementación de Bubble Sort (de mayor a menor)
            for (int i = 0; i < lista.Cantidad - 1; i++)
            {
                for (int j = 0; j < lista.Cantidad - i - 1; j++)
                {
                    if (lista.Obtener(j) < lista.Obtener(j + 1))
                    {
                        // Si el elemento actual es más pequeño que el siguiente, los intercambiamos.
                        int temp = lista.Obtener(j);
                        lista.Set(j, lista.Obtener(j + 1));
                        lista.Set(j + 1, temp);
                    }
                }
            }
        }

        private void ConquistarTerritorio(Territorio territorioAtacante, Territorio territorioDefensor, int tropasDelAtaque)
        {
            Jugador defensor = territorioDefensor.Propietario;
            Jugador atacante = territorioAtacante.Propietario;

            // 1. Quitar el territorio del defensor (búsqueda simple)
            if (defensor != null)
            {
                int i = 0;
                while (i < defensor.Territorios.Cantidad)
                {
                    if (defensor.Territorios.Obtener(i) == territorioDefensor)
                    {
                        defensor.Territorios.Remover(i);
                        break;
                    }
                    i = i + 1;
                }
            }

            // 2. Cambiar propietario
            territorioDefensor.Propietario = atacante;

            // 3. Añadir el territorio al atacante
            atacante.Territorios.Agregar(territorioDefensor);

            // 4. Mover tropas: al menos los dados usados, dejando 1 en origen
            int maximoMover = territorioAtacante.Tropas - 1;
            int minimoMover = tropasDelAtaque;
            if (minimoMover > maximoMover) minimoMover = maximoMover;

            territorioAtacante.Tropas = territorioAtacante.Tropas - minimoMover;
            territorioDefensor.Tropas = minimoMover;

            // 5. Dar una tarjeta aleatoria al atacante
            int r = azar.Next(0, 3);
            TipoTarjeta tipo;
            if (r == 0) tipo = TipoTarjeta.Infanteria;
            else if (r == 1) tipo = TipoTarjeta.Caballeria;
            else tipo = TipoTarjeta.Artilleria;
            atacante.Tarjetas.Agregar(new Tarjeta(tipo));

            VerificarCondicionDeVictoria();
        }

        // Este método usa un algoritmo (Búsqueda en Profundidad - DFS) para ver si hay un camino.
        private bool ExisteRutaAmiga(Territorio origen, Territorio destino, ListaSimple<Territorio> visitados)
        {
            // Marcamos el territorio actual como visitado para no entrar en bucles.
            visitados.Agregar(origen);

            // Revisamos los vecinos del territorio actual.
            for (int i = 0; i < origen.TerritoriosAdyacentes.Cantidad; i++)
            {
                Territorio vecino = origen.TerritoriosAdyacentes.Obtener(i);

                // Si el vecino es el destino, encontramos un camino.
                if (vecino == destino)
                {
                    return true;
                }

                // Si el vecino pertenece al mismo jugador y no lo hemos visitado aún...
                bool vecinoYaVisitado = false;
                for (int j = 0; j < visitados.Cantidad; j++) { if (visitados.Obtener(j) == vecino) vecinoYaVisitado = true; }

                if (vecino.Propietario == origen.Propietario && !vecinoYaVisitado)
                {
                    // ...hacemos una llamada recursiva para seguir buscando desde el vecino.
                    if (ExisteRutaAmiga(vecino, destino, visitados))
                    {
                        return true;
                    }
                }
            }

            // Si exploramos todos los vecinos y no encontramos el destino, no hay ruta desde aquí.
            return false;
        }

        public void MoverTropasPlaneacion(Territorio origen, Territorio destino, int cantidad)
        {
            // --- VALIDACIONES ---
            if (origen.Propietario != JugadorEnTurno) return; // El territorio origen debe ser del jugador en turno.
            if (origen.Propietario != destino.Propietario) return; // Ambos territorios deben ser del mismo jugador.
            if (cantidad >= origen.Tropas) return; // Debe quedar al menos una tropa en el origen
            if (planeacionUsadaEsteTurno == true) return;

            // Verificamos si existe una ruta de territorios amigos entre el origen y el destino.
            if (!ExisteRutaAmiga(origen, destino, new ListaSimple<Territorio>())) return;

            // --- EJECUCIÓN DEL MOVIMIENTO ---
            origen.Tropas -= cantidad;
            destino.Tropas += cantidad;

        }

        public void FinalizarTurno()
        {
            // Buscamos el índice del jugador actual en la lista.
            int indiceActual = -1;
            for (int i = 0; i < Jugadores.Cantidad; i++)
            {
                if (Jugadores.Obtener(i) == JugadorEnTurno)
                {
                    indiceActual = i;
                    break;
                }
            }

            // Avanzamos al siguiente jugador, saltándonos al ejército neutral.
            int indiceSiguiente = (indiceActual + 1) % Jugadores.Cantidad;
            while (Jugadores.Obtener(indiceSiguiente).Alias == "Neutral")
            {
                indiceSiguiente = (indiceSiguiente + 1) % Jugadores.Cantidad;
            }

            Jugador siguienteJugador = Jugadores.Obtener(indiceSiguiente);

            // Iniciamos el turno para el siguiente jugador.
            IniciarTurno(siguienteJugador);
        }

        private void VerificarCondicionDeVictoria()
        {
            // El total de territorios en el mapa es 42.
            int totalTerritoriosEnMapa = 42;

            // Recorremos la lista de jugadores.
            for (int i = 0; i < Jugadores.Cantidad; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);
                // Si la cantidad de territorios de un jugador es igual al total, ha ganado.
                if (jugador.Territorios.Cantidad == totalTerritoriosEnMapa)
                {
                    FaseActual = FaseDeJuego.Terminado;
                    // El jugador en turno es el ganador.
                    JugadorEnTurno = jugador;
                    // En la UI, cuando veamos que la fase es 'Terminado', mostraremos un mensaje de victoria.
                }
            }
        }

        private bool HanTerminadoDeColocarTodos()
        {
            for (int i = 0; i < Jugadores.Cantidad; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);
                int tropasQueDebeTener = 40; // Cada jugador empieza con 40 tropas

                int tropasActuales = 0;
                for (int j = 0; j < jugador.Territorios.Cantidad; j++)
                {
                    tropasActuales += jugador.Territorios.Obtener(j).Tropas;
                }

                if (tropasActuales < tropasQueDebeTener)
                {
                    return false; // Si encontramos UN SOLO jugador que aún no ha puesto todas, la fase no ha terminado.
                }
            }
            return true; // Si el bucle termina, es porque todos han puesto sus 40 tropas.
        }

    }
}