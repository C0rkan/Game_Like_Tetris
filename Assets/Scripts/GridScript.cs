using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.Assemblies;
using UnityEngine.InputSystem;

public class GridScript : MonoBehaviour
{

    private PlayerInput input;

    [Header("Grid Size")]
    public static Transform[,] grids;
    public static int width = 10;
    public static int height = 20;


    private void Awake() {
        
        grids = new Transform[width, height];

    }
}
