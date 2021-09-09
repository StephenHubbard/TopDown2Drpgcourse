using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string areaToLoad;
    [SerializeField] private string areaTransitionName;

    public AreaEntrance theEntrance;

    private void Start() {
        theEntrance.transitionName = areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            SceneManager.LoadScene(areaToLoad);

            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
