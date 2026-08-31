using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.Assemblies;
using UnityEngine.InputSystem;

public class GridScript : MonoBehaviour
{

    private PlayerInput input;

    [Header("Grid Size")]
    [SerializeField] private Transform[,] grids;
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Time")]
    public float timePassed = 0;

    private void Awake() {
        
        grids = new Transform[width, height];

    }


    void Update()
    {
        
        timePassed += Time.deltaTime;

    }
}
