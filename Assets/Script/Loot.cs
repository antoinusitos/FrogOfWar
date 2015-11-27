using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {

    public enum bonus
    {
        shield = 0,
        attaque = 1,
        fauxCoffre = 2,
        piege = 3,
    }

    public bonus currentBonus;
    public int value = 1;
    public GameObject _case;

    void Start()
    {
        int rand = Random.Range(0, 3);
        if(rand == 0)
        {
            currentBonus = bonus.attaque;
        }
        else if (rand == 1)
        {
            currentBonus = bonus.shield;
        }
        else if (rand == 2)
        {
            currentBonus = bonus.piege;
        }
    }

    public void Pickup(GameObject picker)
    {
        if(picker.GetComponent<Player>())
        {
            SoundManager.Instance.Chest();
            switch (currentBonus)
            {
                case bonus.attaque:
                    picker.GetComponent<Player>().AjouterAttaque(value);
                    picker.GetComponent<Player>().TakeEpee();
                    break;

                case bonus.shield:
                    picker.GetComponent<Player>().AjouterShield(value);
                    picker.GetComponent<Player>().TakeShield();
                    break;

                case bonus.piege:
                    picker.GetComponent<Player>().AjouterPiege(value);
                    picker.GetComponent<Player>().TakeSac();
                    break;
            }
        }
        StartCoroutine(ShowBonus(picker));
    }

    IEnumerator ShowBonus(GameObject picker)
    {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.Bonus();
        if (currentBonus == bonus.attaque)
            PlayerManager.Instance.GetPlayerTurn().GetComponent<Player>().ShowBonus(0);
        else if (currentBonus == bonus.shield)
            PlayerManager.Instance.GetPlayerTurn().GetComponent<Player>().ShowBonus(1);
        else if (currentBonus == bonus.piege)
            PlayerManager.Instance.GetPlayerTurn().GetComponent<Player>().ShowBonus(2);
        yield return new WaitForSeconds(1);
        
        Destroy(gameObject);
    }
}
