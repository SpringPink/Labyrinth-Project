using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float _distanceToPlayer;
    private Vector2 _input;

    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle _cameraAngle;

    private CameraRotation _cameraRotation;

    private void Awake()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    public void Look(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        _cameraRotation.Yaw += _input.x * mouseSensitivity.horizontal * Time.deltaTime;
        _cameraRotation.Pitch += _input.y * mouseSensitivity.vertical * Time.deltaTime;
        _cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, _cameraAngle.min, _cameraAngle.max);
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);
        transform.position = target.position - transform.forward * _distanceToPlayer;
    }
}


[Serializable] 
public struct MouseSensitivity
{
    public float horizontal;
    public float vertical;
}

public struct CameraRotation
{
    public float Pitch;
    public float Yaw; 
}

[Serializable]
public struct CameraAngle
{
    public float min;
    public float max;
}
