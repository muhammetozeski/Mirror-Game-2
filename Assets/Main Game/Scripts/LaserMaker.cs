using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class LaserMaker : MonoBehaviour
{
    //[SerializeField] GameObject endOfLaser;//TODO:add a red light or a cool VFX at the end of laser
    [SerializeField] Transform LaserStartPosition;
    [SerializeField] Camera MainCamera;
    public int ReflectionsLimit;
    public float MaxLenght;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;

    /// <summary>
    /// you should set the value you want to increase health of the object. 
    /// so set a negative value to decrease health.
    /// </summary>
    float DamagePower = -1;

    InputManager inputManager;
    InputAction Fire;
    private void Awake()
    {
        inputManager = GameManager.Instance.inputManager;
        lineRenderer = GetComponent<LineRenderer>();
        Fire = inputManager.Fire;

        Fire.canceled += i =>
        {
            lineRenderer.enabled = false;
        };

    }
    private void FixedUpdate()
    {
        if (Fire.IsInProgress())
        {
            lineRenderer.enabled = true;

            Vector3 MidOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0); 
            ray = MainCamera.ScreenPointToRay(MidOfScreen);

            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, LaserStartPosition.position);

            float remainingLenght = MaxLenght;

            for (int i = 0; i < ReflectionsLimit; i++)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLenght))
                {
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                    remainingLenght -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                    if (hit.transform.gameObject.layer != (int)Layers.Reflectable)
                    {
                        if (hit.transform.gameObject.layer == (int)Layers.Damageable)
                        {
                            //here give some damage to enemies:

                            IDamageable damageable;
                            if(hit.transform.TryGetComponent<IDamageable>(out damageable))
                            {
                                damageable.IncreaseHealth(DamagePower);
                            }
                        }

                        //Ray hitted something not reflectable and won't get reflect again
                        break;
                    }
                    
                }
                else
                {
                    //Ray is going to sky

                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLenght);
                    
                }
            }
            
        }
    }
}
