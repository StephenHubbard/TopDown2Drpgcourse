using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private int swordDamage = 1;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float thrust;
    
    private PlayerController player;
    
    private void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyAttack(other);
        DestructibleAttack(other);
    }

    private void EnemyAttack(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().TakeDamage(swordDamage);
            other.GetComponent<EnemyHealth>().KnockBack(knockbackTime, player.transform, thrust);
            gameObject.SetActive(false);
        }
    }

    private void DestructibleAttack(Collider2D other) {
        if (other.gameObject.CompareTag("Destructible")) {
            other.GetComponent<BreakablePot>().BreakPot();
        }
    }
}
