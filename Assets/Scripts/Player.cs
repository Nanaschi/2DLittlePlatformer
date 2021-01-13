using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpPlayerSpeed = 4f;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSide();
        Jump();
    }
    private void Run()
    {
        var controlImpact = Input.GetAxis("Horizontal");
        var horizontalVelocity = new Vector2(controlImpact * playerSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = horizontalVelocity;
        var playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }
    private void FlipSide()
    {
        var playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    private void Jump()
    {
        var onTheGround = myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!onTheGround)
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            
            var jumpVelocity = new Vector2(0, jumpPlayerSpeed);
            myRigidBody.velocity += jumpVelocity;
           
        }
        var playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > 0.5;
        myAnimator.SetBool("Jumping", playerHasVerticalSpeed);

    }
}
