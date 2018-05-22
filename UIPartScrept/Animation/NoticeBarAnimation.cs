using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoticeBarAnimation : MonoBehaviour,IPointerUpHandler {


    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform);
    }
}
