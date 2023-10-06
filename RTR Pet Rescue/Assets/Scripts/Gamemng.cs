using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gamemng : MonoBehaviour
{
    public static Gamemng instane;
    [SerializeField] public int Level { get; private set; }
    [SerializeField] private GameObject StaticUI;
    [SerializeField] private TextMeshProUGUI LevelTxInStatic;
    public int coin { get; private set; }

    private void Awake()
    {
        if (instane == null)
        {
            instane = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        StaticUI.SetActive(true);
        coin = 1;
        Level = 14;
        LevelTxInStatic.text = $"Level {Level}";
    }
    public IEnumerator CheckDoneLevel(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(obj);
        yield return new WaitForSeconds(1f);

        GameObject ring = GameObject.FindGameObjectWithTag("Ring");
        if (ring == null)
        {
            SceneManager.LoadScene("MainScene");
            StaticUI.SetActive(false);
        }
    }
    public void ResetSceneBtn()
    {
        SceneManager.LoadScene("Level" + Level);
    }
    public void NetLevel()
    {
        Level++;
        SceneManager.LoadScene("Level" + Level);
        StaticUI.SetActive(true);
        LevelTxInStatic.text = $"Level {Level}";
    }
    public void addcoin(int count)
    {
        coin += count;
    }
}
