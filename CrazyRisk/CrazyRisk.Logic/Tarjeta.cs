using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyRisk.Logic
{
    // La clase Tarjeta es muy simple, solo contiene su tipo.
    public class Tarjeta
    {
        public TipoTarjeta Tipo { get; private set; }

        public Tarjeta(TipoTarjeta tipo)
        {
            Tipo = tipo;
        }
    }
}