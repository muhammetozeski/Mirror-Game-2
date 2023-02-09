using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_SaveSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [EButton]
    void GetAllObjectsInScene()
    {
        objects = new List<GameObject>(FindObjectsOfType<GameObject>());
    }

    [EButton]
    void Save()
    {

    }
}
