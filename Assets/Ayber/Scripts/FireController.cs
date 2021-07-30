using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> Rigidbodies;
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
        
        if (other.CompareTag("Butterfly"))
        {GetComponent<AudioSource>().Play();
            foreach (var butterfly in Butterflies)
            {
                butterfly.SetActive(true);
                
            }
            foreach (var rigidbody in Rigidbodies)
            {
                
                rigidbody.isKinematic = false;
                
            }
           
        }
       
    }
}
