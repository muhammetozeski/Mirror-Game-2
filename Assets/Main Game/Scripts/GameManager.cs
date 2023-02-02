using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Layers which has been added in unity by programmer
/// </summary>
public enum Layers
{
    Default = 0,
    Reflectable = 3,
    Damageable = 6,
}
public class GameManager : MonoBehaviour
{
    //TODO: (turkish) game manager'i singleton olarak kullanmamýzýn pek bir mantýðý yok ve çok sorun çýkýyor bu þekilde. bunu obje olarak kullanalým
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// TODO: use this as object instead of static
    /// </summary>
    public InputManager inputManager;
    public Transform Player;
    public PlayerMovingController playerMovingController;


    private void Awake()
    {
        if (!Player || !playerMovingController || !inputManager)
        {
            Debug.LogError("Null object in Game Manager!");
        }
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
