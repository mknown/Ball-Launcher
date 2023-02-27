using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
            
        }
        Vector2 touchData=Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(touchData);

        Debug.Log(worldPoint);

    }
}
