using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerScript : MonoBehaviour {

    [Header("Blocks")]
    public GameObject[] blocks;
    private GameObject selectedBlock;
    private int selectedBlockIndex;

    [Header("Block Spawn")]
    public bool canSpawnBlock = true;
    public Transform spawnLocation;
    public Transform holdBlockPosition;
    public Transform nextBlockPosition;

    [Header("Block Selecting")]
    public GameObject currentBlock;
    public GameObject nextBlock = null;
    public GameObject holdedBlock = null;
    public bool anyBlockHolded = false;


    private void Update() {
        if (canSpawnBlock && currentBlock == null) {
            SpawnBlock(spawnLocation);
        }
        if(nextBlock == null)
            SpawnNextBlock(nextBlockPosition);
    }

    public void SpawnBlock(Transform spawnLocation) {
        if (currentBlock == null) {
            Randomizer();
            currentBlock = Instantiate(selectedBlock, spawnLocation.position, Quaternion.identity);
            selectedBlock = null;
            
        }
        canSpawnBlock = false;
    }

    private void SpawnNextBlock(Transform nextBloakPosition) {
        if (nextBlock == null) {
            Randomizer();
            nextBlock = Instantiate(selectedBlock, nextBlockPosition.position, Quaternion.identity);
            selectedBlock = null;
        }
    }

    private void Randomizer() {
        selectedBlockIndex = UnityEngine.Random.Range(0, blocks.Length);
        selectedBlock = blocks[selectedBlockIndex];
    }
}
