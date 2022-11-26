using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector3 defaultPosition;
    protected override void Start()
    {
        base.Start();
        //background.gameObject.SetActive(false);
        defaultPosition = background.localPosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //background.gameObject.SetActive(true);
        base.OnPointerDown(eventData); 
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
        background.localPosition = defaultPosition;
    }
}