using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCTRL : MonoBehaviour
{
    private Rigidbody playerRb;
    //[SerializeField] float speed = 20f;
    [SerializeField] float jumpforce = 30;
    [SerializeField] float gravityMod;
    [SerializeField]private bool OnGround;
    private static bool gameOver;
    [SerializeField] InputActionAsset inputActions;
    private InputAction MoveAct;
    private InputAction JumpAct;

    private void Awake(){
        MoveAct = inputActions.FindAction("Move");
        JumpAct = inputActions.FindAction("Jump");
    }

    private void OnEnable(){
        MoveAct.Enable();
        JumpAct.Enable();
    }

    private void OnDisable(){
        MoveAct.Disable();
        JumpAct.Disable();
    }

    void Start(){
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
    }

    void Update()
    {
        MovePlayer();

        if (JumpAct.WasPressedThisFrame() && OnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            OnGround = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            OnGround = true; 
        }
    }

    void MovePlayer(){
        Vector2 direction = MoveAct.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y)*Time.deltaTime;
    }
}
