using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesEventController : MonoBehaviour
{
    [SerializeField] GameObject _webWithButterfly, _brokenWeb;

    private bool _inTornado;
    private float _timeCounter, _tornadoSpeed = 3, _tornadoWidth = 2, _tornadoHeight = 2;
    private ButterfliesController butterflyController;

    void Awake()
    {
        butterflyController = transform.parent.GetComponent<ButterfliesController>();
    }

    void FixedUpdate()
    {
        if (_inTornado)
            TornadoMovement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tornado"))
        {
            _inTornado = true;
            PlyrController.IsRunning = false;
            transform.parent.localPosition += Vector3.forward * 2f;

            //print("GAME OVER!!!!!");
            UIController.instance.Retry();
        }
        else if (other.CompareTag("Web"))
        {
            Transform webTransform = other.gameObject.transform.parent;
            GameObject web = other.transform.parent.gameObject;


            butterflyController.DestroyButterfly();

            if (butterflyController.butterflyList.Count > 0)
            {
                Instantiate(_brokenWeb, other.transform.position, other.transform.rotation);
                Destroy(web);
            }

            else
            {
                Instantiate(_webWithButterfly, webTransform.position, webTransform.rotation);
                Destroy(web);

                // transform.parent.parent --> Player
                transform.parent.parent.gameObject.SetActive(false);
                PlyrController.IsRunning = false;

                //print("GAME OVER!!!!!");
                UIController.instance.Retry();
            }
        }

        else if (other.CompareTag("Fire"))
        {
            butterflyController.DestroyButterfly();
            if (!(butterflyController.butterflyList.Count > 0))
            {

                transform.parent.parent.gameObject.SetActive(false);
                PlyrController.IsRunning = false;
                
                //print("GAME OVER!!!!!");
                UIController.instance.Retry();
            }
        }
        else if (other.CompareTag("Wall"))
        {
            butterflyController.DestroyButterfly();
            if (butterflyController.butterflyList.Count > 0)
            {
                
                GetComponentInParent<PlyrController>().TurnHuman();
                 
            }
            else
            {

                transform.parent.parent.gameObject.SetActive(false);
                PlyrController.IsRunning = false;
                //print("GAME OVER!!!!!");
                UIController.instance.Retry();
            }
        }

        else if (other.CompareTag("GameOver"))
        {
            PlyrController.IsRunning = false;
            //print("GAME OVER!!!!!");
            UIController.instance.Retry();
        }
    }

    void TornadoMovement()
    {
        _timeCounter += Time.deltaTime * _tornadoSpeed;
        float x = -Mathf.Cos(_timeCounter) * _tornadoWidth;
        float y = (_timeCounter / 10f) - 1f;
        float z = Mathf.Sin(_timeCounter) * _tornadoHeight;
        transform.localPosition = new Vector3(x, y, z);
        if (y > 1f)
        {
            _tornadoWidth = 20f;
            _tornadoHeight = 20f;
        }
    }
}
