using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Material matWhiteFlash;

    private Material matDefault;
    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    private void Update() {
        DetectDeath();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void KnockBack(float knockbackTime, Transform damageSource, float thrust) {
            spriteRenderer.material = matWhiteFlash;
            GetComponent<EnemyMovement>().canMove = false;
            Vector2 difference = transform.position - damageSource.position;
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(knockbackTime));
    }

    private IEnumerator KnockCo(float knockbackTime) {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        GetComponent<EnemyMovement>().canMove = true;
        spriteRenderer.material = matDefault;
    }
}
