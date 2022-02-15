using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    //private List<GameObject> pooledObjects = new List<GameObject>();

    public static ObjectPool Instance { get { return _instance; } }
    [SerializeField] List<PoolInfo> poolInfos;
    

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
        //SolveConflicts();
    }

    void CreatePool(PoolInfo info)
    {
        for (int i = 0; i < info.poolSize; i++)
        {
            GameObject obj = Instantiate(info.prefab, info.container.transform);

            float xPos = Random.Range(-info.xPositionLimit, info.xPositionLimit);
            float zPos = (info.prefabSize * i) + Random.Range(0, 5);
           // while (info.poolObjects.Find(x => x.transform.position.z <= (zPos + info.prefabSize) && x.transform.position.z >= (zPos - info.prefabSize)) != null)
            while(info.poolObjects.Exists(x => (x.transform.position - obj.transform.position).magnitude < info.prefabSize)) 
                zPos += info.prefabSize;
            
            if (zPos > info.zPositionLimit)
                obj.SetActive(false);

            obj.transform.position = new Vector3(xPos, info.yPosition, zPos);
            
            info.poolObjects.Add(obj);
            //this.pooledObjects.Add(obj);
        }
    }

   /* void SolveConflicts()
    {
        
        for (int i = 0; i <= Mathf.Round(pooledObjects.Count / 2); i++)
        {
            if (pooledObjects.Exists(x => (x.transform.position - pooledObjects[i].transform.position).magnitude < 3))
                RelocatePooledObject(pooledObjects[i]);
        }
    }
*/
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
            float zPos = (poolInfo.prefabSize * poolInfo.poolSize) + Random.Range(1, 5);
            Debug.Log(obj.name + " obj relocated: old: " + obj.transform.position + " new: (" + 0 + ", " + 0 + ", " + zPos + ")");
            obj.transform.position += new Vector3(0, 0, zPos);
            obj.SetActive(obj.transform.position.z <= poolInfo.zPositionLimit);
            Debug.Log("enabled: " + (obj.transform.position.z <= poolInfo.zPositionLimit));
        }
    }
}
