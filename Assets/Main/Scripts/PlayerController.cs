using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{

public class PlayerController : MonoBehaviour
{       
    // refs
    [SerializeField] private GameObject _brokenCage, _brokenWeb, _playerRoot, _character;
    [SerializeField] ParticleSystem _sparkleParticle;
    [SerializeField] SwitchManager switchManager;

    [SerializeField] private float _speed, _scaleChange, _yJump;
    [SerializeField] Animator animator;
    private bool isRunning = false;
    [SerializeField] Rigidbody _rb;

    void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        //_rb = GetComponentInChildren<Rigidbody>();
        switchManager.OnSwitchToButterfly += SwitchToButterfly;
        switchManager.OnSwitchToCharacter += SwitchToCharacter;
    }

    void Update()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                Prepare();
                break;
            case GameManager.GameState.MainGame:
                PlayerMove();
                break;
            case GameManager.GameState.MiniGame:
                
                break;
            case GameManager.GameState.FinishGame:
                break;
        }
    }
    void Prepare()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // leave idle situation and switch into running situation
            animator.SetTrigger("isRunning");
            isRunning = true;
            GameManager.instance.CurrentGameState = GameManager.GameState.MainGame;
        }
    }
    void PlayerMove()
    {
        if (isRunning)
            _playerRoot.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    private void SwitchToButterfly()
    {
        _character.SetActive(false);
    }
    private void SwitchToCharacter()
    {
        StartCoroutine(Blob());   
    }
    private IEnumerator Blob()
    {
        yield return new WaitForSeconds(0.7f);
        _sparkleParticle.Play();
        _playerRoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        _character.SetActive(true);
        float _timer = 0;
        while(true)
        {
            _timer += Time.deltaTime;
            if(_timer >= 1f)
            {
                break;
            }
            _playerRoot.transform.localScale = Vector3.Lerp(_playerRoot.transform.localScale, Vector3.one, _timer);
            yield return new WaitForEndOfFrame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);

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
            isRunning = false;

            StartCoroutine(DestroyBrokenCage(other));
        }

        else if (other.gameObject.name == "PlantBoss")
        {
            animator.SetTrigger("isFalling");
            isRunning = false;
        }

        // hit the obstacle (bush) and fall down
        else if (other.tag == "Obstacle")
        {
            // print("Hitted Obstacle");
            animator.SetTrigger("isFalling");
            isRunning = false;
            StartCoroutine(StandUp());
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
        isRunning = true;

        // increase character scale
        //transform.localScale += new Vector3(_scaleChange, _scaleChange, _scaleChange);
    }

    IEnumerator StandUp()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("isRunning");
        yield return new WaitForSeconds(0.5f);
        isRunning = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tornado")
        {
            _rb.AddForce(new Vector3(0, _yJump, 0), ForceMode.Impulse);
        }
    }
}

}
