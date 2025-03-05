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

    [Header("Events")]

    public UnityEvent OnCreated;
    public UnityEvent IsMoving;

    // Start is called before the first frame update
    void Start()
    {
        if(enableRandomMoveSpeed == true){ moveSpeed = SetRandomMoveSpeed(minSpeed, maxSpeed); }
        OnCreated?.Invoke();   
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemyLeft(); // Function to move enemy to the left when it is created.
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
}
