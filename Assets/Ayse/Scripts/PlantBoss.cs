using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBoss : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float _force;
    private void Start() 
    {
        _rb = GetComponent<Rigidbody>();    
    }
    private void OnTriggerEnter(Collider other) 
    {
        _rb.AddForce(Vector3.forward * _force, ForceMode.Impulse);
    }
}
