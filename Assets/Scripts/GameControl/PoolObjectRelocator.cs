using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolObjectRelocator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ObjectPool.Instance.RelocatePooledObject(other.gameObject);
    }
}
