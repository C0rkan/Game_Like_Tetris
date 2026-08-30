using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform[,] grid;
    public int width;
    public int height;

    void Start()
    {
        grid = new Transform[width, height];
    }


    void Update()
    {
        
    }
}
