using System;
using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [Header("Blocks")]
    public GameObject[] blocks;

    [Header("Block Spawn")]
    private bool canSpawnBlock = false;
    [SerializeField] private Transform spawnLocation;


    [Header("Block Selecting")]
    private GameObject currentBlock;
    private GameObject nextBlock;
    private GameObject hidedBlock;

    void Start() {

        spawnLocation.transform.position = new Vector3(5, 18);
        if (canSpawnBlock) {
            SpawnBlock(spawnLocation);
        }
    }

    private void SpawnBlock(Transform spawnLocation) {

    }
}
