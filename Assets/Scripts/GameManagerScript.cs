using UnityEngine;
using Unity;
using UnityEngine.InputSystem;
using System;


public class GameManagerScript : MonoBehaviour
{
    public SpawnerScript spawner { get; }
    public PlayerInput input;

    private void Awake() {
        input = GetComponent<PlayerInput>();
    }

    private void HoldBlock() {
        
    }
    
    
    private void RotateBlocks() {
        
    }
}
