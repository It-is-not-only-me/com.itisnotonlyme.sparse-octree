using System;
using System.Collections;
using UnityEngine;

namespace ItIsNotOnlyMe.SparseOctree
{
    public class Octree<TTipo> where TTipo : IComparable
    {
        private const int _profunidadBase = 0;

        private Nodo<TTipo> _raiz;
        private int _profunidad;

        public Octree(Vector3 posicion, Vector3 dimensiones, int profundidad)
        {
            _profunidad = profundidad;
            _raiz = new Nodo<TTipo>(posicion, dimensiones, _profunidadBase);
        }

        public bool Insertar(Vector3 posicion, TTipo valor)
        {
            return _raiz.Insertar(posicion, valor, _profunidad);
        }

        public bool Eliminar(Vector3 posicion)
        {
            return _raiz.Eliminar(posicion, _profunidad);
        }

        public void Clear()
        {
            _raiz.Clear();
        }

        public void Visitar(IVisitor<TTipo> visitor)
        {
            _raiz.Visitar(visitor);
        }
    }
}
