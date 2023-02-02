using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// some spaghetti code. don't read. this script is for test purpose. i don't clear it and not use it.
/// </summary>
public class TS_ClassTest : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] AudioSource Audio;
    Vector3 firstPosition;

    [Header("debug purpose")]
    [SerializeField] float ShakeAmount;
    [SerializeField] float ShakeSpeed;
    private void Start()
    {
        firstPosition = transform.position;
    }
    private void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effect.SetActive(true);
            Audio.Play();
        }
        if(effect.activeInHierarchy)
            MainTools.ShakeYZ(transform, firstPosition);*/
    }

    private void FixedUpdate()
    {
        //Debug.Log(Time.fixedDeltaTime);
    }
    int mycar;
    int myVar { 
        get {
            return mycar;
        }
        set
        {
            mycar += value;
        }
    }

    [EButton]
    void gotolevel2()
    {
        SceneManager.LoadScene(1);
    }

    [EButton]
    void basBanaAþkým()
    {
        GameManager.Instance.Player.transform.position += Vector3.up;
        print(GameManager.Instance.Player.name);
    }




    enum MyEnum { Value1, Value2, Value3 };
    void testing()
    {
        string myString = gameObject.tag;
        if (Enum.TryParse(myString, out MyEnum myEnum))
        {
            if(myEnum == MyEnum.Value1)
            {
                //doing something
            }
        }

        if(gameObject.layer == 5) { }
    }

}
