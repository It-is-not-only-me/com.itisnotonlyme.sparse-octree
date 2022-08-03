using System;
using UnityEngine;

public class Punto : MonoBehaviour, IComparable
{
    [SerializeField] private float _valor = 0;

    public int CompareTo(object obj)
    {
        Punto otro = obj as Punto;
        if (otro._valor == _valor)
            return 0;
        return otro._valor > _valor ? 1 : -1;
    }
}
