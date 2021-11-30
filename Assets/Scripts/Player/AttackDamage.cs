using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float thrust;
    [SerializeField] private bool isSword = false;
    
    private PlayerController player;
    
    private void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyAttack(other);
        PlayerSelfDamage(other);
        DestructibleAttack(other);
    }

    private void EnemyAttack(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            other.GetComponent<EnemyHealth>().KnockBack(knockbackTime, player.transform, thrust);
            if (isSword) {
                gameObject.SetActive(false);
            }
        }
    }

    private void PlayerSelfDamage(Collider2D other) {
        if (isSword) { return; }
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            other.GetComponent<PlayerHealth>().KnockBack(transform);
        }
    }

    private void DestructibleAttack(Collider2D other) {
        if (other.GetComponent<Breakable>()) {
            other.GetComponent<Breakable>().BreakObject();
        }
    }
}
