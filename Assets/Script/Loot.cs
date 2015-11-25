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

    public void Pickup(GameObject picker)
    {
        if(picker.GetComponent<Player>())
        {
            switch (currentBonus)
            {
                case bonus.attaque:
                    picker.GetComponent<Player>().AjouterAttaque(value);
                    break;

                case bonus.shield:
                    picker.GetComponent<Player>().AjouterShield(value);
                    break;

                case bonus.piege:
                    picker.GetComponent<Player>().AjouterPiege(value);
                    break;
            }
        }
        Destroy(gameObject);
    }
}
