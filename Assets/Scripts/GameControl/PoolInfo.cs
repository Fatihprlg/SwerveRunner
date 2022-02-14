using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PoolInfo
{
    public GameObject prefab;
    public GameObject container;
    public int poolSize;
    public float prefabSize;
    public float xPositionLimit;
    public float zPositionLimit;
    public float yPosition;
    [HideInInspector]
    public List<GameObject> poolObjects;
}

