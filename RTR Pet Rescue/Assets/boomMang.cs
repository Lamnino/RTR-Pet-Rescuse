using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomMang : MonoBehaviour
{
    /// <summary>
    /// To effect and act of boom 
    /// </summary>
        [SerializeField] Transform effect;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // boom touch Ring
        if (collision.tag == "Ring")
        {
            //Destroy this ring
            collision.GetComponent<RingAct>().StartCoroutine(collision.GetComponent<RingAct>().OnDestroyDone(0.5f));
            //Destroy parento of boom
            transform.parent.GetComponent<RingAct>().StartCoroutine(transform.parent.GetComponent<RingAct>().OnDestroyDone(0.5f));
            //effect explose
            boom();
            //sound effect boom
            Gamemng.instane.aus.PlayOneShot(Gamemng.instane.Boom);
        }
    }
    private void boom()
    {
        Instantiate(effect,transform);
    }
}
