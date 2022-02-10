using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private float moveSpeedWaitTime = .5f;
    
    public string transitionName;

    private PlayerController playerController;

    private void Awake() {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start() {
        if (PlayerController.instance != null) {
            if (transitionName == PlayerController.instance.areaTransitionName) {
                PlayerController.instance.transform.position = transform.position;
                StartCoroutine(HeroMoveDelay());

                if (UIFade.instance != null) {
                    UIFade.instance.FadeToClear();
                }
            }
        }

        
    }

    private IEnumerator HeroMoveDelay() {
        playerController.canMove = false;
        yield return new WaitForSeconds(moveSpeedWaitTime);
        playerController.canMove = true;
    }
}
