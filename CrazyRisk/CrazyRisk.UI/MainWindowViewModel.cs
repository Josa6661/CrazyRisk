using CrazyRisk.Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CrazyRisk.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Territorio? territorioSeleccionado;
        public Juego Juego { get; set; }
        public ListaSimple<Territorio> TodosLosTerritorios { get; private set; }

        public string FaseActualDelJuego => $"Fase: {Juego.FaseActual}";
        public string TurnoActual
        {
            get
            {
                if (Juego.FaseActual == FaseDeJuego.ColocacionInicial)
                {
                    Jugador jugadorActual = Juego.Jugadores.Obtener(Juego.IndiceJugadorColocandoTropas);
                    return $"Turno de: {jugadorActual.Alias}";
                }
                return $"Turno de: {Juego.JugadorEnTurno?.Alias}";
            }
        }

        public ICommand TerritorioClickedCommand { get; } // Renombrado
        public ICommand FinalizarTurnoCommand { get; }

        public MainWindowViewModel()
        {
            Juego = new Juego();
            Juego.PrepararPartida();

            TodosLosTerritorios = new ListaSimple<Territorio>();
            for (int i = 0; i < Juego.MapaDelJuego.Continentes.Cantidad; i++)
            {
                var continente = Juego.MapaDelJuego.Continentes.Obtener(i);
                for (int j = 0; j < continente.Territorios.Cantidad; j++)
                {
                    TodosLosTerritorios.Agregar(continente.Territorios.Obtener(j));
                }
            }

            TerritorioClickedCommand = new RelayCommand(OnTerritorioClicked);
            FinalizarTurnoCommand = new RelayCommand(OnFinalizarTurno); ;
        }

        private void OnTerritorioClicked(object? territorioObj)
        {
            if (territorioObj is not Territorio terr) return;

            if (Juego.FaseActual == FaseDeJuego.ColocacionInicial)
            {
                // Lógica de colocación (la que ya teníamos)
                Jugador jugadorActual = Juego.Jugadores.Obtener(Juego.IndiceJugadorColocandoTropas);
                Juego.ColocarTropaInicial(jugadorActual, terr);
                OnPropertyChanged(nameof(TurnoActual));
                OnPropertyChanged(nameof(FaseActualDelJuego));
            }
            else if (Juego.FaseActual == FaseDeJuego.Jugando)
            {
                // Lógica de ATAQUE
                if (territorioSeleccionado == null)
                {
                    // PRIMER CLIC: Seleccionar origen
                    // Verificamos si el territorio pertenece al jugador en turno
                    if (terr.Propietario == Juego.JugadorEnTurno)
                    {
                        territorioSeleccionado = terr;
                        // (Opcional: podrías añadir lógica visual para resaltar el territorio)
                    }
                }
                else
                {
                    // SEGUNDO CLIC: Seleccionar destino y atacar
                    // Atacamos con todas las tropas posibles (dejando una atrás)
                    int tropasParaAtaque = territorioSeleccionado.Tropas - 1;
                    if (tropasParaAtaque > 0)
                    {
                        Juego.ResolverAtaque(territorioSeleccionado, terr, tropasParaAtaque);
                    }

                    // Limpiamos la selección para el próximo ataque
                    territorioSeleccionado = null;
                }
            }
        }

        private void OnFinalizarTurno(object? parametro)
        {
            Juego.FinalizarTurno();
            // Notificamos a la UI que el turno ha cambiado
            OnPropertyChanged(nameof(TurnoActual));
        }

        // --- Lógica para las notificaciones ---
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}