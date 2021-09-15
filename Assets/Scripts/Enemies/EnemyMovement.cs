using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int xDir;
    private int yDir;

    private float randomNum;

    public bool canMove = true;

    Vector2 movement;

    private void Start() {
        StartCoroutine(ChangeDirection());
    }

    private void Update() {
        Move();
    }

    private IEnumerator ChangeDirection() {
        randomNum = Random.Range(-5, 5);
        movement.x = Random.Range(-1, 2);
        movement.y = Random.Range(-1, 2);
        yield return new WaitForSeconds(randomNum);
        StartCoroutine(ChangeDirection());

    }

    private void Move() {
        if (!canMove) { return; }


        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement.x == -1) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }

}
