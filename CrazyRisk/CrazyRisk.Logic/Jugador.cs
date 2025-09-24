using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyRisk.Logic
{
    public class Jugador
    {
        public string Alias { get; set; }
        // Podríamos usar un string para el color o una clase más compleja,
        // por ahora un string es suficiente.
        public string Color { get; set; }

        // ¡Aquí usamos nuestra lista! Un jugador tiene una lista de territorios...
        public ListaSimple<Territorio> Territorios { get; set; }

        // ...y una lista de tarjetas.
        public ListaSimple<Tarjeta> Tarjetas { get; set; }

        public Jugador(string alias, string color)
        {
            Alias = alias;
            Color = color;

            // ¡Muy importante! Siempre hay que inicializar las listas en el constructor
            // para que no estén vacías (null) cuando intentemos usarlas.
            Territorios = new ListaSimple<Territorio>();
            Tarjetas = new ListaSimple<Tarjeta>();
        }
    }
}