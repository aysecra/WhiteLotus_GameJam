using System;
using System.Collections;
using System.Collections.Generic;
using Recep;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterConroller_AUK : MonoBehaviour
{
    // refs
    [SerializeField] private GameObject _brokenCage, _brokenWeb,_character;

    [SerializeField] private float _scaleChange, _yJump;
    [SerializeField] private int initScale=1;
   
    private Animator animator;
    private Rigidbody _rb;
    


 

   

    void Awake()
    {
        
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
         
    }

    void OnTriggerEnter(Collider other)
    {
    
        
        {   
             if (other.tag == "Web")
             {
                 // destroy web --> replace web as broken web
                 Transform webTransform = other.gameObject.transform;
                 Instantiate(_brokenWeb, webTransform.position, webTransform.rotation);
                 // print(other.gameObject.transform.parent);
                 GameObject web = other.transform.parent.gameObject;
                 Destroy(web);
             }
            
             // broke cage and take butterfly
             else if (other.tag == "Cage")
             {
                 // show broking cage anim
                 animator.SetTrigger("isBrokingCage");
                 PlyrController_AUK.IsRunning = false;
                 IncreaseScale();
                        
            
                 StartCoroutine(DestroyBrokenCage(other));
             }
            
             else if (other.gameObject.name == "PlantBoss")
             {
                 animator.SetTrigger("isFalling");
                 PlyrController_AUK.IsRunning = false;
             }
            
             // hit the obstacle (bush) and fall down
             else if (other.tag == "Obstacle")
             {
                 // print("Hitted Obstacle");
                 animator.SetBool("isFalling",true);
                 // PlyrController_AUK.IsRunning = false;
                 StartCoroutine(StandUp());
                       
             }
             else if(other.tag == "LightningTag")
             {
                       
                 animator.SetBool("isFalling", true);
                 // animator.SetTrigger("isFalling");
                 //isRunning = false;
                 StartCoroutine(StandUp());
                        
            
             }
             else if(other.tag == "Wall")
             {
                       
                 animator.SetBool("isBrokingCage", true);
                 //animator.SetTrigger("isBrokingCage");
                        
                 StartCoroutine(WallFinish());
                        
             }
      
        
        }
    }

   

    IEnumerator DestroyBrokenCage(Collider other)
    {
        
        
        // replace cage as broken cage
        Transform cageTransform = other.gameObject.transform;
        GameObject currentBC = Instantiate(_brokenCage, cageTransform.position, cageTransform.rotation);
        Destroy(other.gameObject);

        yield return new WaitForSeconds(0.4f);
        Destroy(currentBC);

        // return running anim
        //animator.SetTrigger("isRunning");
        animator.SetBool("isBrokingCage",false);
        
        PlyrController_AUK.IsRunning = true;
        
        // increase character scale
        
    }

    void  DecreaseScale()
    {
        transform.localScale -= new Vector3(_scaleChange, _scaleChange, _scaleChange);
        initScale--;
        print("demage");
    }
    
    void  IncreaseScale()
    {
        transform.localScale += new Vector3(_scaleChange, _scaleChange, _scaleChange);
        initScale++;
    }
    IEnumerator StandUp()
    {
        yield return new WaitForSeconds(0.15f);
        PlyrController_AUK.isRunning = false;
        yield return new WaitForSeconds(0.4f);
       // animator.SetTrigger("isFalling");
       if (initScale>=2 )
       {    DecreaseScale();
           animator.SetBool("isFalling", false);
           yield return new WaitForSeconds(0.5f);
           PlyrController_AUK.isRunning = true;
           
           
       }
       else
       {
           
       }





    }
    IEnumerator WallFinish()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isBrokingCage", false);
        
        PlyrController_AUK.isRunning = true;
        yield return new WaitForSeconds(0.3f);
        IncreaseScale();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tornado")
        {
            _rb.AddForce(new Vector3(0, _yJump, 0), ForceMode.Impulse);
        }
    }
}
