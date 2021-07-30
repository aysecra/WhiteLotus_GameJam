using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recep
{
public class Player : MonoBehaviour
{
    [SerializeField] GameObject _playerRoot;
    [SerializeField] ParticleSystem _sparkleParticle;
    SwitchManager switchManager;
    private void Start() 
    {
        switchManager = FindObjectOfType<SwitchManager>();
        switchManager.OnSwitchToButterfly += SwitchToButterfly;
        switchManager.OnSwitchToCharacter += SwitchToCharacter;
    }
    private void Update() 
    {
        
    }
    private IEnumerator Blob()
    {
        yield return new WaitForSeconds(0.7f);
        _sparkleParticle.Play();
        _playerRoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        _playerRoot.SetActive(true);
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
    private void SwitchToButterfly()
    {
        _playerRoot.SetActive(false);
    }
    private void SwitchToCharacter()
    {
        StartCoroutine(Blob());   
    }
}
}
