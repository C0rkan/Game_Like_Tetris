using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerScript : MonoBehaviour
{
    public SpawnerScript() {
    }


    [Header("Blocks")]
    public GameObject[] blocks;
    private GameObject selectedBlock;
    private int selectedBlockIndex;

    [Header("Block Spawn")]
    public bool canSpawnBlock = true;
    [SerializeField] private Transform spawnLocation;


    [Header("Block Selecting")]
    public GameObject currentBlock = null;
    public GameObject nextBlock = null;
    public GameObject holdedBlock = null;
    public bool anyBlockHolded = false;

    void Start() {

        if (canSpawnBlock && currentBlock == null) {
            SpawnBlock(spawnLocation);
        }
    }

    private void SpawnBlock(Transform spawnLocation) {
        Randomizer();
        Instantiate(selectedBlock, spawnLocation.position,quaternion.identity);
        currentBlock = selectedBlock;
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
