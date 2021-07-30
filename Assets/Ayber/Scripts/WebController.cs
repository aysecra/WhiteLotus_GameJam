using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> Rigidbodies;

    void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            foreach (var rigidbody in Rigidbodies)
            {

                rigidbody.isKinematic = false;

            }

        }
        else if (other.CompareTag("Butterfly"))
        {
        }


    }
}
