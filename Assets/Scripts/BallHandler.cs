using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;

    private Rigidbody2D currentBallRigidBody ;
    private SpringJoint2D currentBallSpringJoint ;
    [SerializeField] float counter = 0f;
    [SerializeField] float respawnDelay = 0f;
    bool isDragged;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        RespawnBall();
    }
     void OnEnable()
    {
        EnhancedTouchSupport.Enable();    
    }
     void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        
    }
    void RespawnBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab,pivot.position,Quaternion.identity);
        currentBallRigidBody=ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint=ballInstance.GetComponent<SpringJoint2D>();
        currentBallSpringJoint.connectedBody = pivot;
    }

    // Update is called once per frame
    void Update()
    {   
        if (currentBallRigidBody==null) { return; }

        else if (Touch.activeTouches.Count==0)
        {
            if (isDragged)
            {
                LaunchBall();
            }
           
            return;
            
        }
        isDragged = true;
        currentBallRigidBody.isKinematic=true;
        Vector2 touchData=new Vector2();
        foreach (Touch touch in Touch.activeTouches)
        {
            touchData += touch.screenPosition;
            
        }
        touchData /= Touch.activeTouches.Count;
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(touchData);
        currentBallRigidBody.position = worldPoint;

    }
    private void LaunchBall()
    {
        //currentBallRigidBody.bodyType = RigidbodyType2D.Dynamic;
        currentBallRigidBody.isKinematic = false;
        currentBallRigidBody=null;

        Invoke("Detach", counter);

    }
    void Detach()
    {
        currentBallSpringJoint.enabled=false;
        currentBallSpringJoint=null;
        Invoke(nameof(RespawnBall), respawnDelay);

    }
}
