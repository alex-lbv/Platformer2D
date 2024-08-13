using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    [SerializeField] private Animator animator; 

    private float horizontal = -1f;
    private bool isGround = false;
    private bool isJump = false;
    private bool isFacingRight = true;
    private bool isFinish = false;
    private bool isLeverArm = false;


    private Rigidbody2D rb;
    private Finish finish;
    private LeverArm leverArm;



    const float speedMultiplier = 300f;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>(); 
        leverArm = FindObjectOfType<LeverArm>();
    }

    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("speedX", Mathf.Abs(horizontal));
        if(Input.GetKey(KeyCode.W) && isGround) {
            isJump = true;
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            if (isFinish) {
                finish.FinishLevel();
            }
            if (isLeverArm) {
                leverArm.ActivateLeverArm();
            }   
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speedMultiplier * Time.fixedDeltaTime, rb.velocity.y);

        if(isJump) {
            rb.AddForce(new Vector2(0f, 500f));
            isGround = false; 
            isJump = false; 
        }

        if(horizontal > 0f && !isFacingRight) {
            Flip();
        } else if (horizontal < 0f && isFacingRight) {
           Flip();
        }
    }

    void Flip () {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();

        if (other.CompareTag("Finish")) {
            Debug.Log("Worked");
            isFinish = true;
        }
        if (leverArmTemp != null) {
            isLeverArm = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();

        if (other.CompareTag("Finish")) {
            Debug.Log("NotWorked");
            isFinish = false;
        }
        if (leverArmTemp != null) {
            isLeverArm = false;
        }
    }
}
