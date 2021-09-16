using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject virtualCamera;
    [SerializeField] private PolygonCollider2D cameraConfiner;

    public static CameraController instance;

    private void Awake() {
        
    }

    void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        ExitApplication();
        DetectCameraConfiner();
    }

    private void ExitApplication() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }

    private void DetectCameraConfiner() {
        if (virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D != null) {return;}

        cameraConfiner = GameObject.Find("CameraConfiner").GetComponent<PolygonCollider2D>();

        virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = cameraConfiner;
    }
}
