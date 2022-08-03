using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.SparseOctree;

public class VisitantePrueba : IVisitor<PuntoPrueba>
{
    public struct DatosPrueba
    {
        public Bounds Limites;
        public int Profundidad;
        public int Valor;
    }

    public List<DatosPrueba> _datos;

    public VisitantePrueba()
    {
        _datos = new List<DatosPrueba>();
    }

    public void Visitar(Nodo<PuntoPrueba> nodo)
    {
        DatosPrueba datosPrueba = new DatosPrueba();

        datosPrueba.Limites = nodo.Region;
        datosPrueba.Profundidad = nodo.Profundidad;
        datosPrueba.Valor = nodo.Valor.ValorActual;

        _datos.Add(datosPrueba);
    }
}

public class OctreeTest
{
    private Vector3 _posicion = Vector3.zero;
    private Vector3 _dimensiones = Vector3.one * 1024;

    [Test]
    public void Test01NoSeInsertaYTieneQueTenerSoloUnaHoja()
    {
        int profundidad = 3;

        Octree<PuntoPrueba> octree = new Octree<PuntoPrueba>(_posicion, _dimensiones, profundidad);


    }

    /*
     * Hacer una prueba donde se inserta, y por lo tanto deberia solo tener valor la hoja, y nada mas
     * Hacer una prueba donde se inserta y se elimina, entonces solo esta la raiz sin valor
     * Hacer una prueba donde se inserta 8 elemento que deberia ir en las 8 hojas, y por lo tanto solo esta la raiz con ese valor 
     */
}
