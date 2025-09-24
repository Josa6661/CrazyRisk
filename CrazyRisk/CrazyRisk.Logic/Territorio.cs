using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CrazyRisk.Logic
{
    public class Territorio : INotifyPropertyChanged
    {
        private int _tropas;

        public string Nombre { get; set; }
        public Jugador? Propietario { get; set; }
        public int Tropas
        {
            get { return _tropas; }
            set
            {
                _tropas = value;
                OnPropertyChanged(); // Notifica a la UI que el valor de Tropas ha cambiado
            }
        }

        public ListaSimple<Territorio> TerritoriosAdyacentes { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Territorio(string nombre)
        {
            Nombre = nombre;
            Propietario = null;
            TerritoriosAdyacentes = new ListaSimple<Territorio>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}