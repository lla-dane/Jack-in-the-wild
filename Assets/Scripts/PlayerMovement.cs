using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 8f;
    public float jumpSpeed = 28f;
    public GameObject torch;
    public GameObject barrel;

    Vector2 moveInput;
    bool IsSitting;
    bool IsTorchOn;
    public bool IsAlive = true;

    Animator myAnimator;
    BoxCollider2D feetCollider;
    Rigidbody2D rb;
    void Start()
    {
        Debug.Log("Started");
        IsSitting = false;
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        feetCollider=GetComponent<BoxCollider2D>();
 
    }

   
    void Update()
    {
        if(IsAlive)
        {
            Run();
            FlipSprite();
            Torch();
            InAirAnimations();
        }
       
    }

    void InAirAnimations()
    {
        bool playerHasPositiveVerticalSpeed = (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon) && Mathf.Sign(rb.velocity.y) ==  1;
        bool playerHasNegativeVerticalSpeed = (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon) && Mathf.Sign(rb.velocity.y) == -1;

        myAnimator.SetBool("isFlyingUp", playerHasPositiveVerticalSpeed);
        myAnimator.SetBool("isFlyingDown", playerHasNegativeVerticalSpeed);
    }

    void OnMove(InputValue value)
    {
        if (IsSitting) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (IsSitting) { return; }
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Torch()
    {
        if(Input.GetKeyDown("t") && !IsTorchOn)
        {
            torch.SetActive(true);
            IsTorchOn = true;
        }
        else if (Input.GetKeyDown("t") && IsTorchOn)
        {
            torch.SetActive(false);
            IsTorchOn = false;
        }
    }
    public void Sit()
    {        
            if (IsSitting) { return; }
            if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            
                myAnimator.SetBool("isGoingToSit", true);
                myAnimator.SetBool("isSitting", true);
                IsSitting = true;
            
        
    }
    public void Stand()
    {
        
        if (!IsSitting) { return; }          
            
        myAnimator.SetBool("isGoingToSit", false);
        myAnimator.SetBool("isSitting", false);
        myAnimator.SetBool("isGoingToStand", true);
        IsSitting = false;
            
        
    }
    void Run()
    {
        
        if (IsSitting) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - this.transform.position).normalized;

        if (direction.x > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
            Vector2 gun_scale = this.transform.GetChild(2).transform.GetChild(1).transform.localScale;
            gun_scale.y = 1f;
            gun_scale.x = -1f;
            float z_comp = barrel.transform.rotation.z;
            barrel.transform.rotation = Quaternion.Euler(0f, 0f, z_comp);
            this.transform.GetChild(2).transform.localScale = gun_scale;
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            Vector2 gun_scale = this.transform.GetChild(2).transform.GetChild(1).transform.localScale;
            float z_comp = barrel.transform.rotation.z;
            barrel.transform.rotation = Quaternion.Euler(0f, 180f, z_comp);
            gun_scale.y = -1f;
            gun_scale.x = 1f;
            this.transform.GetChild(2).transform.localScale = gun_scale;
        }

    }

}
