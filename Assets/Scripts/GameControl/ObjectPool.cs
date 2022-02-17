using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    [SerializeField] List<PoolInfo> poolInfos;
   
    public static ObjectPool Instance { get { return _instance; } }

    private void Start()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        foreach (PoolInfo inf in poolInfos)
        {
            CreatePool(inf);
        }
    }

    void CreatePool(PoolInfo info)
    {
        for (int i = 0; i < info.poolSize; i++)
        {
            GameObject obj = Instantiate(info.prefab, info.container.transform);
            float xPos = Random.Range(-info.xPositionLimit, info.xPositionLimit);
            float zPos = (info.prefabSize * i) + Random.Range(0, 3);

            obj.transform.position = new Vector3(xPos, info.yPosition, zPos);

            while (info.poolObjects.Find(x => Vector3.Distance(x.transform.position, obj.transform.position) < info.prefabSize) != null)
            { 
                zPos += info.prefabSize;
                obj.transform.position = new Vector3(xPos, info.yPosition, zPos);
            }
            
            if (zPos > info.zPositionLimit)
                obj.SetActive(false);
            info.poolObjects.Add(obj);
        }
    }




    public void RelocatePooledObject(GameObject obj)
    {
        PoolInfo poolInfo = null;
        foreach (var info in poolInfos)
        {
            if (info.poolObjects.Contains(obj))
            {
                poolInfo = info;
            }
        }
        if(poolInfo != null)
        {
            //poolInfo.poolObjects.Remove(obj);
            float zPos = (poolInfo.prefabSize * poolInfo.poolSize);
            /*Debug.Log("PosLimit: " + poolInfo.zPositionLimit + "\n" +
                obj.name + " obj relocated: old: " + obj.transform.position + " new: (" + 0 + ", " + 0 + ", " + (obj.transform.position.z + zPos) + ")\n" +
                "enabled: " + (obj.transform.position.z <= poolInfo.zPositionLimit));*/
            obj.transform.position += new Vector3(0, 0, zPos);
            while (poolInfo.poolObjects.Find(x => Vector3.Distance(x.transform.position, obj.transform.position) < poolInfo.prefabSize && obj != x) != null)
            {
                obj.transform.position += new Vector3(0, 0, poolInfo.prefabSize);
            }
            obj.SetActive(obj.transform.position.z <= poolInfo.zPositionLimit);
        }
    }
}
