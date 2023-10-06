using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using DG.Tweening;
using System;
using UnityEngine.UI;
public static class ToggleExtension
{
    public static void AddOnValueChangedListener<T>(this Toggle toggle, T param, Action<T, bool> onValueChanged)
    {
        toggle.onValueChanged.AddListener(delegate (bool value)
        {
            onValueChanged(param, value);
        });
    }
}
public class UIInMain : MonoBehaviour
{
    [SerializeField] Transform CoinParent;
    [SerializeField] public TextMeshProUGUI CoinNumberthem;
    [SerializeField] public TextMeshProUGUI CoinNumber;
    [SerializeField] TextMeshProUGUI LevelTx;
    [SerializeField] List<Sprite> image;
    [SerializeField] Image CongratulateImage;
    [SerializeField] Toggle toggleBGPref;
    [SerializeField] Transform ContentToggleBG;
    [SerializeField] GameObject announce;
    private void Start()
    {
        // coin text
        CoinNumber.text = Gamemng.instane.coin.ToString();
        CoinNumberthem.text = Gamemng.instane.coin.ToString();
        //level text
        LevelTx.text = $"Level {Gamemng.instane.Level}";
        //congratulate
            CongratulateImage.sprite = image[UnityEngine.Random.Range(0, 5)];
    }
    /// <summary>
    /// animation of coin  claim in main UI
    /// </summary>
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
    /// <summary>
    /// add coin when claim colab with effect claim
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Chose Background sprite will random to applied
    /// </summary>
    /// <param name="sprite">sprite</param>
    /// <param name="result">chose or not</param>
    public void ToggleBG(Sprite sprite, bool result)
    {
        if (!result)
        {
            if (Gamemng.instane.spriteBG.Contains(sprite))
            Gamemng.instane.spriteBG.Remove(sprite);
        }
        else
        {
            if (!Gamemng.instane.spriteBG.Contains(sprite))
                Gamemng.instane.spriteBG.Add(sprite);
        }
    }
    /// <summary>
    /// Background will unlock and aplli when you see video
    /// </summary>
    /// <param name="sprite"></param>
    public void BGVideo(Sprite sprite)
    {
            if (!Gamemng.instane.spriteBG.Contains(sprite))
        Gamemng.instane.spriteBG.Add(sprite);
        Toggle tg = Instantiate(toggleBGPref, ContentToggleBG).GetComponent<Toggle>();
        tg.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        tg.transform.SetAsFirstSibling();
        tg.AddOnValueChangedListener(sprite, ToggleBG);
    }
    public void ToggleBBG(Sprite sprite)
    {
              if (Gamemng.instane.spriteBG.Contains(sprite))
                Gamemng.instane.spriteBG.Remove(sprite);
        else
            if (!Gamemng.instane.spriteBG.Contains(sprite))
                Gamemng.instane.spriteBG.Add(sprite);
    }
    /// <summary>
    /// Anouncement when you do action (ex: buy sprite of ring)
    /// </summary>
    /// <param name="s"></param>
    /// <param name="isWarn"></param>
    public void  Ann(string s, bool isWarn)
    {
        announce.SetActive(true);
        if (isWarn)
        {
            announce.transform.GetChild(1).gameObject.SetActive(true);
            announce.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = s;
        }
        else
        {
            announce.transform.GetChild(0).gameObject.SetActive(true);
            announce.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = s;
        }
    }
}
