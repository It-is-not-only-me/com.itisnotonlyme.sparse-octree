using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.SparseOctree
{
    public class Nodo<TTipo> where TTipo : IComparable
    {
        private static TTipo _valorBase = default(TTipo);

        private Vector3 _posicion, _dimensiones;
        private int _profunidad;
        
        private List<Nodo<TTipo>> _hijos;
        private TTipo _valor;

        public Nodo(Vector3 posicion, Vector3 dimensiones, int profundiad, TTipo valor = default(TTipo))
        {
            _posicion = posicion;
            _dimensiones = dimensiones;
            _profunidad = profundiad;

            _hijos = new List<Nodo<TTipo>>();
            _valor = valor;
        }

        public Bounds Region => new Bounds(_posicion + _dimensiones / 2, _dimensiones);

        public int Profundidad => _profunidad;

        public TTipo Valor => _valor;

        private bool EstaSubdividido() => _hijos.Count > 0;

        public bool Insertar(Vector3 posicion, TTipo valor, int profundidadMaxima)
        {
            if (!PuntoContenido(posicion))
                return false;

            if (_profunidad >= profundidadMaxima)
            {
                _valor = valor;
                return true;
            }

            if (!EstaSubdividido() || !SonIguales(valor))
            {
                if (!EstaSubdividido())
                    Subdividir();

                Nodo<TTipo> nodoInsertar = NodoParaPosicion(posicion);
                nodoInsertar.Insertar(posicion, valor, profundidadMaxima);
            }

            if (SonIguales(valor))
                Juntar(valor);            

            return true;
        }

        public bool Eliminar(Vector3 posicion, int profundidadMaxima)
        {
            return Insertar(posicion, _valorBase, profundidadMaxima);
        }

        public void Clear()
        {
            Juntar(_valorBase);
        }

        public void Visitar(IVisitor<TTipo> visitor)
        {
            visitor.Visitar(this);
            foreach (Nodo<TTipo> nodo in _hijos)
                nodo.Visitar(visitor);
        }

        private Nodo<TTipo> NodoParaPosicion(Vector3 posicion)
        {
            int indice = IndiceParaPosicion(posicion);
            return _hijos[indice];
        }

        private int IndiceParaPosicion(Vector3 posicion)
        {
            int indice = 0;

            for (int i = 2, potencia = 1; i >= 0; i--, potencia *= 2)
                if (_posicion[i] + _dimensiones[i] / 2 <= posicion[i])
                    indice += potencia;

            return indice;
        }

        private void Subdividir()
        {
            Vector3 nuevaDimensiones = _dimensiones / 2;
            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 1; j++)
                    for (int k = 0; k <= 1; k++)
                    {
                        Vector3 nuevaPosicion = new Vector3 (
                            _posicion.x + nuevaDimensiones.x * i,
                            _posicion.y + nuevaDimensiones.y * j,
                            _posicion.z + nuevaDimensiones.z * k
                        );

                        Nodo<TTipo> nuevoNodo = new Nodo<TTipo> (
                            nuevaPosicion, 
                            nuevaDimensiones, 
                            _profunidad + 1, 
                            _valor
                        );
                        _hijos.Add(nuevoNodo);
                    }
        }

        private void Juntar(TTipo valor = default(TTipo))
        {
            _hijos.Clear();
            _valor = valor;
        }

        private bool SonIguales(TTipo valor = default(TTipo))
        {
            return _hijos.TrueForAll(nodo => nodo.TieneMismoValor(valor));
        }

        private bool TieneMismoValor(TTipo valor)
        {
            if (EstaSubdividido())
                return false;

            if (_valor == null)
                return valor == null;

            return _valor.CompareTo(valor) == 0;
        }

        private bool PuntoContenido(Vector3 posicion)
        {
            bool contenido = true;
            for (int i = 0; i < 3; i++)
                contenido &= _posicion[i] <= posicion[i] && _posicion[i] + _dimensiones[i] >= posicion[i];
            return contenido;
        }
    }
}
