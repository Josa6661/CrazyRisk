using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyRisk.Logic
{
    public class Continente
    {
        public string Nombre { get; set; }
        public int BonusRefuerzo { get; set; }

        // Un continente tiene una lista de los territorios que lo componen.
        public ListaSimple<Territorio> Territorios { get; set; }

        public Continente(string nombre, int bonus)
        {
            Nombre = nombre;
            BonusRefuerzo = bonus;
            Territorios = new ListaSimple<Territorio>();
        }
    }
}