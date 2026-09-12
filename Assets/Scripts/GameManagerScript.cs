using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;



public class GameManagerScript : MonoBehaviour
{
    public SpawnerScript spawner { get; private set; }
    private TetrisControls tetrisControls;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI timeTxt;

    private float timePassed;

    [Header("Gravity")]
    private float fallTimer = 0;
    private float fallTime = 1;


    private void Awake() {
        tetrisControls = new TetrisControls();
        spawner = FindAnyObjectByType<SpawnerScript>();
        

        tetrisControls.TetrisPlayer.Rotate.performed += context => RotateBlocks();
        tetrisControls.TetrisPlayer.Movement.started += context => MoveBlocks(context);
        tetrisControls.TetrisPlayer.Hold.performed += context => HoldBlock();
        tetrisControls.TetrisPlayer.FastDrop.performed += context => FastDrop();

    }

    private void Update() {
        fallTimer += Time.deltaTime;
        GraviyForBlocks();
        TimeManager();
    }

    public bool isPositionAvailable(Transform blockTransform) {

        foreach (Transform child in blockTransform) {

            int roundToX = Mathf.RoundToInt(child.position.x);
            int roundToY = Mathf.RoundToInt(child.position.y);

            if (roundToX < 0 || roundToX >= GridScript.width || roundToY < 0) {
                return false;
            }

            if (roundToY < GridScript.height) {
                if (GridScript.grids[roundToX, roundToY] != null) {
                    return false;
                }
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
                LineCheck();
                spawner.SpawnBlock(spawner.spawnLocation);
            }
            fallTimer = 0;
        }
    }
    //Satýr dololuk kontrol
    private void LineCheck() {
        
        for (int y = 0; y < GridScript.height; y++) {
            if (IsLineFull(y)) {
                LineDestroy(y);
                DropLine(y + 1); // +1 çünkü bir üstten düþüyoruz. 
                
                y--; //silinen satýr var ise yerine döneni de kontrol etmek için 
            }
        }
    }

    //Satýr dolu mu ?
    private bool IsLineFull(int y) {

        for (int x = 0; x < GridScript.width; x++) {
            if (GridScript.grids[x,y] == null) {
                return false;
            }
        }

        return true;
    }
    //Bir sartýrý silmek için
    private void LineDestroy(int y) {
        for (int x = 0; x < GridScript.width; x++) {
            //Ýlgili matristeki dolu alanlarý sileceðiz.
            Destroy(GridScript.grids[x, y].gameObject);
            //Silinen alanlarý boþ olarak geri tanýmlýyoruz.
            GridScript.grids[x,y]= null;
        }
    }

    private void DropLine(int startY) {

        for (int y = startY; y < GridScript.height; y++) {
            for (int x = 0; x < GridScript.width; x++) {

                if (GridScript.grids[x, y] != null) {
                    //Matris olarak aþþaðý indirir. 
                    GridScript.grids[x, y - 1] = GridScript.grids[x, y];
                    GridScript.grids[x,y] = null;
                    //Fiziki olarak aþþaðý indirir. 
                    GridScript.grids[x,y-1].position += new Vector3(0,-1,0);
                }
            }
        }
    }


    private void AddToGrid(Transform blockTransform) {

        //Parent silindiðinde bozulmamasý için objeleri listeye alýyoruz. 
        List<Transform> childrens = new List<Transform>();
        foreach (Transform child in blockTransform) {
            childrens.Add(child);
        }

        foreach (Transform child in childrens) {

            int roundToX = Mathf.RoundToInt(child.position.x);
            int roundToY = Mathf.RoundToInt(child.position.y);
            
            if (roundToX >= 0 && roundToX < GridScript.width && roundToY >= 0 && roundToX < GridScript.height) {
                GridScript.grids[roundToX,roundToY] = child;
                child.parent = null;
            }
        }
        Destroy(blockTransform.gameObject);

        spawner.currentBlock = spawner.nextBlock;
        spawner.nextBlock.transform.position = spawner.spawnLocation.position;
        spawner.nextBlock = null;
        spawner.canSpawnBlock = true;
    }

    private void RotateBlocks() {
        if (spawner.currentBlock != null) {
            if (spawner.currentBlock.name.Contains("SquareBlock")) {
                return;
            }
            int rotationScale = -90;
            spawner.currentBlock.transform.Rotate(0, 0, rotationScale);
        }
    }


    private void MoveBlocks(InputAction.CallbackContext context) {

        if (spawner == null || spawner.currentBlock == null) return;

        //Alýnan girdinin 1 veya -1 olduðunu anlamak için. 
        float xDirection = context.ReadValue<Vector2>().x;
        float yDirection = context.ReadValue<Vector2>().y;

        Vector3 move = new Vector3(Mathf.RoundToInt(xDirection),0,0);
        Vector3 moveY = new Vector3(0, Mathf.RoundToInt(yDirection), 0);

        spawner.currentBlock.transform.position += move;
        spawner.currentBlock.transform.position += moveY;


        if (!isPositionAvailable(spawner.currentBlock.transform)) {
            spawner.currentBlock.transform.position -= move;
        }
        if (!isPositionAvailable(spawner.currentBlock.transform)) {
            spawner.currentBlock.transform.position -= moveY;
        }
    }

    private void FastDrop() {

        if (spawner == null && spawner.currentBlock == null) {
            return;
        }

        while (true) {
            spawner.currentBlock.transform.position += new Vector3(0, -1, 0);

            if (!isPositionAvailable(spawner.currentBlock.transform)) {
                spawner.currentBlock.transform.position += new Vector3(0, 1, 0);
                break;
            }
        }
        AddToGrid(spawner.currentBlock.transform);
        LineCheck();
    }


    private void HoldBlock() {
        if (spawner.currentBlock != null && spawner.holdedBlock == null && !spawner.anyBlockHolded) {
            spawner.holdedBlock = spawner.currentBlock;
            spawner.holdedBlock.transform.position = spawner.holdBlockPosition.position;
            
            spawner.currentBlock = spawner.nextBlock;
            spawner.currentBlock.transform.position = spawner.spawnLocation.position;
            spawner.holdedBlock.transform.rotation = Quaternion.identity;
            spawner.anyBlockHolded = true;
            spawner.nextBlock = null;
        }
        else if(spawner.currentBlock != null && spawner.holdedBlock != null && spawner.anyBlockHolded) {
            GameObject temp = spawner.currentBlock;
            spawner.currentBlock = spawner.holdedBlock;
            spawner.holdedBlock= temp;

            spawner.currentBlock.transform.position = spawner.spawnLocation.position;
            spawner.holdedBlock.transform.position = spawner.holdBlockPosition.position;
        }
    }

    private void TimeManager() {
        timePassed += Time.deltaTime;

        int min = Mathf.FloorToInt(timePassed / 60);
        int sec = Mathf.FloorToInt(timePassed % 60);

        timeTxt.text = "Time : " + string.Format("{0:00}:{1:00}",min, sec);
    }

    private void ScoreManager() {

    }
    
    private void OnEnable() {
        tetrisControls.Enable();
    }

    private void OnDisable() {
        tetrisControls.Disable();   
    }
}
