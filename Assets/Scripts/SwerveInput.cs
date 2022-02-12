using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInput : MonoBehaviour
{
    float _lastFramePositionX;
    float _moveFactorX;
    public float MoveFactorX { get { return _moveFactorX; } }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFramePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFramePositionX;
            _lastFramePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
        }
    }
}
