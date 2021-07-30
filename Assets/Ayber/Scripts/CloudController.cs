using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ayber
{
    

public class CloudController : MonoBehaviour
{
    [SerializeField] private Animator cloudAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
              cloudAnimator.SetBool("isLightning",true); 
              GetComponent<AudioSource>().Play();
              cloudAnimator.SetBool("isRaining",true);
              StartCoroutine(Light());
        }
    }
   // private void OnTriggerExit(Collider other)
   // {
    //    if (other.CompareTag("Player"))
     //   {
      //      cloudAnimator.SetBool("isLightning",false);  
      //      cloudAnimator.SetBool("isRaining",false);  
      //  }
   // }

    IEnumerator Light()
    {
        yield return new WaitForSeconds(0.1f);
        cloudAnimator.SetBool("isLightning", false);
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
}