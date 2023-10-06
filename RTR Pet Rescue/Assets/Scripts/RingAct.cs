using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RingAct : MonoBehaviour
{
    //[SerializeField] Rigidbody2D rb;
    // 12, 15, 20
    static float time=0.5f;
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
    [SerializeField]  int dircblock = 0;
    List<GameObject> LockIsTouching = new List<GameObject>();
    [SerializeField] float angleDegrees = 0;
    [SerializeField] bool touchBefore = false;
    bool idone = false;
    private void Awake()
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
        // ring is being clicked
        if (IsHeld)
        {
            //ring is not locked by any lock
            if (CheckLoc())
            {
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);
                if (MousePos != PreviousPos)
                {
                    // determine degree to rorate the ring
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
                    // ring S is not  blocked by collider
                    if (Block.Count == 0)
                        transform.Rotate(0, 0, angleDegrees);
                    else
                    // ring might be blocked by collider
                    {
                        bool iss = false;
                        foreach (var bl in Block)
                        {
                            if (bl.IsTouchingLayers())
                            {
                                    iss = true;
                                // determine direction is blocked 
                                if (angleDegrees < 0 && !touchBefore)
                                {
                                    touchBefore = true;
                                    dircblock = -1;
                                    transform.DOShakeRotation(0.5f,new Vector3(0,0,0.5f)).SetEase(Ease.InCubic);
                                }
                                else
                                if (angleDegrees > 0 && !touchBefore)
                                {
                                    touchBefore = true;
                                    dircblock = 1;
                                    transform.DOShakeRotation(0.5f,new Vector3(0,0,0.5f)).SetEase(Ease.InCubic);
                                }
                            }
                        }
                        // result is not blocked
                        if (!iss) touchBefore = false;
                        if (angleDegrees * dircblock <= 0 && touchBefore || !touchBefore)
                        {
                            transform.Rotate(0, 0, angleDegrees);
                            dircblock = 0;
                        }
                    }
                    PreviousPos = MousePos;
                }
            }
        }
        else
        {
            if (Gamemng.instane.istart && !IsLock && CheckLoc() && !Input.GetMouseButton(0) && !idone)
            {
                // when the ting is free
                StartCoroutine(OnDestroyDone(time));
                idone = true;
            }
        }
    }
    /// <summary>
    /// Check the ring is blocked by the lock
    /// </summary>
    /// <returns></returns>
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
                if (locks.GetComponent<BoxCollider2D>().IsTouchingLayers())
                {
                    return false;
                }
            }
            return true;
        }
    }
    /// <summary>
    /// when mouse down set isheld = true when is not blocked
    /// </summary>
    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Gamemng.instane.istart = true;
            if (CheckLoc())
            {
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);
                PreviousPos = MousePos;
                IsHeld = true;
            }
            else
            {
                Gamemng.instane.SF(Gamemng.instane.cantrote);
                transform.DOShakeRotation(0.5f,new Vector3(0,0,2)).SetEase(Ease.InCubic);
            }
        }
    }
    /// <summary>
    /// Destroy the ring and check is done the level
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
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
    /// <summary>
    /// set is not locked by the lock of another ring
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (LockIsTouching.Contains(other.gameObject)) LockIsTouching.Remove(other.gameObject);
        if (LockIsTouching.Count == 0)
        {
            IsLock = false;
        }
    }
}

