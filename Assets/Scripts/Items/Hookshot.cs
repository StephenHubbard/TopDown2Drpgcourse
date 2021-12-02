using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] private GameObject hookshotLinePrefab;
    [SerializeField] private Transform hookshotStartPoint;
    [SerializeField] private float hookDistance = 5f;
    
    private Vector2 hookshotTargetPoint;
    private PlayerController player;

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start() {
        FindPositionToThrow();
        GameObject newLine = Instantiate(hookshotLinePrefab, transform.position, transform.rotation);
        newLine.GetComponent<HookshotLineController>().AssignTarget(hookshotStartPoint, hookshotTargetPoint);
        newLine.transform.parent = gameObject.transform;
        player.usingHookshot = true;
        transform.parent = player.transform;
    }
    

    private void FindPositionToThrow() {
        Animator playerAnimator = player.GetComponent<Animator>();

        if (playerAnimator.GetFloat("lastMoveX") == 1) {
            hookshotTargetPoint = new Vector2(player.transform.position.x + hookDistance, player.transform.position.y + 0.5f);
            gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
            GetComponent<SpriteRenderer>().flipY = false;
        }
        else if (playerAnimator.GetFloat("lastMoveX") == -1) {
            hookshotTargetPoint = new Vector2(player.transform.position.x - hookDistance, player.transform.position.y + 0.5f);
            gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (playerAnimator.GetFloat("lastMoveY") == 1) {
            hookshotTargetPoint = new Vector2(player.transform.position.x, player.transform.position.y + hookDistance);
            gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
            GetComponent<SpriteRenderer>().flipY = false;

        }
        else if (playerAnimator.GetFloat("lastMoveY") == -1) {
            hookshotTargetPoint = new Vector2(player.transform.position.x, player.transform.position.y - hookDistance);
            gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }

}
