using UnityEngine;

public class GridScript : MonoBehaviour
{

    [Header("Grid Size")]
    [SerializeField] private Transform[,] grids;
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Time")]
    [SerializeField] private float timePassed = 0;

    private void Awake() {
        grids = new Transform[width, height];
    }


    void Update()
    {

        timePassed += Time.deltaTime;

    }
}
