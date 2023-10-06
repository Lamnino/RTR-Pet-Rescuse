using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOption : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] List<SpriteRenderer> prefabs = new List<SpriteRenderer>();
    [SerializeField] SpriteRenderer lockpfe;
    [SerializeField] Sprite lockSprite;
    bool isUnlock = false;
    [SerializeField] bool UnLockByMoney;
    UIInMain mainUI;
    private void Start()
    {
        mainUI = GameObject.FindObjectOfType<UIInMain>();
    }
    public void SpriteOptionClick()
    {
        if (UnLockByMoney && !isUnlock)
        {
            if (Gamemng.instane.coin < 1000)
            {
                mainUI.Ann("You don't have enough money", true);
            }
            else
            {
                for (int i=0; i<prefabs.Count; i++)
                {
                    prefabs[i].sprite = sprites[i];
                }
                lockpfe.sprite = lockSprite;
                    mainUI.Ann("Success", false);
                Gamemng.instane.addcoin(-1000);
                mainUI.CoinNumber.text = Gamemng.instane.coin.ToString();
            }
        }
        else
        {
            for (int i = 0; i < prefabs.Count; i++)
            {
                prefabs[i].sprite = sprites[i];
            }
                lockpfe.sprite = lockSprite;
                    mainUI.Ann("Success", false);
        }
    }
}
