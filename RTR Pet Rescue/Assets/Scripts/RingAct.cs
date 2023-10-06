using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RingAct : MonoBehaviour
{
    //[SerializeField] Rigidbody2D rb;
    static float time = 0.5f;
     Vector2 MousePos;
     Vector2 PreviousPos = Vector2.zero;
    [SerializeField] float dx;
    [SerializeField] float dy;
    [SerializeField]  bool IsHeld = false;
    [SerializeField]  bool IsLock;
    Collider2D[] allColliders;
    [SerializeField] GameObject[] Lock;
    [SerializeField] List<CircleCollider2D> Block = new List<CircleCollider2D>();
    BoxCollider2D[] rb;
    bool ISDone = false;
    [SerializeField]  int dircblock = 0;
    List<GameObject> LockIsTouching = new List<GameObject>();
    [SerializeField] float angleDegrees = 0;
    [SerializeField] bool touchBefore = false;
    private void Start()
    {
        if (Lock.Length > 0)
        {
            allColliders = FindObjectsOfType<PolygonCollider2D>();
        }
       if (Block.Count > 0)
        {
            rb = GameObject.FindObjectsOfType<BoxCollider2D>();
        }
    }

    private void Update()
    {
        if (IsHeld)
        {
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);
            CheckBlock();
            if (MousePos != PreviousPos)
            {
                Vector2 vectorBA = (Vector2)transform.position - PreviousPos;
                Vector2 vectorBC = (Vector2)transform.position - MousePos;

                float dotProduct = Vector2.Dot(vectorBA, vectorBC);
                float magnitudeBA = vectorBA.magnitude;
                float magnitudeBC = vectorBC.magnitude;

                float angleRadians = Mathf.Acos(dotProduct / (magnitudeBA * magnitudeBC));
                angleDegrees = angleRadians * Mathf.Rad2Deg;
                float crossProduct = vectorBA.x * vectorBC.y - vectorBA.y * vectorBC.x;
                if (crossProduct < 0)
                {
                    angleDegrees = -angleDegrees;
                }
                if (Block.Count == 0)
                    transform.Rotate(0, 0, angleDegrees);
                else
                {
                    if (IsLock)
                    {
                        foreach(var bl in Block)
                        {
                            Vector2 direction1 = (Vector2)(bl.transform.position -transform.position);
                            float radius = direction1.magnitude;
                            Vector2 direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)) * radius;
                            RaycastHit2D hit = Physics2D.Raycast((Vector2)bl.transform.position, direction, direction.magnitude); // Thay "YourLayer" bằng tên layer bạn muốn kiểm tr
                            Debug.DrawRay((Vector2)bl.transform.position, direction,Color.red);
                            Debug.Log("v");
                            if (hit.collider != null )
                            {
                                Debug.Log(hit.collider);
                            }
                        }
                    }
                        transform.Rotate(0, 0, angleDegrees);
                        touchBefore = false;
                        dircblock = 0;
                }
                PreviousPos = MousePos;
            }
        }
        else
        {
            if (!IsLock &&CheckLoc() && !Input.GetMouseButton(0) && !ISDone)
            {
                StartCoroutine(OnDestroyDone(time));
                ISDone = true;
            }
        }
    }
    private bool CheckBlock()
    {
        if (Block.Count == 0)
            return true;
        else
        {
            foreach (var gobj in Block)
            {
                foreach (var rigi in rb)
                {
                    if (gobj.IsTouching(rigi))
                    {
                        if (angleDegrees > 0) dircblock = 1;
                        else
                            if (angleDegrees < 0) dircblock = -1;
                        return false;
                    }
                }
            }
            return true;

        }
    }
    private bool CheckLoc()
    {
        if (Lock.Length == 0)
        {
            return true;
        }
        else
        {
            foreach (GameObject locks in Lock)
            {
                foreach (Collider2D collider2D in allColliders)
                {
                    if (collider2D != null)
                    {
                        if (locks.GetComponent<BoxCollider2D>().IsTouching(collider2D))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckLoc())
            {
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);
                PreviousPos = MousePos;
                IsHeld = true;
            }
            else
            {
                transform.DOShakeRotation(0.5f,new Vector3(0,0,2)).SetEase(Ease.InCubic);
            }
        }
    }
    public IEnumerator OnDestroyDone(float time)
    {
        if (dx == 0 && dy == 0)
        {
            transform.DOScale(0, time);
        }
        else
        {
            transform.DOLocalMove(new Vector2(dx, dy), time);
        }
        yield return new WaitForSeconds(time);
        Gamemng.instane.StartCoroutine(Gamemng.instane.CheckDoneLevel(gameObject));
    }
    public void OnMouseUp()
    {
        IsHeld = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsLock = true;
        LockIsTouching.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (LockIsTouching.Contains(other.gameObject)) LockIsTouching.Remove(other.gameObject);
        foreach (var i in LockIsTouching) Debug.Log(i);
        if (LockIsTouching.Count == 0)
        {
            IsLock = false;
        }
    }
}

