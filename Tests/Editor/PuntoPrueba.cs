using System;

public class PuntoPrueba : IComparable
{
    public int ValorActual => _valor;

    private int _valor = 0;

    public int CompareTo(object obj)
    {
        PuntoPrueba punto = obj as PuntoPrueba;
        if (punto._valor == _valor)
            return 0;
        return _valor > punto._valor ? 1 : -1;
    }
}
