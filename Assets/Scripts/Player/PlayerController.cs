using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float startingMoveSpeed;
    [SerializeField] private Animator myAnimator;
    [SerializeField] public string areaTransitionName;
    [SerializeField] public bool canMove = true;

    [SerializeField] private GameObject hitBox_Top;
    [SerializeField] private GameObject hitBox_Bottom;
    [SerializeField] private GameObject hitBox_Left;
    [SerializeField] private GameObject hitBox_Right;

    public static PlayerController instance;

    Vector2 movement;

    private void Awake() {
        startingMoveSpeed = moveSpeed;
    }

    void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        PlayerInput();
        Attack();
        Run();
    }

    public void SetDefaultMoveSpeed() {
        moveSpeed = startingMoveSpeed;
        myAnimator.SetBool("isRunning", false);
    }

    private void FixedUpdate() {
        Move();   
    }

    private void Run() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            myAnimator.SetBool("isRunning", true);
            moveSpeed += 4f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            myAnimator.SetBool("isRunning", false);
            moveSpeed -= 4f;
        }
    }

    private void PlayerInput() {
        if (!canMove) { return; }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            if (canMove) {
                myAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }
    }

    private void Move() {
        if (!canMove) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    

    private void Attack() {
        if (Input.GetButtonDown("Fire1")) {
            //temp debug to elimate attack movement bug
            rb.velocity = Vector2.zero;
            canMove = false;
            myAnimator.SetTrigger("attack");
        }
    }

    public void canMoveFunction() {
        canMove = true;

        hitBox_Left.SetActive(false);
        hitBox_Right.SetActive(false);
        hitBox_Bottom.SetActive(false);
        hitBox_Top.SetActive(false);

    }

    public void ActivateCollider() {
        if (myAnimator.GetFloat("lastMoveX") == -1) {
            hitBox_Left.SetActive(true);
        }
        if (myAnimator.GetFloat("lastMoveX") == 1) {
            hitBox_Right.SetActive(true);
        }
        if (myAnimator.GetFloat("lastMoveY") == -1) {
            hitBox_Bottom.SetActive(true);
        }
        if (myAnimator.GetFloat("lastMoveY") == 1) {
            hitBox_Top.SetActive(true);
        }
    }

}
