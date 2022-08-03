using System.Collections.Generic;
using UnityEngine;
using ItIsNotOnlyMe.SparseOctree;

public class VisitanteValoresDeLosNodosPrueba : IVisitor<PuntoPrueba>
{
    public struct DatosPrueba
    {
        public Bounds Limites;
        public int Profundidad;
        public PuntoPrueba Valor;

        public DatosPrueba(Bounds limites, int profundidad, PuntoPrueba valor)
        {
            Limites = limites;
            Profundidad = profundidad;
            Valor = valor;
        }
    }

    public List<DatosPrueba> Datos;

    public VisitanteValoresDeLosNodosPrueba()
    {
        Datos = new List<DatosPrueba>();
    }

    public void Visitar(Nodo<PuntoPrueba> nodo)
    {
        DatosPrueba datoPrueba = new DatosPrueba(
            nodo.Region,
            nodo.Profundidad,
            nodo.Valor
        );
        Datos.Add(datoPrueba);
    }
}
