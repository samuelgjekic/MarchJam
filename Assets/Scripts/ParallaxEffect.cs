using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here we will only control one element of the background.    //
// You'll need one script attached to each BG component        //
// So they will move separatedly to the individually set speed //

public class ParallaxEffect : MonoBehaviour
{
    public float speed;

    private Transform _camTransform;
    private Vector3 _pastCamPos;
    private float _spriteWidth;
    private float _startPos;

    void Start()
    {
        // Gets main camera position and stores it
        _camTransform = Camera.main.transform;
        _pastCamPos = _camTransform.position;
        // Gets the width and position of the sprite so it can duplicate later
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        _startPos = transform.position.x;
    }

    void LateUpdate()
    {
        // Sets the image movement amount at the desired speed...
        float deltaX = (_camTransform.position.x - _pastCamPos.x) * speed;
        float moveAmount = _camTransform.position.x * (1 - speed);
        // ...and moves the image accordingly
        transform.Translate(new Vector3(deltaX, 0, 0));
        _pastCamPos = _camTransform.position;

        // Duplicates the image when a border is reached so it loops
        if (moveAmount > _startPos + _spriteWidth)
        {
            transform.position = new Vector3(transform.position.x + _spriteWidth, transform.position.y, transform.position.z);
            _startPos += _spriteWidth * 2f;
        }
        else if (moveAmount < _startPos - _spriteWidth)
        {
            transform.position = new Vector3(transform.position.x - _spriteWidth, transform.position.y, transform.position.z);
            _startPos -= _spriteWidth * 2f;
        }
    }
}
