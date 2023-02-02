using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour
{
    public GameObject[] ObjectsToBePooled;
    [SerializeField] int poolCount = 0;
    public GameObject[] ObjectPool;
    int poolCounter = 0;
    private void Start()
    {
        InitializePoolingSystem(ObjectsToBePooled, poolCount);
    }
    public void InitializePoolingSystem(GameObject[] ObjectsToBePooled, int poolCount, bool setActive = false)
    {
        if (poolCount != 0)
        {
            List<GameObject> ObjectPoolList = new List<GameObject>();
            for (int i = 0; i < poolCount; i++)
            {
                for (int i2 = 0; i2 < ObjectsToBePooled.Length; i2++)
                {
                    GameObject obj = Instantiate(ObjectsToBePooled[i2]);
                    obj.SetActive(setActive);
                    ObjectPoolList.Add(obj);
                }
            }
            ObjectPool = ObjectPoolList.ToArray(); //arrays are faster than lists
            ObjectPoolList = null;
        }
    }
    public void InitializePoolingSystem(GameObject ObjectsToBePooled, int poolCount, bool setActive = false)
    {
        if (poolCount != 0)
        {
            List<GameObject> ObjectPoolList = new List<GameObject>();
            for (int i = 0; i < poolCount; i++)
            {
                GameObject obj = Instantiate(ObjectsToBePooled);
                obj.SetActive(setActive);
                ObjectPoolList.Add(obj);

            }
            ObjectPool = ObjectPoolList.ToArray(); //arrays are faster than lists
            ObjectPoolList = null;
        }
    }

    public GameObject GetPool()
    {
        if (poolCounter >= ObjectPool.Length) poolCounter = 0;
        return ObjectPool[poolCounter++];
    }
}
