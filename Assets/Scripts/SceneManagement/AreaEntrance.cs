using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private float moveSpeedWaitTime = .5f;
    
    public string transitionName;

    private void Start() {
        if (PlayerController.Instance != null) {
            if (transitionName == PlayerController.Instance.areaTransitionName) {
                PlayerController.Instance.transform.position = transform.position;
                StartCoroutine(HeroMoveDelayRoutine());

                if (UIFade.Instance != null) {
                    UIFade.Instance.FadeToClear();
                }
            }
        }
    }

    private IEnumerator HeroMoveDelayRoutine() {
        PlayerController.Instance.canMove = false;
        yield return new WaitForSeconds(moveSpeedWaitTime);
        PlayerController.Instance.canMove = true;
    }
}
