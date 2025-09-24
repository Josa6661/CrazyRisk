using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyRisk.Logic
{
    public enum FaseDeJuego
    {
        ColocacionInicial, // Fase para poner las tropas al inicio
        Jugando,           // Turnos normales (refuerzo, ataque, plan)
        Terminado          // El juego ha finalizado
    }
}