using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleScript : MonoBehaviour
{
    [SerializeField] RectTransform UiHandleRectTranform;
    [SerializeField] Color32  BGColor;
    [SerializeField] Color32 HandleColor;
    [SerializeField] Image BG;
    Toggle toggle;
    Vector2 handlePosition;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = UiHandleRectTranform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }
    void OnSwitch(bool on)
    {
        UiHandleRectTranform.DOAnchorPos(on ? handlePosition : -handlePosition, 0.5f);
        UiHandleRectTranform.GetComponent<Image>().DOColor(on ? HandleColor : Color.white, 1f);
        UiHandleRectTranform.GetComponent<Image>().DOColor(on ? HandleColor : Color.white, 1f);
        BG.DOColor(on ? BGColor : Color.white, 1f);
    }
    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
