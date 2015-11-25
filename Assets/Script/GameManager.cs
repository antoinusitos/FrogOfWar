using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        menu = 0,
        inGame = 1,
        ending = 2,

    }

    public GameState _state;

    public GameObject _plateau;

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

    public void StartGame ()
    {
        _plateau = new GameObject();
        _plateau.name = "plateau";
        _state = GameState.inGame;
        FogOfWarManager.Instance.ClearRevealers();
        PlateauManager.Instance.InitPlateau(_plateau);
        PlayerManager.Instance.SpawnPlayers(_plateau);
        BonusManager.Instance.SpawnObjectif(_plateau);
        BonusManager.Instance.SpawnLoot(_plateau);
        UIManager.Instance.LaunchGame();
        PlayerManager.Instance.RefreshScorePlayers();
        PlayerManager.Instance.ActualiseStaminaText();
    }

    public void FinGame()
    {
        _state = GameState.ending;
        UIManager.Instance.EndGame();
    }

    public void MenuGame()
    {
        Destroy(_plateau);
        _state = GameState.menu;
        UIManager.Instance.MenuGame();
    }
}
