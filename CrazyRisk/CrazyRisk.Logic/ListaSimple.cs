using System;
using System.Collections; // Necesario para IEnumerable
using System.Collections.Generic; // Necesario para IEnumerable<T>

namespace CrazyRisk.Logic
{
    // Ahora nuestra lista implementa la interfaz IEnumerable<T>
    public class ListaSimple<T> : IEnumerable<T>
    {
        private T[] items;
        private int contador;

        public ListaSimple()
        {
            items = new T[4];
            contador = 0;
        }

        public int Cantidad
        {
            get { return contador; }
        }

        public void Agregar(T nuevoItem)
        {
            if (contador == items.Length)
            {
                T[] nuevoArreglo = new T[items.Length * 2];
                for (int i = 0; i < items.Length; i++)
                {
                    nuevoArreglo[i] = items[i];
                }
                items = nuevoArreglo;
            }
            items[contador] = nuevoItem;
            contador++;
        }

        public T Obtener(int indice)
        {
            if (indice < 0 || indice >= contador)
            {
                throw new IndexOutOfRangeException("El índice está fuera del rango de la lista.");
            }
            return items[indice];
        }

        public void Remover(int indice)
        {
            if (indice < 0 || indice >= contador)
            {
                throw new IndexOutOfRangeException("El índice está fuera del rango de la lista.");
            }

            for (int i = indice; i < contador - 1; i++)
            {
                items[i] = items[i + 1];
            }
            contador--;
            items[contador] = default(T);
        }

        public void Set(int indice, T item)
        {
            if (indice >= 0 && indice < contador)
            {
                items[indice] = item;
            }
        }

        // --- MÉTODOS NUEVOS REQUERIDOS POR IEnumerable ---
        // Este método le permite a WPF recorrer nuestra lista con un bucle (foreach).
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < contador; i++)
            {
                // La palabra clave "yield return" devuelve los elementos uno por uno.
                yield return items[i];
            }
        }

        // Versión no genérica del método anterior (también requerida).
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}