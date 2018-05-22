using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickExtention : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] private GameObject NoticeBar;
    [SerializeField] private Text HoverText;
    ///// <summary>
    ///// 单例
    ///// </summary>
    //private static ButtonClickExtention _instance;

    //public static ButtonClickExtention Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new ButtonClickExtention();
    //        }
    //        return _instance;
    //    }
    //}

    //private ButtonClickExtention()
    //{

    //}
    //private GameObject go;
    private int i = 0;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HoverText == null) HoverText = GameObject.FindGameObjectWithTag("HoverText").GetComponent<Text>();
        i++;
        UISeletedData.Instance.nowObject = eventData.pointerEnter;
        GameObject go = eventData.pointerEnter;
        Debug.Log("触发第"+i+"次");
        try
        {
            if (go.GetComponent<BasePanel>() != null)
            {
                return;
            }

            HoverText.text=  go.transform.Find("Text").GetComponent<Text>().text;
            HoverText.gameObject.transform.SetParent(eventData.pointerEnter.transform,false);
            HoverText.gameObject.SetActive(true);
                Debug.Log(UISeletedData.Instance.nowObject.name);
        }
        catch
        {
            return;
        }


    }

    private void OnMouseEnter()
    {

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //UISeletedData.Instance.nowObject = eventData.pointerPress;
        //NoticeBar.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform);
    }

    //private float i = 0;
    //void Update()
    //{
    //    if (i > 1)
    //    {
    //        if(EventSystem.current.currentSelectedGameObject != null)
    //        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    //        else
    //        {
    //            Debug.Log("未选中物体");
    //        }

    //        i = 0;
    //    }

    //    i += Time.deltaTime;

    //}
    public void OnPointerExit(PointerEventData eventData)
    {
        //UISeletedData.Instance.nowObject = eventData.pointerEnter;
        HoverText.gameObject.SetActive(false);
    }
}
