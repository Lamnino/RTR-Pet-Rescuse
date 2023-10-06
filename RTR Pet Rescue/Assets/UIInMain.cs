using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIInMain : MonoBehaviour
{
    [SerializeField] Transform CoinParent;
    [SerializeField] TextMeshProUGUI CoinNumber;
    [SerializeField] TextMeshProUGUI LevelTx;
    private void Start()
    {
        CoinNumber.text = Gamemng.instane.coin.ToString();
        LevelTx.text = $"Level {Gamemng.instane.Level}";
    }
    public void Cointranform()
    {
        CoinParent.gameObject.SetActive(true);
        float delay = 0;
        CoinNumber.transform.DOPunchScale(new Vector2(0.2f, 0.2f), 2f).SetDelay(0.7f);
        for (int i = 0; i< CoinParent.childCount; i++)
        {
            Transform cointr = CoinParent.GetChild(i);
            cointr.DOScale(1, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            cointr.GetComponent<RectTransform>().DOAnchorPos(new Vector2(349, 1577), 1f).SetDelay(delay + 0.5f).SetEase(Ease.OutBack);
            cointr.DOScale(0, 0.3f).SetDelay(delay + 1.6f).SetEase(Ease.OutBack);
            delay += 0.07f;
        }
            StartCoroutine(CountCoint());
    }
    IEnumerator CountCoint()
    {
            yield return new WaitForSeconds(0.7f);
        float time = 0.04f;
        float timeadd = 0.007f;
        for (int i = 1; i<11; i++)
        {
            Gamemng.instane.addcoin(1);
            CoinNumber.text = Gamemng.instane.coin.ToString();
            yield return new WaitForSeconds(time);
            time += timeadd;
            timeadd += 0.011f;
        }
        Gamemng.instane.NetLevel();
    }
}
