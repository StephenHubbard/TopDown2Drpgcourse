using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookshotLineController : MonoBehaviour
{
    [SerializeField] private float hookshotSpeed = 15f;
    [SerializeField] private GameObject chainHead;
    
    private LineRenderer lineRenderer;
    private GameObject newChainHead;
    private PlayerController player;
    private Vector2 targetPos;
    private Transform startPos;
    private bool targetLocationHit = false;
    public bool didHitSolidObject = false;

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        newChainHead = Instantiate(chainHead, transform.position, transform.rotation);
        newChainHead.transform.parent = gameObject.transform;
        player.itemInUse = true;
        player.canMove = false;
        StartCoroutine(FailSafeForCollisionBugs());
    }

    private void Update() {
        UpdateHookshotTargetPoint();
    }

    public void AssignTarget(Transform startPosition, Vector2 targetPosition) {
        targetPos = targetPosition;
        startPos = startPosition;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition.position);
        lineRenderer.SetPosition(1, startPosition.position);
    }

    private void UpdateHookshotTargetPoint() {
        if (targetLocationHit == false && didHitSolidObject == false) {
            lineRenderer.SetPosition(1, Vector2.Lerp(lineRenderer.GetPosition(1), targetPos, hookshotSpeed * Time.deltaTime));
            newChainHead.transform.position = lineRenderer.GetPosition(1);
        } 

        if (Vector2.Distance(lineRenderer.GetPosition(1), targetPos) < .5f) {
            targetLocationHit = true;
        }

        if (targetLocationHit == true && didHitSolidObject == false) {
            lineRenderer.SetPosition(1, Vector2.Lerp(lineRenderer.GetPosition(1), startPos.position, hookshotSpeed * Time.deltaTime));
            newChainHead.transform.position = lineRenderer.GetPosition(1);
            newChainHead.GetComponent<Collider2D>().enabled = false;
        } else if (didHitSolidObject == true) {
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, hookshotSpeed * Time.deltaTime);
            lineRenderer.SetPosition(0, startPos.position);
            newChainHead.transform.position = lineRenderer.GetPosition(1);
            newChainHead.GetComponent<Collider2D>().enabled = false;
        }

        if (Vector2.Distance(newChainHead.transform.position, startPos.position) < .5f && (targetLocationHit || didHitSolidObject)) {
            player.itemInUse = false;
            player.usingHookshot = false;
            player.canMove = true;
            Destroy(gameObject.transform.parent.gameObject);
            }
    }

    private IEnumerator FailSafeForCollisionBugs() {
        // debugging logic for now
        yield return new WaitForSeconds(1.5f);
        player.itemInUse = false;
        player.usingHookshot = false;
        player.canMove = true;
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void MovePlayerToHitPoint() {
        player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, hookshotSpeed * Time.deltaTime);
    }
}
