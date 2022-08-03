using System.Collections.Generic;
using ItIsNotOnlyMe.SparseOctree;
using UnityEngine;

public class ConseguirDimensiones : IVisitor<Punto>
{
    public struct DatosDimensiones
    {
        public Bounds Limites;
        public int Profundidad;
    }

    public List<DatosDimensiones> Datos => _datos;

    private List<DatosDimensiones> _datos;

    public ConseguirDimensiones()
    {
        _datos = new List<DatosDimensiones>();
    }

    public void Visitar(Nodo<Punto> nodo)
    {
        DatosDimensiones datosDimensiones = new DatosDimensiones();
        datosDimensiones.Limites = nodo.Region;
        datosDimensiones.Profundidad = nodo.Profundidad;
        _datos.Add(datosDimensiones);
    }

}
