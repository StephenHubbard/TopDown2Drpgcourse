using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePot : MonoBehaviour
{
    public void BreakPot() {
        gameObject.GetComponent<Animator>().SetTrigger("Break");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DelayDestroy(gameObject));
    }

    private IEnumerator DelayDestroy(GameObject other) {
        yield return new WaitForSeconds(2f);
        Destroy(other);
    }
}
