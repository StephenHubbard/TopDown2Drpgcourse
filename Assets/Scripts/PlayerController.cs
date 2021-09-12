using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator myAnimator;
    [SerializeField] public string areaTransitionName;
    [SerializeField] public bool canMove = true;

    public static PlayerController instance;

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
        Movement();
        ExitApplication();
    }

    private void Movement() {
        if (canMove) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        } else {
            rb.velocity = Vector2.zero;
        }

        myAnimator.SetFloat("moveX", rb.velocity.x);
        myAnimator.SetFloat("moveY", rb.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            if (canMove) {
                myAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }
    }

    private void ExitApplication() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
