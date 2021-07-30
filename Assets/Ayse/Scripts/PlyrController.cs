using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrController : MonoBehaviour
{
    private GameObject _character, _butterflies;

    [SerializeField] private ParticleSystem _poofParticle, _sparkleParticle;
    [SerializeField] private GameObject _butterflyEffect;
    [SerializeField] private Transform _finishLine;

    private Animator _characterAnim;
    private GameObject _insButterflyObject;
    private AudioSource _audio;

    private float _speed = 5, _touchTimer = 0;
    private static bool _isRunning = false, _isBeginning = false, _isButterfly = false;
    private bool _isFinish = false;

    public static bool IsRunning { get => _isRunning; set => _isRunning = value; }
    public static bool IsBeginning { get => _isBeginning; set => _isBeginning = value; }

    void Awake()
    {
        _character = transform.GetChild(0).gameObject;
        _butterflies = transform.GetChild(1).GetChild(0).gameObject;
        _characterAnim = _character.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        _touchTimer += Time.deltaTime;

        if (_isRunning)
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));

        if (Input.GetMouseButtonDown(0) && _isBeginning == false)
        {
            if (CameraManager.isCamChanged)
            {
                // leave idle situation and switch into running situation
                _characterAnim.SetTrigger("isRunning");
                _isRunning = true;
                _isBeginning = true;
            }
        }

        // removed (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        else if (Input.GetMouseButtonDown(0) && _touchTimer >= 1 && _isRunning == true && _isBeginning == true)
        {
                // convert character to butterfly
                if (_isButterfly == false)
                {
                    TurnButterfly();
                }

                // convert butterfly to character
                else
                {
                    StartCoroutine(Blob());
                }
            _audio.Play();
            _touchTimer = 0;
        }

        // finish when reach finish position (assumed 93 / changeable)
        if (transform.position.z >= _finishLine.position.z && _isFinish == false)
        {
            _isFinish = true;
            _isRunning = false;
            _insButterflyObject = Instantiate(_butterflyEffect, transform.position + new Vector3(0, 0, 1.5f), transform.rotation);
            TurnButterfly();
            StartCoroutine(DefeatBoss());
            StartCoroutine(Victory());

        }
    }
    public void TurnButterfly()
    {
        _isButterfly = true;
        _poofParticle.Play();
        _butterflies.SetActive(true);
        _character.SetActive(false);   
    }
     public void TurnHuman()
        {
            _isButterfly = false; 
            _sparkleParticle.Play();
            _butterflies.SetActive(false);
            _character.SetActive(true);
            _characterAnim.SetTrigger("isRunning");
        }


    // character growth effect
    private IEnumerator Blob()
    {
        _sparkleParticle.Play();
        _character.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        _butterflies.SetActive(false);
        _character.SetActive(true);
        _characterAnim.SetTrigger("isRunning");

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

        _isButterfly = false;
    }

    private IEnumerator DefeatBoss()
    {
        // grow butterfly effect
        float timer = 0;
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
                break;
            _insButterflyObject.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(3, 3, 3), timer);
            yield return new WaitForEndOfFrame();
        }

        // throw boss / move butterfly effect
        timer = 0;
        while (true)
        {
            timer += Time.deltaTime/3f;
            if (timer >= 1f)
                break;
            _insButterflyObject.transform.position = Vector3.Lerp(_insButterflyObject.transform.position, _insButterflyObject.transform.position + Vector3.forward *4f, timer);
            yield return new WaitForEndOfFrame();
        }
        _insButterflyObject.SetActive(false);
    }

    private IEnumerator Victory()
    {
        yield return new WaitForSeconds(3);

        // turn character again
        _sparkleParticle.Play();
        _character.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        _butterflies.SetActive(false);
        _character.SetActive(true);

        // grow character
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                break;
            }
            _character.transform.localScale = Vector3.Lerp(_character.transform.localScale, Vector3.one, timer);
            yield return new WaitForEndOfFrame();
        }

        // walking
        timer = 0;
        _characterAnim.SetTrigger("isWalking");
        
        // move character
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
                break;
            _character.transform.position = Vector3.MoveTowards(_character.transform.position, _character.transform.position + new Vector3(0, 0, 0.1f), timer);
            yield return new WaitForEndOfFrame();
        }

        // victory pose
        _characterAnim.SetTrigger("isVictory");
        UIController.instance.NextLevel();
    }

}

