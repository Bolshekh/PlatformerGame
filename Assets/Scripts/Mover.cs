using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] float lerpStep;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, direction * speed + transform.position, lerpStep);
    }
}
