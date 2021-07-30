using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrController_AUK : MonoBehaviour
{
    private GameObject _character, _butterflies;
    [SerializeField] private ParticleSystem _poofParticle, _sparkleParticle;
    private Animator _characterAnim;
    
   
    private float _speed = 5; 
    public static bool isRunning = false, isBeginning = false, isButterFly = false;

    public static bool IsRunning { get => isRunning; set => isRunning = value; }
   
    public static bool IsButterFly { get => isButterFly; set => isButterFly = value; }

    void Awake()
    {
        _character = transform.GetChild(0).gameObject;
        _butterflies = transform.GetChild(1).GetChild(0).gameObject;
        _characterAnim = _character.GetComponent<Animator>();
    }
    

    void FixedUpdate()
    {
        
        if (isRunning)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (isBeginning == false)
            {
                // leave idle situation and switch into running situation
                _characterAnim.SetBool("isRunning",true);
                isRunning = true;
                isBeginning = true;
            }
            else
            {
                // convert character to butterfly
                if (isButterFly == false)
                { _poofParticle.Play();

                    _butterflies.SetActive(true);
                    _character.SetActive(false);

                    isButterFly = true;
                    
                   
                }

                // convert butterfly to character
                else
                {
                    StartCoroutine(Blob());
                    isButterFly = false;
                }
            }
        }
        
    }

    // character growth effect
    private IEnumerator Blob()
    {
        _sparkleParticle.Play();
        _character.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        _butterflies.SetActive(false);
        _character.SetActive(true);
        _characterAnim.SetBool("isRunning",true);

        float _timer = 0;
        while (true)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                break;
            }
            _character.transform.localScale = Vector3.Lerp(_character.transform.localScale, Vector3.one, _timer);
            yield return new WaitForEndOfFrame();
        }
    }
}
