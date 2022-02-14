using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        CharacterControl characterControl = other.GetComponent<CharacterControl>();
        if (characterControl != null)
        {
            characterControl.DealDamage(damage);
        }
    }

}
