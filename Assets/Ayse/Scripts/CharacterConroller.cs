using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConroller : MonoBehaviour
{
    // refs
    [SerializeField] private GameObject _brokenCage, _brokenWeb;

    private float _scaleChange = 0.1f, _yJump = 61f;

    private Animator animator;
    private Rigidbody _rb;
    private ButterfliesController _BC;

    void Awake()
    {   
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _BC = transform.parent.GetChild(1).GetComponent<ButterfliesController>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Web")
        {
            // destroy web --> replace web as broken web
            Transform webTransform = other.gameObject.transform;
            Instantiate(_brokenWeb, webTransform.position, webTransform.rotation);
            GameObject web = other.transform.parent.gameObject;
            Destroy(web);
        }

        // broke cage and take butterfly
        else if (other.tag == "Cage")
        {
            
            // show broking cage anim
            animator.SetTrigger("isBrokingCage");
            GetComponent<AudioSource>().Play();
            
            PlyrController.IsRunning = false;
            
            _BC.SpawnButterfly();

            StartCoroutine(DestroyBrokenCage(other));
        }

        //else if (other.gameObject.name == "PlantBoss")
        //{
        //    animator.SetTrigger("isFalling");
        //    PlyrController.IsRunning = false;
        //}

        // hit the obstacle (bush) and fall down
        else if (other.tag == "Obstacle")
        {
            animator.SetTrigger("isFalling");
            PlyrController.IsRunning = false;

            // print("Not destroyed Butterfly: " + _BC.butterflyList.Count);
            _BC.DestroyButterfly();
            // print("Destroyed Butterfly: " + _BC.butterflyList.Count);

            if(_BC.butterflyList.Count > 0)
                StartCoroutine(StandUp());
            else
            {
                print("GAME OVER!!!!!");
                UIController.instance.Retry();
            }
        }

        else if (other.tag == "LightningTag")
        {

            animator.SetTrigger("isFalling");
            PlyrController.IsRunning = false;

            print("Not destroyed Butterfly: " + _BC.butterflyList.Count);
            _BC.DestroyButterfly();
            print("Destroyed Butterfly: " + _BC.butterflyList.Count);

            if (_BC.butterflyList.Count > 0)
                StartCoroutine(StandUp());
            else
            {
                print("GAME OVER!!!!!");
                UIController.instance.Retry();
            }

        }
        else if (other.tag == "Wall")
        {

            // animator.SetBool("isBrokingCage", true);
            animator.SetBool("isBreakWall", true);
            //SoundManagerScript.PlaySound("ConcreteBreak");
            StartCoroutine(WallFinish());
        }

        else if (other.tag == "Fall")
        {

            animator.SetTrigger("isFalling");
            PlyrController.IsRunning = false;
            
            print("GAME OVER!!!!!");
            UIController.instance.Retry();
        }

    }

    IEnumerator DestroyBrokenCage(Collider other)
    {
        yield return new WaitForSeconds(0.3f);

        // replace cage as broken cage
        Transform cageTransform = other.gameObject.transform;
        GameObject currentBC = Instantiate(_brokenCage, cageTransform.position, cageTransform.rotation);
        Destroy(other.gameObject);

        yield return new WaitForSeconds(0.4f);
        Destroy(currentBC);

        // return running anim
        animator.SetTrigger("isRunning");
        yield return new WaitForSeconds(0.15f);
        PlyrController.IsRunning = true;

        // increase character scale
        transform.localScale += new Vector3(_scaleChange, _scaleChange, _scaleChange);

        // turn butterfly
        GetComponentInParent<PlyrController>().TurnButterfly();
    }

    IEnumerator StandUp()
    {
        
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("isRunning");
        yield return new WaitForSeconds(0.5f);
        PlyrController.IsRunning = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tornado")
        {
            _rb.AddForce(new Vector3(0, _yJump, 0), ForceMode.Impulse);
        }
    }
    IEnumerator WallFinish()
    {
        yield return new WaitForSeconds(0.1f);
        //animator.SetBool("BrokeWall", false);
        animator.SetTrigger("isRunning");
        PlyrController_AUK.IsRunning = true;
        yield return new WaitForSeconds(0.3f);
    }


}
