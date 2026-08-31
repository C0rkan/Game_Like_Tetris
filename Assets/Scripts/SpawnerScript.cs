using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerScript : MonoBehaviour
{

    [Header("Blocks")]
    public GameObject[] blocks;
    private GameObject selectedBlock;
    private int selectedBlockIndex;

    [Header("Block Spawn")]
    public bool canSpawnBlock = true;
    public Transform spawnLocation;


    [Header("Block Selecting")]
    public GameObject currentBlock;
    public GameObject nextBlock = null;
    public GameObject holdedBlock = null;
    public bool anyBlockHolded = false;

    void Start() {

        if (canSpawnBlock && currentBlock == null) {
            SpawnBlock(spawnLocation);
        }
    }

    public void SpawnBlock(Transform spawnLocation) {
        Randomizer();
        currentBlock = Instantiate(selectedBlock, spawnLocation.position, Quaternion.identity);
        selectedBlock = null;

        Randomizer();
        nextBlock = selectedBlock;
        selectedBlock = null;

        canSpawnBlock = false;
    }

    private void Randomizer() {
        selectedBlockIndex = UnityEngine.Random.Range(0, blocks.Length);
        selectedBlock = blocks[selectedBlockIndex];
    }
}
