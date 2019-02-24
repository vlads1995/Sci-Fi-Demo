﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity = 1f;
    void Start()
    {
        
    }

  
    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;       
        newRotation.x -= _mouseY * _sensitivity;
        transform.localEulerAngles = newRotation;
    }
}
