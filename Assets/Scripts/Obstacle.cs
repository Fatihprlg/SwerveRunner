using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        CharacterControl characterControl = collision.collider.GetComponent<CharacterControl>();
        if(characterControl != null)
        {
            characterControl.DealDamage(damage);
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

   
}
