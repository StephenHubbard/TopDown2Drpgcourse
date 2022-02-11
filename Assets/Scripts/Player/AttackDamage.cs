using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float thrust;
    [SerializeField] private bool isSword = false;
    [SerializeField] private bool isBomb = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyAttack(other);
        PlayerSelfDamage(other);
        DestructibleAttack(other);
        DestroyBoulderBlockingCave(other);
    }

    private void EnemyAttack(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            other.GetComponent<KnockBack>().getKnockedBack(PlayerController.Instance.transform, knockBackThrust);

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
            other.GetComponent<KnockBack>().getKnockedBack(transform, knockBackThrust);
        }
    }

    private void DestructibleAttack(Collider2D other) {
        if (other.GetComponent<Breakable>()) {
            other.GetComponent<Breakable>().BreakObject();
        }
    }

    private void DestroyBoulderBlockingCave(Collider2D other) {
        if (other.GetComponent<Boulder>() && isBomb) {
            other.GetComponent<Boulder>().DestroyCave();
        }
    }
}
