using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HookShotChainHead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        FindObjectOfType<HookshotLineController>().didHitSolidObject = true;
    }
}
