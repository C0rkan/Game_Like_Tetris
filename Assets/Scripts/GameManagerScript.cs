using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;



public class GameManagerScript : MonoBehaviour
{
    public SpawnerScript spawner { get; private set; }
    private TetrisControls tetrisControls;

    [Header("Gravity")]
    private float fallTimer = 0;
    private float fallTime = .8f;


    private void Awake() {
        tetrisControls = new TetrisControls();
        spawner = FindAnyObjectByType<SpawnerScript>();


        tetrisControls.TetrisPlayer.Rotate.performed += context => RotateBlocks();
        tetrisControls.TetrisPlayer.Hold.performed += context => HoldBlock();
        tetrisControls.TetrisPlayer.Movement.performed += context => MoveBlocks();
    }

    private void Update() {
        fallTimer += Time.deltaTime;
        GraviyForBlocks();
    }

    public bool isPositionAvailable(Transform blockTransform) {

        foreach (Transform child in blockTransform) {

            int roundToX = Mathf.RoundToInt(child.position.x);
            int roundToY = Mathf.RoundToInt(child.position.y);

            if (roundToX < 0 || roundToX >= GridScript.width || roundToY < 0) {
                return false;
            }

            else if (GridScript.grids[roundToX,roundToY] != null ) {
                return false;
            }

        }
        return true;
    }


    private void GraviyForBlocks() {

        if (fallTimer >= fallTime) {

            spawner.currentBlock.transform.position += new Vector3(0,-1,0);

            if (!isPositionAvailable(spawner.currentBlock.transform)) {
                spawner.currentBlock.transform.position += new Vector3(0, 1, 0);
                AddToGrid(spawner.currentBlock.transform);

                spawner.SpawnBlock(spawner.spawnLocation);
            }
            fallTimer = 0;
        }
    }

    private void AddToGrid(Transform blockTransform) {

        foreach (Transform child in blockTransform) {

            int roundToX = Mathf.RoundToInt(child.position.x);
            int roundToY = Mathf.RoundToInt(child.position.y);
            
            if (roundToX >= 0 && roundToX < GridScript.width && roundToY >= 0 && roundToX < GridScript.height) {
                GridScript.grids[roundToX,roundToY] = child;
            }
        }

    }

    private void RotateBlocks() {
        
        if (spawner.currentBlock != null) {
            int rotationScale = 90;
            spawner.currentBlock.transform.Rotate(0, 0, rotationScale);
        }
    }


    private void MoveBlocks() {

    }

    private void MoveRight() {
        GameObject chosenBlock = spawner.currentBlock;
        if (chosenBlock != null) {

        }
    }


    private void HoldBlock() {
        
    }
    

    private void OnEnable() {
        tetrisControls.Enable();
    }

    private void OnDisable() {
        tetrisControls.Disable();   
    }
}
