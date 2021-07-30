using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController_AUK : MonoBehaviour
{
    [SerializeField] GameObject _root;
    [SerializeField] Animator animator;
    [SerializeField] GameObject _butterflies;
    bool inTornado;
    private float timeCounter, tornadoSpeed = 3, tornadoWidth = 2, tornadoHeight = 2;


    void FixedUpdate()
    {
        StartCoroutine(FlyingRoutine());
        if (inTornado || PlyrController_AUK.IsRunning == false)
        {
            TornadoMovement();
        }
    }


    IEnumerator FlyingRoutine()
    {
        float posX = Random.Range(-1f, 1f);
        float posY = Random.Range(-1f, 1f);
        float posZ = Random.Range(-1f, 1f);

        float speedModifier = Random.Range(0.75f, 1.45f);
        float animSpeed = Random.Range(0.75f, 1.45f);
        animator.speed = animSpeed;

        Vector3 newPos = new Vector3(posX, posY, posZ);
        Vector3 oldPos = _root.transform.localPosition;

        float _timer = 0;
        while (true)
        {
            _timer += Time.deltaTime * speedModifier;
            if (_timer >= 1f)
            {
                _root.transform.localPosition = Vector3.Lerp(newPos, oldPos, (_timer - 1f));
                yield return new WaitForEndOfFrame();
                if (_timer >= 2f)
                {
                    StartCoroutine(FlyingRoutine());
                    break;
                }
            }
            else
            {
                _root.transform.localPosition = Vector3.Lerp(oldPos, newPos, _timer);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator SwitchRoutine()
    {
        float posX = Random.Range(-0.10f, 0.10f);
        float posY = Random.Range(-0.60f, -0.50f);
        float posZ = Random.Range(-0.10f, 0.10f);
        Vector3 newPos = new Vector3(posX, posY, posZ) + Vector3.up;

        float _timer = 0;
        while (true)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _root.SetActive(false);
                break;
            }
            _root.transform.localPosition = Vector3.Lerp(_root.transform.localPosition, Vector3.zero, _timer);
            newPos.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, newPos, _timer);
            yield return new WaitForEndOfFrame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("Tornado"))
        {
            inTornado = true;
            PlyrController_AUK.IsRunning = false;
            transform.parent.localPosition += Vector3.forward * 2f;
        }

        if (other.CompareTag("Fire"))
        {
            PlyrController_AUK.IsRunning = false;
            _butterflies.SetActive(false);
        }
        if (other.CompareTag("Wall"))
        {
            PlyrController_AUK.IsRunning = false;
            _butterflies.SetActive(false);
        }

        
    }
    

   
    void TornadoMovement()
    {
        timeCounter += Time.deltaTime * tornadoSpeed;
        float x = -Mathf.Cos(timeCounter) * tornadoWidth;
        float y = (timeCounter / 10f) - 1f;
        float z = Mathf.Sin(timeCounter) * tornadoHeight;
        transform.localPosition = new Vector3(x, y, z);
        if (y > 1f)
        {
            tornadoWidth = 20f;
            tornadoHeight = 20f;
        }
    }
}
