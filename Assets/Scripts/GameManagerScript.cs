using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;



public class GameManagerScript : MonoBehaviour
{
    public SpawnerScript spawner { get; private set; }
    private TetrisControls tetrisControls;

    [Header("Gravity")]
    private float fallTimer = 0;
    private float fallTime = 1;


    private void Awake() {
        tetrisControls = new TetrisControls();
        spawner = FindAnyObjectByType<SpawnerScript>();


        tetrisControls.TetrisPlayer.Rotate.performed += context => RotateBlocks();
        tetrisControls.TetrisPlayer.Hold.performed += context => HoldBlock();
        tetrisControls.TetrisPlayer.Movement.performed += context => MoveBlocks(context);
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


    private void MoveBlocks(InputAction.CallbackContext context) {

        if (spawner == null || spawner.currentBlock == null) return;

        //Alýnan girdinin 1 veya -1 olduðunu anlamak için. 
        float xDirection = context.ReadValue<Vector2>().x;

        Vector3 move = new Vector3(Mathf.RoundToInt(xDirection),0,0);

        spawner.currentBlock.transform.position += move;

        if (!isPositionAvailable(spawner.currentBlock.transform)) {
            spawner.currentBlock.transform.position -= move;
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
