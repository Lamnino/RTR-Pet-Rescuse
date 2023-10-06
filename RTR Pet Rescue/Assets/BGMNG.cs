using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMNG : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = Gamemng.instane.spriteBG[Random.Range(0, Gamemng.instane.spriteBG.Count)];
    }
}
