using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100f;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, spinSpeed) * Time.deltaTime);
    }
}
