using System;

namespace ItIsNotOnlyMe.SparseOctree
{
    public interface IVisitor<TTipo> where TTipo : IComparable
    {
        public void Visitar(Nodo<TTipo> nodo);
    }
}
