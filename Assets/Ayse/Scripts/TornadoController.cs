using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        GetComponent<AudioSource>().Play();
       
    }
}
