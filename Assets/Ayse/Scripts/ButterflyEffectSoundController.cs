using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyEffectSoundController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButterflyEffect"))
        {
            
            GetComponent<AudioSource>().Play();
        }
       
    }
}
