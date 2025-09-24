using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyRisk.Logic
{
    public class Mapa
    {
        // El mapa contiene una lista de todos los continentes.
        public ListaSimple<Continente> Continentes { get; set; }

        public int ContadorIntercambiosGlobal { get; set; }

        public Mapa()
        {
            Continentes = new ListaSimple<Continente>();
            ContadorIntercambiosGlobal = 0; // Inicia en 0.
        }
    }
}