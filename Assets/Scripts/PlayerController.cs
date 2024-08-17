using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;

    private float _horizontal = 1f;
    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFacingRight = true;
    private bool _isFinish = false;
    private bool _isLeverArm = false;

    private Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;

    const float speedMultiplier = 300f;


    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>(); 
        _leverArm = FindObjectOfType<LeverArm>();
    }

    void Update() {
        _horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if(Input.GetKey(KeyCode.W) && _isGround) {
            _isJump = true;
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            if (_isFinish) {
                _finish.FinishLevel();
            }
            if (_isLeverArm) {
                _leverArm.ActivateLeverArm();
            }   
        }
    }

    void FixedUpdate() {
        _rb.velocity = new Vector2(_horizontal * speedMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if(_isJump) {
            _rb.AddForce(new Vector2(0f, 500f));
            _isGround = false; 
            _isJump = false; 
        }

        if(_horizontal > 0f && !_isFacingRight) {
            Flip();
        } else if (_horizontal < 0f && _isFacingRight) {
           Flip();
        }
    }

    void Flip () {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            _isGround = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();

        if (other.CompareTag("Finish")) {
            Debug.Log("Worked");
            _isFinish = true;
        }
        if (leverArmTemp != null) {
            _isLeverArm = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();

        if (other.CompareTag("Finish")) {
            Debug.Log("NotWorked");
            _isFinish = false;
        }
        if (leverArmTemp != null) {
            _isLeverArm = false;
        }
    }
}
