using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState 
    {
        Menu,
        Prepare,
        MainGame,
        MiniGame,
        FinishGame,
    }
    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get { return _currentGameState;}
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:                       
                    break;
                case GameState.MiniGame:                       
                    break;
                case GameState.FinishGame:                        
                    break;
            }
            _currentGameState = value;
        }           
    }
}
}
