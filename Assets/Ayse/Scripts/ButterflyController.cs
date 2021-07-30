using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    [SerializeField] GameObject _root; //_webWithButterfly, _brokenWeb;
    [SerializeField] Animator animator;

    //private bool _inTornado;
    //private float _timeCounter, _tornadoSpeed = 3, _tornadoWidth = 2, _tornadoHeight = 2;
    //private ButterfliesController butterflyController;

    void Awake()
    {
        //butterflyController = transform.parent.parent.parent.GetComponent<ButterfliesController>();
    }

    void FixedUpdate()
    {
        StartCoroutine(FlyingRoutine());       
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
}
