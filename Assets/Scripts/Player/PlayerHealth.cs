using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float whiteFlashTime = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float knockBackThrust = 5f;
    [SerializeField] private float knockbackTime = .5f;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
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
        maxHealth = startingHealth;
        defaultMat = spriteRenderer.material;
    }

    private void Start() {
        SetHeartsUI();
    }

    private void Update() {
        UpdateHearthUI();
    }

    private void SetHeartsUI() {
        Transform heartContainer = GameObject.Find("HeartContainer").transform;
        
        List<Image> allHearts = new List<Image>();

        foreach (Transform child in heartContainer)
        {
            allHearts.Add(child.gameObject.GetComponent<Image>());
        }

        hearts = allHearts.ToArray();
    }

    private void CheckIfDeath() {
        if (currentHealth <= 0) {
            PlayerController.instance.canMove = false;
            PlayerController.instance.canAttack = false;
            myAnimator.SetTrigger("dead");
        }
    }

    private void UpdateHearthUI() {
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if(i < maxHealth) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") && canTakeDamage) {
            TakeDamage(1);
            KnockBack(other.gameObject.transform);
            CheckIfDeath();
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
        if (currentHealth >= 1) {
            playerController.canMove = true;
        }
    }
}
