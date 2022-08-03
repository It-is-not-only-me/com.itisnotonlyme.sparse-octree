using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItIsNotOnlyMe.SparseOctree;

public class OctreeBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _dimensiones;
    [SerializeField] private int _profundidad = 0;

    [Space]

    [SerializeField] private List<Punto> _valorAInsertar = new List<Punto>();

    [Space]

    [SerializeField] private bool _regenerarValores = false;
    
    private Octree<Punto> _octree;

    private void Awake()
    {
        _octree = CrearOctree();
        CargarDatos();
    }

    private Octree<Punto> CrearOctree()
    {
        return new Octree<Punto>(transform.position, _dimensiones, _profundidad);
    }

    private void CargarDatos()
    {
        _octree.Clear();
        foreach (Punto punto in _valorAInsertar)
        {
            bool sePudoInsertar = _octree.Insertar(punto.transform.position, punto);

            if (!sePudoInsertar)
                Debug.Log("No se inserto");
        }
    }

    private void OnDrawGizmos()
    {
        if (_octree == null)
            _octree = CrearOctree();

        if (_regenerarValores)
        {
            _regenerarValores = false;
            CargarDatos();
        }

        ConseguirDimensiones visitor = new ConseguirDimensiones();
        _octree.Visitar(visitor);
        
        List<DatosDimensiones> datos = visitor.Datos;
        List<Color> colores = ColoresPorProfunidad();

        Gizmos.color = Color.white;

        foreach (Punto punto in _valorAInsertar)
            Gizmos.DrawSphere(punto.transform.position, 0.1f);

        foreach (DatosDimensiones datosDimension in datos)
        {
            //Gizmos.color = colores[datosDimension.Profundidad];
            Gizmos.DrawWireCube(datosDimension.Limites.center, datosDimension.Limites.size);
        }

        Gizmos.color = Color.white;
    }

    private List<Color> ColoresPorProfunidad()
    {
        List<Color> colores = new List<Color>();

        for (int i = 0; i <= _profundidad; i++)
        {
            float valor = 1 / (i + 1);
            colores.Add(new Color(valor, valor, valor));
        }

        return colores;
    }
}
