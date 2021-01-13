using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInput : MonoBehaviour
{
    [Header ("Player's parameters")]
    [SerializeField] float playerSpeed = 2f;
    bool isRunning;
    Animator animator;
    Rigidbody2D rigidBody2D;
    public NewInputScript controls;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        controls = new NewInputScript();
    }
    void Start()
    {
        controls.PlayerControl.Enable();
    }
    void Update()
    {
        Movement();
        FlipSide();

    }
    public void Movement()
    {
        var newValue = controls.PlayerControl.Movement.ReadValue<float>();
        var horizontalMovement = new Vector2(newValue * playerSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = horizontalMovement;
        isRunning = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;
        Debug.Log(isRunning);
        animator.SetBool("Running", isRunning);
    }
    public void FlipSide()
    {
       if (isRunning)
        {
            transform.localScale =  new Vector2 (Mathf.Sign(rigidBody2D.velocity.x), 1f);
        }
    }
}
