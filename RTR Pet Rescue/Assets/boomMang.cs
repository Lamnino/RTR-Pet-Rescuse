using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomMang : MonoBehaviour
{
        [SerializeField] Transform effect;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ring")
        {
            collision.GetComponent<RingAct>().StartCoroutine(collision.GetComponent<RingAct>().OnDestroyDone(0.5f));
            transform.parent.GetComponent<RingAct>().StartCoroutine(transform.parent.GetComponent<RingAct>().OnDestroyDone(0.5f));
            boom();
        }
    }
    private void boom()
    {
        Instantiate(effect,transform);
    }
}
