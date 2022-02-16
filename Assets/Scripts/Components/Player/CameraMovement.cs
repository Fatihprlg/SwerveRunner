using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject winCamPos;
    [SerializeField] Vector3 offset;
    [SerializeField] float camSpeed = 3;

    void LateUpdate()
    {
        if (GameController.Instance.isSuccess)
        {
            winCamPos.transform.parent = null;
            transform.position = Vector3.Lerp(transform.position, winCamPos.transform.position, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, winCamPos.transform.rotation, Time.deltaTime);
        }
        else
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * camSpeed);
    }
}
