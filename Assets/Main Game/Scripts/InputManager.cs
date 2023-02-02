using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [Tooltip("Input Action Map Name")]
    [SerializeField] public string PlayerMapName = "Player";

    [Tooltip("Pm = Player map. Input Action Name which is in the \"player\" input action map")]
    [SerializeField]
    public string PmMove = "Move", PmLook = "Look", PmFire = "Fire", PmJump = "Jump",
        PmInteract = "Interact";

    [HideInInspector] public InputAction Move, Look, Fire, Jump, Interact;
    [HideInInspector] public InputActionMap PlayerMap;


    public delegate void _InputManagerOnEnable();
    public _InputManagerOnEnable InputManagerOnEnable;
    public delegate void _InputManagerOnDisable();
    public _InputManagerOnDisable InputManagerOnDisable;

    private void Awake()
    {
        #region Input Action Assigns
        Move = playerInput.actions.FindAction(PlayerMapName + "/" + PmMove);
        Look = playerInput.actions.FindAction(PlayerMapName + "/" + PmLook);
        Fire = playerInput.actions.FindAction(PlayerMapName + "/" + PmFire);
        Jump = playerInput.actions.FindAction(PlayerMapName + "/" + PmJump);
        Interact = playerInput.actions.FindAction(PlayerMapName + "/" + PmInteract);
        PlayerMap = playerInput.actions.FindActionMap(PlayerMapName);
        #endregion

        InputManagerOnEnable = ThisOnEnable;
        InputManagerOnDisable = ThisOnDisable;
    }

    private void ThisOnEnable()
    {
        PlayerMap.Enable();
    }
    private void ThisOnDisable()
    {
        PlayerMap.Disable();
    }

    private void OnEnable()
    {
        InputManagerOnEnable();
        PlayerMap.Enable();
    }
    private void OnDisable()
    {
        //InputManagerOnDisable();//TODO: SOLVE THE BUG

        PlayerMap.Disable();
    }
}
