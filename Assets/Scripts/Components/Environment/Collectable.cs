using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] Vector3 rotation = Vector3.up;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] bool isPooled = true;
    [SerializeField] GameObject collectParticle;

    private void OnTriggerEnter(Collider other)
    {
        CharacterControl characterControl = other.GetComponent<CharacterControl>();
        if (characterControl != null)
        {
            characterControl.TakeGem();
            gameObject.SetActive(false);
            GameObject effect = Instantiate(collectParticle, transform.position, Quaternion.identity);
            Destroy(effect, 2);
        }
        if (isPooled) ObjectPool.Instance.RelocatePooledObject(this.gameObject);
        
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }

}
