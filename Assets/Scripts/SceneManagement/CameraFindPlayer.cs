using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFindPlayer : MonoBehaviour
{
    [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

    private void Update() {
        if (stateDrivenCamera.Follow == null) {
            stateDrivenCamera.Follow = FindObjectOfType<PlayerController>().gameObject.transform;
        }
    }
}
