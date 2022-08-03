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

        public bool Insertar(Vector3 posicion, TTipo valor, int profunidadMaxima)
        {
            if (!PuntoContenido(posicion))
                return false;

            if (profunidadMaxima >= _profunidad)
            {
                _valor = valor;
                return true;
            }

            if (EstaSubdividido() && !HayVariacion(valor))
            {
                _valor = valor;
                Juntar();
                return true;
            }

            if (!EstaSubdividido())
                Subdividir();

            Nodo<TTipo> nodoInsertar = NodoParaPosicion(posicion);
            return nodoInsertar.Insertar(posicion, valor, profunidadMaxima);
        }

        public bool Eliminar(Vector3 posicion, int profundiadMaxima)
        {
            if (!PuntoContenido(posicion))
                return false;

            if (profundiadMaxima >= _profunidad)
            {
                _valor = _valorBase;
                return true;
            }

            if (TieneMismoValor(_valorBase))
                return true;
            
            if (!EstaSubdividido())
                Subdividir();
            
            Nodo<TTipo> nodoParaEliminar = NodoParaPosicion(posicion);
            bool resultado = nodoParaEliminar.Eliminar(posicion, profundiadMaxima);

            if (!HayVariacion(_valorBase))
                Juntar();

            return resultado;
        }

        public void Clear()
        {
            Juntar();
            _valor = _valorBase;
        }

        public void Visitar(IVisitor<TTipo> visitor)
        {
            visitor.Visitar(this);
            foreach (Nodo<TTipo> nodo in _hijos)
                nodo.Visitar(visitor);
        }

        private Nodo<TTipo> NodoParaPosicion(Vector3 posicion)
        {
            int indice = 0;

            for (int i = 2, potencia = 1; i >= 0; i++, potencia *= 2)
                if (_posicion[i] + _dimensiones[i] / 2 < posicion[i])
                    indice += potencia;

            return _hijos[indice];
        }

        private void Subdividir()
        {
            Vector3 nuevaDimensiones = _dimensiones / 2;
            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 1; j++)
                    for (int k = 0; k <= 1; k++)
                    {
                        Vector3 nuevaPosicion = new Vector3 (
                            _posicion.x + _dimensiones.x * i,
                            _posicion.y + _dimensiones.y * j,
                            _posicion.z + _dimensiones.z * k
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

        private void Juntar()
        {
            _hijos.Clear();
        }

        private bool HayVariacion(TTipo valor)
        {
            return !_hijos.TrueForAll(nodo => TieneMismoValor(valor));
        }

        private bool TieneMismoValor(TTipo valor)
        {
            return EstaSubdividido() ? false : _valor.CompareTo(valor) == 0;
        }

        private bool PuntoContenido(Vector3 posicion)
        {
            bool contenido = true;
            for (int i = 0; i < 3; i++)
                contenido &= _posicion[i] < posicion[i] && _posicion[i] + _dimensiones[i]  > posicion[i];
            return contenido;
        }
    }
}
