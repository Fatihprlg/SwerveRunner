using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovingObstacle : MonoBehaviour
{
    GameObject[] positions;

    bool distanceCtrl = true;
    bool direction = false;
    int distCount = 0;
    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        positions = new GameObject[transform.childCount];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(0).gameObject;
            positions[i].transform.SetParent(transform.parent);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (distanceCtrl)
        {
            distance = (positions[distCount].transform.position - transform.position).normalized;
            distanceCtrl = false;
        }
        float dist = Vector3.Distance(transform.position, positions[distCount].transform.position);
        transform.position += distance * Time.deltaTime;
        if (dist < 0.5f)
        {
            distanceCtrl = true;
            if (distCount == positions.Length - 1)
            {
                direction = false;
            }
            else if (distCount == 0)
            {
                direction = true;
            }
            if (direction)
            {
                distCount++;
            }
            else
            {
                distCount--;
            }
        }


    }

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 0.1f);
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
#if UNITY_EDITOR
    [CustomEditor(typeof(MovingObstacle))]
    [System.Serializable]
    class MovingObstacleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MovingObstacle script = (MovingObstacle)target;
            if (GUILayout.Button("Add"))
            {
                GameObject newObject = new GameObject();
                newObject.transform.parent = script.transform;
                newObject.transform.position = script.transform.position;
                newObject.name = script.transform.childCount.ToString();
            }

        }
    }
#endif
}
