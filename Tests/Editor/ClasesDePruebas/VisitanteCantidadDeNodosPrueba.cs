﻿using ItIsNotOnlyMe.SparseOctree;

public class VisitanteCantidadDeNodosPrueba : IVisitor<PuntoPrueba>
{
    public int CantidadDeNodos => _cantidadDeNodos;

    private int _cantidadDeNodos = 0;

    public void Visitar(Nodo<PuntoPrueba> nodo)
    {
        _cantidadDeNodos++;
    }
}
