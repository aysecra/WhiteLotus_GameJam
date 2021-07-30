using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_AUK : MonoBehaviour
{
    [SerializeField] private Transform _character;
    private Vector3 _distance;
    private bool isDie;

    void Awake()
    {
        _distance = transform.position - _character.position;
    }

    private void FixedUpdate()
    {
        if (_character.position.y <= -1)
        {
            isDie = true;
        }
    }

    void LateUpdate()
    {
        if (isDie == false)
        {
            cameraMove();
        }
        
    }

    void cameraMove()
    {
        Vector3 newPosition = new Vector3(transform.position.x, _distance.y + _character.position.y, _distance.z + _character.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.6f);
    }
}
