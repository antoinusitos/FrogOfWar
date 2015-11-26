﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        menu = 0,
        inGame = 1,
        ending = 2,
        waitPlayer = 3,
    }

    private float _timeToFade = 0.5f;
    private float _timeFading = 0f;
    public bool _isFading = false;

    public GameState _state;

    public GameObject _plateau;
    public GameObject _camPosMap;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        _state = GameState.menu;
    }

    void Update()
    {
        if(_isFading)
        {
            _timeFading += Time.deltaTime;
            if(_timeFading >= _timeToFade)
            {
                _isFading = false;
                SceneFadeInOut.Instance.EndFade();
                if (_state == GameState.waitPlayer)
                {
                    UIManager.Instance.LaunchGame();
                    _state = GameState.inGame;
                    PlayerManager.Instance.showCases();
                    PlayerManager.Instance.ShowPlayers();
                }
                else if (_state == GameState.inGame)
                {
                    _state = GameState.waitPlayer;
                    UIManager.Instance.Turn();
                }
                else if (_state == GameState.ending)
                {
                    Destroy(_plateau);
                    _state = GameState.menu;
                    UIManager.Instance.MenuGame();
                    SoundManager.Instance.Menu();
                }
                SetCameraPos();
                //Debug.Log("show players");
            }
        }
    }

    public void StartGame ()
    {
        SetCameraPos();
        _plateau = new GameObject();
        _plateau.name = "plateau";
        FogOfWarManager.Instance.ClearRevealers();
        PlateauManager.Instance.InitPlateau(_plateau);
        PlayerManager.Instance.SpawnPlayers(_plateau);
        BonusManager.Instance.SpawnObjectif(_plateau);
        BonusManager.Instance.SpawnLoot(_plateau);
        PlayerManager.Instance.RefreshScorePlayers();
        PlayerManager.Instance.ResetGame();
        _state = GameState.waitPlayer;
        PlayerManager.Instance.HidePlayers();
        UIManager.Instance.Turn();
        SoundManager.Instance.Game();
        // Debug.Log("StartGame");
    }

    public void SetCameraPos()
    {
        if(_state == GameState.inGame)
        {
            Vector3 pos = PlayerManager.Instance.GetPlayerTurn().transform.position;
            pos.z = 6;
            Camera.main.transform.position = pos;
            Camera.main.orthographicSize = 2;
        }
        else
        {
            Camera.main.transform.position = _camPosMap.transform.position;
            Camera.main.orthographicSize = 5;
        }
    }

    public void FinGame()
    {
        _state = GameState.ending;
        UIManager.Instance.EndGame();
        ScoreManager.Instance.MAJScores();
    }

    public void MenuGame()
    {
        _timeFading = 0f;
        _isFading = true;
        SceneFadeInOut.Instance.BeginFade(1);
    }

    public void EndTurn()
    {
        SceneFadeInOut.Instance.BeginFade(1);
        PlayerManager.Instance.HidePlayers();
        _timeFading = 0f;
        _isFading = true;
        //Debug.Log("EndTurn");
    }

    public void Play()
    {
        _timeFading = 0f;
        _isFading = true;
        SceneFadeInOut.Instance.BeginFade(1);
        //Debug.Log("Play");
    }
}
