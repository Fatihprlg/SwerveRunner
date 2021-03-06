using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool isPooled = true;
    [SerializeField] bool special;
    

    private void OnTriggerEnter(Collider other)
    {
        CharacterControl characterControl = other.GetComponent<CharacterControl>();
        if (characterControl != null)
        {
            characterControl.DealDamage(damage);
        }
        else
        if (isPooled && !special)
        {
            ObjectPool.Instance.RelocatePooledObject(this.gameObject);
        }
    }

}
