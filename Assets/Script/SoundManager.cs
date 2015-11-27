using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip _menu;
    public AudioClip _game;
    public AudioClip _attack;
    public AudioClip _chest;
    public AudioClip _bonus;
    public AudioClip _move;
    public AudioClip _range;
    public AudioClip _relic;
    public AudioClip _end;
    public AudioClip _button;
    public AudioClip _ready;
    public AudioClip _papier;

    private static SoundManager instance;

    public static SoundManager Instance
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
    }

    public void Menu()
    {
        Camera.main.GetComponent<AudioSource>().clip = _menu;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void Game()
    {
        Camera.main.GetComponent<AudioSource>().clip = _game;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void Range()
    {
        Camera.main.GetComponent<AudioSource>().clip = _range;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void End()
    {
        Camera.main.GetComponent<AudioSource>().clip = _end;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void Attack()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_attack);
    }

    public void Papier()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_papier);
    }

    public void Chest()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_chest);
    }

    public void Ready()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_ready);
    }

    public void Bonus()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_bonus);
    }

    public void Move()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_move);
    }

    public void Relic()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_relic);
    }

    public void Button()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(_button);
    }
}
