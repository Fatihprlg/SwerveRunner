using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int point = 1;
    [SerializeField] Vector3 rotation = Vector3.up;
    [SerializeField] float rotationSpeed = 100f;

    private void OnTriggerEnter(Collider other)
    {
        CharacterControl characterControl = other.GetComponent<CharacterControl>();
        if (characterControl != null)
        {
            characterControl.TakeGem();
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }
}
