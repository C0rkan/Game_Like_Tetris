using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;



public class GameManagerScript : MonoBehaviour
{
    public SpawnerScript spawner { get; private set; }
    public TetrisControls tetrisControls;

    private void Awake() {
        tetrisControls = new TetrisControls();
        spawner = GetComponent<SpawnerScript>();


        tetrisControls.TetrisPlayer.Rotate.performed += context => RotateBlocks();
        tetrisControls.TetrisPlayer.Hold.performed += context => HoldBlock();
        tetrisControls.TetrisPlayer.Movement.performed += context => MoveBlocks();

        OnEnable();
    }

    private void RotateBlocks() {
        
        int rotationScale = 90;
        spawner.currentBlock.transform.Rotate(0,0,rotationScale);

    }


    private void MoveBlocks() {

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
