using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AttackDamage class can be put on any object with a trigger collider to set off different instances of damage.
public class AttackDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockBackThrust;
    [SerializeField] private float thrust;
    [SerializeField] private bool isBombExplosion = false;
    
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

            // for sword hitbox toggling
            if (transform.parent) {
                if (transform.parent.CompareTag("Player")) {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void PlayerSelfDamage(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            other.GetComponent<KnockBack>().getKnockedBack(transform, knockBackThrust);
        }
    }

    // Applies to bushes and pots
    private void DestructibleAttack(Collider2D other) {
        if (other.GetComponent<Breakable>()) {
            other.GetComponent<Breakable>().BreakObject();
        }
    }

    // Cave will only be destroyed by the instance bomb explosion prefab
    private void DestroyBoulderBlockingCave(Collider2D other) {
        if (other.GetComponent<Boulder>() && isBombExplosion) {
            other.GetComponent<Boulder>().DestroyCave();
        }
    }
}
