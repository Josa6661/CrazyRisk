using System;
using System.Collections;
using System.Collections.Generic;

namespace CrazyRisk.Logic
{
    public class ListaSimple<T> : IEnumerable<T>
    {
        private T[] items;
        private int contador;

        public ListaSimple()
        {
            items = new T[4];
            contador = 0;
        }

        public int Cantidad => contador;

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
                throw new IndexOutOfRangeException("El índice está fuera del rango de la lista.");
            return items[indice];
        }

        public void Remover(int indice)
        {
            if (indice < 0 || indice >= contador)
                throw new IndexOutOfRangeException("El índice está fuera del rango de la lista.");

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
                items[indice] = item;
        }

        // Busca un elemento y devuelve su índice, o -1 si no lo encuentra.
        public int BuscarIndiceDe(T itemBuscado)
        {
            for (int i = 0; i < contador; i++)
            {

                if ((object)items[i] == (object)itemBuscado)
                {
                    return i;
                }
            }
            return -1;
        }

        // Elimina un elemento específico de la lista.
        public bool RemoverElemento(T itemBuscado)
        {
            int indice = BuscarIndiceDe(itemBuscado);
            if (indice >= 0)
            {
                Remover(indice);
                return true;
            }
            return false;
        }

        // Verifica si un elemento existe en la lista.
        public bool ContieneElemento(T itemBuscado)
        {
            return BuscarIndiceDe(itemBuscado) >= 0;
        }

        // --- MÉTODOS PARA COMPATIBILIDAD CON WPF ---
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < contador; i++)
                yield return items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}