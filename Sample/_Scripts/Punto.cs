using System;
using UnityEngine;

public class Punto : MonoBehaviour, IComparable
{
    [SerializeField] private float _valor = 0;

    public int CompareTo(object obj)
    {
        if (obj == null)
            return 1;

        Punto otro = obj as Punto;
        if (otro._valor == _valor)
            return 0;
        return _valor > otro._valor ? 1 : -1;
    }
}
