using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public enum TypeOfPickUp{Rupee, Bomb, Mask};
    public TypeOfPickUp typeOfPickUp;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);

            if (typeOfPickUp == TypeOfPickUp.Rupee) {
                PickUpRupee();
            }
        }
    }

    private void PickUpRupee() {
        InventoryManager.instance.IncreaseRupeeCount(1);
    }
}
