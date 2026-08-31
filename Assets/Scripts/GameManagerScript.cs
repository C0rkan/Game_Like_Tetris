using UnityEngine;
using Unity;
using UnityEngine.InputSystem;
using System;
using UnityEditor.Experimental.GraphView;
using NUnit.Framework.Constraints;


public class GameManagerScript : MonoBehaviour
{
    public SpawnerScript spawner { get; }
    public TetrisControls tetrisControls;

    private void Awake() {
        tetrisControls = new TetrisControls();
        tetrisControls.

    }

    private void HoldBlock() {
        
    }
    
    
    private void RotateBlocks() {
        
    }

    private void OnEnable() {
        
    }

    private void OnDisable() {
        
    }
}
