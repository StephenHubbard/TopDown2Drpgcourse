using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float thrust;
    [SerializeField] private bool isSword = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyAttack(other);
        PlayerSelfDamage(other);
        DestructibleAttack(other);
        Cave(other);
    }

    private void EnemyAttack(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            other.GetComponent<EnemyHealth>().KnockBack(knockbackTime, PlayerController.instance.transform, thrust);
            if (isSword) {
                gameObject.SetActive(false);
            }
        }
    }

    private void PlayerSelfDamage(Collider2D other) {
        // to prevent a damage instance from your own sword
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

    private void Cave(Collider2D other) {
        if (other.GetComponent<Cave>()) {
            other.GetComponent<Cave>().DestroyCave();
        }
    }
}
