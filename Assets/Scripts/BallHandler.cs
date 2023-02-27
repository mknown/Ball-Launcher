using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Rigidbody2D currentBallRigidBody ;
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
            currentBallRigidBody.bodyType=RigidbodyType2D.Dynamic;
            return;
            
        }
        currentBallRigidBody.isKinematic=true;
        Vector2 touchData=Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(touchData);
        currentBallRigidBody.position = worldPoint;

    }
}
