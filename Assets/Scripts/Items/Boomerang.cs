using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100f;
    [SerializeField] private float throwDistance = 5f;
    [SerializeField] private float throwSpeed = 40f;
    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private int boomerangDamage = 1;
    [SerializeField] private float knockbackTime = .1f;
    [SerializeField] private float thrust = 15f;
    [SerializeField] private bool goForward = false;

    private PlayerController player;
    private Vector2 locationToThrow;

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
        circleCollider = GetComponent<Collider2D>();
    }

    private void Start() {
        FindPositionToThrow();
    }

    private void FindPositionToThrow() {
        Animator playerAnimator = player.GetComponent<Animator>();

        if (playerAnimator.GetFloat("lastMoveX") == 1) {
            locationToThrow = new Vector2(player.transform.position.x + throwDistance, player.transform.position.y);
        }
        else if (playerAnimator.GetFloat("lastMoveX") == -1) {
            locationToThrow = new Vector2(player.transform.position.x - throwDistance, player.transform.position.y);
        }
        else if (playerAnimator.GetFloat("lastMoveY") == 1) {
            locationToThrow = new Vector2(player.transform.position.x, player.transform.position.y + throwDistance);
        }
        else if (playerAnimator.GetFloat("lastMoveY") == -1) {
            locationToThrow = new Vector2(player.transform.position.x, player.transform.position.y - throwDistance);
        }

        goForward = true;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, spinSpeed) * Time.deltaTime);

        DetectDestination();
        MoveBoomerang();
    }

    private void DetectDestination() {
        if (goForward && Vector2.Distance(locationToThrow, transform.position) < .5f) {
            goForward = false;
        }
    }

    private void MoveBoomerang() {
        if (goForward) {
            transform.position = Vector2.MoveTowards(transform.position, locationToThrow, throwSpeed * Time.deltaTime);
        }
        else if (!goForward) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), throwSpeed * Time.deltaTime);
        }
        
        if (!goForward && Vector2.Distance(player.transform.position, transform.position) < 1f) {
            player.itemInUse = false;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        goForward = false;
        circleCollider.enabled = false;

        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(boomerangDamage);
            other.gameObject.GetComponent<EnemyHealth>().KnockBack(knockbackTime, player.transform, thrust);
        }
    }
}
