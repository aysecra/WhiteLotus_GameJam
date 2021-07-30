using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Main
{
public class SwitchManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _poofParticle;
    public UnityAction OnSwitchToButterfly;
    public UnityAction OnSwitchToCharacter;
    private int _switchVal;
    private void Start() 
    {
        _switchVal = 0;    
    }
    void Update()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                break;
            case GameManager.GameState.MainGame:
                GetInput();
                break;
            case GameManager.GameState.MiniGame:
                
                break;
            case GameManager.GameState.FinishGame:
                break;
        }
        
    }
    void GetInput()
    {
        if(Input.GetMouseButtonDown(0))
        { 
            if(_switchVal % 2 == 0)
            {
                SwitchToButterfly();
            }
            if(_switchVal % 2 == 1)
            {
                SwitchToCharacter();
            }
            _switchVal++;           
        }
    }
    void SwitchToButterfly()
    {
        _poofParticle.Play();
        OnSwitchToButterfly?.Invoke();
    }
    void SwitchToCharacter()
    {
        OnSwitchToCharacter?.Invoke();
    }
}
}
