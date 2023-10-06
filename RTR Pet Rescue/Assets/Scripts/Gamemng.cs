using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Gamemng : MonoBehaviour
{
    public static Gamemng instane;
    public List<Sprite> spriteBG = new List<Sprite>();


    [SerializeField] public int Level;// { get; private set; }
    [SerializeField] private GameObject StaticUI;
    [SerializeField] private TextMeshProUGUI LevelTxInStatic;
    public bool istart =false;
    
    public AudioSource aus;
    public  AudioClip Boom;
    public AudioClip cantrote;
    public AudioClip coleectring;
    public AudioClip win;

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
        coin = 1110;
        LevelTxInStatic.text = $"Level {Level}";
    }

    public IEnumerator CheckDoneLevel(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        if (obj)
        Destroy(obj);
        yield return new WaitForSeconds(1f);

        GameObject ring = GameObject.FindGameObjectWithTag("Ring");
        if (ring == null)
        {
            istart = false;
            SceneManager.LoadScene("MainScene");
            aus.PlayOneShot(win);
            StaticUI.SetActive(false);
        }
    }
    public void ResetSceneBtn()
    {
        SceneManager.LoadScene("Level"+Level);
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
    public void SF(AudioClip clip)
    {
        Debug.Log("hehe");
        aus.Play();
    }

    public void SoundToggle(bool result)
    {
        if (result)
        {
            aus.mute = false;
        }
        else
        {
            aus.mute = true;
        }
    }
}
