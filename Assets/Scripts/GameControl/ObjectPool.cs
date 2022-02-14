using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] List<PoolInfo> poolInfos;

    private void Start()
    {
        if (instance = null) instance = this;
        foreach (PoolInfo inf in poolInfos)
        {
            CreatePool(inf);
        }
    }

    void CreatePool(PoolInfo info)
    {
        for (int i = 0; i < info.poolSize; i++)
        {
            GameObject obj = Instantiate(info.prefab);
            float xPos = Random.Range(-info.xPositionLimit, info.xPositionLimit);
            float zPos = (info.prefabSize * i) + Random.Range(1, 5);
            if (info.poolObjects.Find(x => x.transform.position.z <= (zPos + info.prefabSize) && x.transform.position.z >= (zPos - info.prefabSize)) != null)
                zPos += info.prefabSize;
            obj.transform.position = new Vector3(xPos, info.yPosition, zPos);
            info.poolObjects.Add(obj);
        }
    }
}
