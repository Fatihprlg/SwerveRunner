using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] float camSpeed = 3;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * camSpeed);
    }
}
