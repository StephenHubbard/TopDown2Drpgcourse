using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explodePrefab;

    public void Explode() {
        Instantiate(explodePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
