using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [Header("Blocks")]
    public GameObject[] blocks;
    public GameObject selectedBlock;

    [Header("Block Spawn")]
    [SerializeField] private bool canSpawnBlock = true;
    [SerializeField] private Transform spawnLocation;


    [Header("Block Selecting")]
    private GameObject currentBlock;
    private GameObject nextBlock;
    private GameObject hidedBlock;

    void Start() {


        if (canSpawnBlock) {
            SpawnBlock(spawnLocation,selectedBlock);
        }
    }

    private void SpawnBlock(Transform spawnLocation, GameObject selectedBlock) {
        int selectBlockIndex = UnityEngine.Random.Range(0, blocks.Length);
        selectedBlock = blocks[selectBlockIndex];
        Instantiate(selectedBlock, spawnLocation);
        canSpawnBlock = false;
    }
}
