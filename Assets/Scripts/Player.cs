using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent onBeginMovingLeft;
    public UnityEvent onBeginMovingRight;
    public UnityEvent onJump;
    public UnityEvent onLand;
    public GameObject groundCheck;
    public float moveSpeed = 7f;
    public float acceleration = 50f;
    public float jumpSpeed = 10f;

    Rigidbody rigidBody;
    int moveDir;


    float _moveX;
    float moveX{
        get{
            return _moveX;
        } set {
            if(_moveX != value){
                if (value > 0 && _moveX <= 0)
                    onBeginMovingLeft?.Invoke();
                else if (value < 0 && _moveX >= 0)
                    onBeginMovingRight?.Invoke();
                _moveX = value; 
            }
        }

    }
    


    bool _grounded;
    bool grounded{
        get{
            return _grounded;
        } set { 
            if(_grounded != value){
                if (!_grounded && value)
                    onLand?.Invoke();
                else
                    hasDoubleJumped = false;
                    _grounded = value;

            }
        }
    }

    bool hasDoubleJumped;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        moveDir = (int)Input.GetAxisRaw("Horizontal");
        if(moveDir !=0)
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration);
        else
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration * 2f);

        grounded = Physics.CheckSphere(groundCheck.transform.position, .2f, LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump")) {
            if (grounded){
                Jump();
            } else if (!hasDoubleJumped) {
                Jump();
                hasDoubleJumped = true;
            }

        }

    }

    void Jump(){
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0);
        onJump?.Invoke();

    }


    void FixedUpdate(){
        if (rigidBody.velocity.y < .75f * jumpSpeed || !Input.GetButton("Jump"))
            rigidBody.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * 5f;
        rigidBody.velocity = new Vector3(moveX, rigidBody.velocity.y, 0);
    }
}
