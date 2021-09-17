using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Tilemap theMap;

    public static CameraController instance;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;

    private void Awake() {
        Singleton();
    }

    private void Start() {
        player = PlayerController.instance.transform;
        theMap = GameObject.Find("Ground").GetComponent<Tilemap>();

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
    }

    private void Update() {
        ExitApplication();
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = 
        new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    private void Singleton() {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void ExitApplication() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
