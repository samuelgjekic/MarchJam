using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private bool _isMoving = false;

    [Header("Movement")]
    [Space()]
    public float moveSpeed = 10f;
    public bool enableRandomMoveSpeed = false;
    public float minSpeed = 3f;
    public float maxSpeed = 5f;
    private bool _hasBeenHit = false;

    public AnimationManager _animationManager;

    [Header("Events")]

    public UnityEvent OnCreated;
    public UnityEvent IsMoving;

    // Start is called before the first frame update
    void Start()
    {
        if (_animationManager == null) _animationManager = gameObject.GetComponent<AnimationManager>();

        if(enableRandomMoveSpeed == true){ moveSpeed = SetRandomMoveSpeed(minSpeed, maxSpeed); }
        OnCreated?.Invoke();   
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemyLeft(); // Function to move enemy to the left when it is created.
        CheckIfEnemyDied(); // Check if enemy died and is off screen
    }

    private void CheckIfEnemyDied()
    {
    
    if (_hasBeenHit == false) return;

    // Get the object's position in viewport space
    Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

    // Check if the object is within the camera's viewport
    if (viewportPosition.x < 0 || viewportPosition.x > 5 || viewportPosition.y < 0 || viewportPosition.y > 5)
    {
        // If the object is outside the screen, destroy it
        killObject();
    }
    }

    private void MoveEnemyLeft()
    {
        if(_isMoving == false) { _isMoving = true; }

        // invoke event for isMoving
        IsMoving?.Invoke();

        transform.position +=  Vector3.left * moveSpeed * Time.deltaTime;

        // Destroy if outside scene
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    public float SetRandomMoveSpeed(float minValue, float maxValue)
    {
        return moveSpeed = Random.Range(minValue, maxValue);
    }

    public void SetHitStatus(bool hasBeenHit)
    {
        _hasBeenHit = hasBeenHit;
    }

    public bool GetHitStatus()
    {
        return _hasBeenHit;
    }

    public void killObject() 
    {
        Destroy(gameObject);
    }

  
    
}
