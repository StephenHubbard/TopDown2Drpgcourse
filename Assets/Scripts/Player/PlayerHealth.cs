using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float whiteFlashTime = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float knockBackThrust = 5f;
    [SerializeField] private float knockbackTime = .5f;
    
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private bool canTakeDamage = true;
    private Rigidbody2D rb;
    private PlayerController playerController;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
        defaultMat = spriteRenderer.material;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") && canTakeDamage) {
            TakeDamage(1);
            KnockBack(other.gameObject.transform);
        }
    }

    private void TakeDamage(int damage) {
        spriteRenderer.material = whiteFlashMat;
        currentHealth -= damage;
        canTakeDamage = false;
        StartCoroutine(SetDefaultMatCo());
        StartCoroutine(DamageRecoveryTimeCo());
    }

    private IEnumerator SetDefaultMatCo() {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = defaultMat;
    }

    private IEnumerator DamageRecoveryTimeCo() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    public void KnockBack(Transform damageSource) {
            Vector2 difference = transform.position - damageSource.position;
            difference = difference.normalized * knockBackThrust * rb.mass;
            rb.AddForce(difference, ForceMode2D.Impulse);
            playerController.canMove = false;
            StartCoroutine(KnockCo());
    }

    private IEnumerator KnockCo() {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        playerController.canMove = true;
    }
}
