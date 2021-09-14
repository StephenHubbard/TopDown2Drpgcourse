using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator myAnimator;

    private int xDir;
    private int yDir;

    private void Start() {
        
    }

    private void Update() {
        Move();
    }

    // private IEnumerator ChangeDirection() {
    //     float randomNum(Random.Range)
    //     yield return WaitForSeconds(randomNum);
    // }

    private void Move() {
        rb.velocity = new Vector2(xDir, yDir).normalized * moveSpeed * Time.fixedDeltaTime;
    }

}
