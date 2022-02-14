using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    //[SerializeField] Animator FinishAnimator;
    [SerializeField] GameObject FinishAnimation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.LevelPassed();
            FinishAnimation.GetComponent<Animation>().Play();
        }
    }
}
