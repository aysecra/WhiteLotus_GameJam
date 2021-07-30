using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;






public class WallController : MonoBehaviour
{
    
   
    
    [Header("brick")]
    [SerializeField] private List<Rigidbody> Rigidbodies;
   
    [Header("butterfly rigidbody")]
    [SerializeField] private List<Rigidbody> ButterfliesRb;
    
    [Header("butterfly")]
    [SerializeField] private List<GameObject> Butterflies;
    
     void Awake()
    {
        
    }

     private void FixedUpdate()
     {
        // DecreaseScale?.Invoke();
     }

     private void OnTriggerEnter(Collider other)
     {
         var audioSource = GetComponent<AudioSource>();
        if (other.CompareTag("Player"))
        {
            foreach (var rigidbody in Rigidbodies)
            {
                
                rigidbody.isKinematic = false;
                
                audioSource.Play();
            }
           
           
        }
        if (other.CompareTag("Butterfly"))
        {
            foreach (var butterfly in Butterflies)
            {
                butterfly.SetActive(true);
                
            }
            foreach (var rigidbody in ButterfliesRb)
            {
                
                rigidbody.isKinematic = false;
                
            }
           
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
