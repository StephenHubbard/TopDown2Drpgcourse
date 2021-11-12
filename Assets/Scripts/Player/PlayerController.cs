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

    [SerializeField] private GameObject hitBox_Top;
    [SerializeField] private GameObject hitBox_Bottom;
    [SerializeField] private GameObject hitBox_Left;
    [SerializeField] private GameObject hitBox_Right;

    [SerializeField] private GameObject boomerangPrefab;

    public static PlayerController instance;
    public bool canAttack = true;
    public bool canMove = true;

    Vector2 movement;

    private PlayerControls playerControls;

    private void Awake() {
        playerControls = new PlayerControls();

        Singleton();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    void Start()
    {
        startingMoveSpeed = moveSpeed;

        playerControls.Movement.Run.performed += _ => StartRun();
        playerControls.Movement.Run.canceled += _ => StopRun();
        playerControls.Combat.Attack.performed += _ => Attack();
        playerControls.RightClick.Use.performed += _ => UseItem();
    }

    void Update()
    {
        PlayerInput();
    }

    private void Singleton() {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetDefaultMoveSpeed() {
        moveSpeed = startingMoveSpeed;
        myAnimator.SetBool("isRunning", false);
    }

    private void FixedUpdate() {
        Move();   
    }

    private void StartRun() {
        myAnimator.SetBool("isRunning", true);
        moveSpeed += 4f;
    }

    private void StopRun() {
        myAnimator.SetBool("isRunning", false);
        moveSpeed -= 4f;
    }

    private void PlayerInput() {
        if (!canMove) { return; }

        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

        if(movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1) {
            if (canMove) {
                myAnimator.SetFloat("lastMoveX", movement.x);
                myAnimator.SetFloat("lastMoveY", movement.y);
            }
        }
    }

    private void Move() {
        if (!canMove) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    

    private void Attack() {
        if (canAttack) {
            rb.velocity = Vector2.zero;
            canMove = false;
            myAnimator.SetTrigger("attack");
        }
    }

    private void UseItem() {
        if (canAttack) {
            rb.velocity = Vector2.zero;
            canMove = false;
            myAnimator.SetTrigger("useItem");
        }
    }

    public void SpawnItem() {
        if (myAnimator.GetFloat("lastMoveX") == 1) {
            Instantiate(boomerangPrefab, new Vector2(transform.position.x + 1, transform.position.y + 0.5f), transform.rotation);
        }
        else if (myAnimator.GetFloat("lastMoveX") == -1) {
            Instantiate(boomerangPrefab, new Vector2(transform.position.x - 1, transform.position.y + 0.5f), transform.rotation);
        }
        else if (myAnimator.GetFloat("lastMoveY") == 1) {
            Instantiate(boomerangPrefab, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        }
        else if (myAnimator.GetFloat("lastMoveY") == -1) {
            Instantiate(boomerangPrefab, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
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
