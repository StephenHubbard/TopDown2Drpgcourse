using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float whiteFlashTime = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private bool canTakeDamage = true;
    private bool isDead = false;
    private Rigidbody2D rb;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
        maxHealth = startingHealth;
        defaultMat = spriteRenderer.material;
    }


    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") && canTakeDamage && currentHealth > 0) {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            TakeDamage(enemy.damageDoneToHero);
            GetComponent<KnockBack>().getKnockedBack(other.gameObject.transform, enemy.enemyKnockBackThrust);
        }
    }

    public void CheckIfDeath() {
        if (currentHealth <= 0 && !isDead) {
            // prevent death animation from triggering multiple times
            isDead = true;
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.canAttack = false;
            myAnimator.SetTrigger("dead");
            StartCoroutine(RespawnRoutine());
        } else {
            PlayerController.Instance.canMove = true;
            PlayerController.Instance.canAttack = true;
        }
    }

    public void TakeDamage(int damage) {
        spriteRenderer.material = whiteFlashMat;
        currentHealth -= damage;
        canTakeDamage = false;
        StartCoroutine(SetDefaultMatRoutine());
        StartCoroutine(DamageRecoveryTimeRoutine());
    }

    private IEnumerator SetDefaultMatRoutine() {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = defaultMat;
    }

    private IEnumerator DamageRecoveryTimeRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }


    private IEnumerator RespawnRoutine() {
        yield return new WaitForSeconds(2f);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("Town");
    }
}
