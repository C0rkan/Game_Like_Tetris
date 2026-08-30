using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [Header("Blocks")]
    public GameObject[] blocks;
    public GameObject selectBlock;

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
        int selectBlockIndex = UnityEngine.Random.Range(0, blocks.Length);
        selectBlock = blocks[selectBlockIndex];
        Instantiate(selectBlock, spawnLocation);
        canSpawnBlock = false;
    }
}
