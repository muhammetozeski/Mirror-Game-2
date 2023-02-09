using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour
{
    /// <summary>
    /// Do not use this to find script. Use UniqueID instead.
    /// </summary>
    [Tooltip("Name must not be same with other pooling system scripts and not be null")]
    public string UniqueName;

    public bool InitializeOnStart = true;

    [SerializeField] GameObject ParentObject;

    [SerializeField] private GameObject[] _ObjectsToBePooled;
    public GameObject[] ObjectsToBePooled
    {
        get { return _ObjectsToBePooled; }
        private set { _ObjectsToBePooled = value; }
    }

    [SerializeField] private int _poolSize = 0;
    public int poolSize 
    {
        get { return _poolSize; } 
        private set { _poolSize = value; }
    }

    private GameObject[] _ObjectPool;
    /// <summary>
    /// It's not recommended you to access the pool directly. Use GetPool() instead. You can use this array to look at its properties of course.
    /// </summary>
    public GameObject[] ObjectPool
    {
        get { return _ObjectPool; }
        private set { _ObjectPool = value; }
    }

    private int poolCounter = 0;

    private int _UniqueID;
    public int UniqueID
    {
        get { return _UniqueID; }
        private set { _UniqueID = value; }
    }
    private void Start()
    {
        if(MainTools.CheckNull(UniqueName))
            UniqueID = UniqueName.GetHashCode();
        if(ObjectsToBePooled != null && InitializeOnStart)
            InitializePoolingSystem(ObjectsToBePooled, poolSize);
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
                    if(ParentObject)
                        obj.transform.SetParent(ParentObject.transform);
                    obj.SetActive(setActive);
                    ObjectPoolList.Add(obj);
                }
            }
            ObjectPool = ObjectPoolList.ToArray(); //arrays are faster than lists for connection according to big(o) stuff
        }
    }
    public void InitializePoolingSystem(GameObject ObjectToBePooled, int poolCount, bool setActive = false)
    {
        InitializePoolingSystem(MainTools.ToArray(ObjectToBePooled), poolCount, setActive);
    }

    /// <summary>
    /// Get objects with this. Don't forget to make setActive true.
    /// </summary>
    public GameObject GetPool()
    {
        if (poolCounter >= ObjectPool.Length) poolCounter = 0;
        return ObjectPool[poolCounter++];
    }
}
