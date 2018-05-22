using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 管理UI中的一些小部件动画
/// </summary>
public class UIAnimationManager {

    /// <summary>
    /// 单例
    /// </summary>
    private static UIAnimationManager _instance;

    public static UIAnimationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIAnimationManager();
            }
            return _instance;
        }
    }

    private UIAnimationManager()
    {
        noticeBarGameObject = GameObject.Instantiate(Resources.Load("Prefab/NoticeBarPrefab")) as GameObject;
        noticeBarGameObject.GetComponent<CanvasGroup>().alpha = 0;
        noticeBarGameObject.transform.SetParent(GameObject.FindGameObjectWithTag("UIRoot").transform);
    }


    private static GameObject noticeBarGameObject;

    //private 
    private GameObject targetObj;

    private void GetClickObejet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //print("hit:" + hit.collider.gameObject.name);
            targetObj = hit.collider.gameObject;
        }
    }

    /// <summary>
    /// 显示小的提醒条
    /// </summary>
    /// <param name="parent">设置bar的父物体</param>
    public void NoticeBarEnter()
    {
        //GameObject parent = EventSystem.current.RaycastAll()
        GetClickObejet();

        if (noticeBarGameObject == null)
        {
            noticeBarGameObject = GameObject.Instantiate(Resources.Load("Prefab/NoticeBarPrefab")) as GameObject;
            //noticeBarGameObject.transform.SetParent(CanvasTransform);
        }

        noticeBarGameObject.GetComponent<CanvasGroup>().alpha = 1;
        noticeBarGameObject.transform.SetParent(targetObj.transform,false);
        noticeBarGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1.5f, 31f, 0);
        float parentY = targetObj.GetComponent<RectTransform>().sizeDelta.y;
        //float parentX = parent.GetComponent<RectTransform>().sizeDelta.x;
        noticeBarGameObject.GetComponent<RectTransform>().DOAnchorPosY(0,.5f);
        //noticeBarGameObject.transform.SetAsFirstSibling();
    }

    public void NoticeBarExist(GameObject parent)
    {
        if (noticeBarGameObject == null)
        {
            noticeBarGameObject = GameObject.Instantiate(Resources.Load("NoticeBarPrefab")) as GameObject;
            //noticeBarGameObject.transform.SetParent(CanvasTransform);
        }

        noticeBarGameObject.GetComponent<CanvasGroup>().alpha = 1;
        noticeBarGameObject.transform.SetParent(parent.transform, false);
        noticeBarGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1.5f, 0f, 0);
        noticeBarGameObject.GetComponent<RectTransform>().DOAnchorPosY(-31f, 0.5f)
            .OnComplete(() => noticeBarGameObject.GetComponent<CanvasGroup>().alpha = 0);
        //noticeBarGameObject.transform.SetAsFirstSibling();
    }

     public void NoticeBarState(GameObject parent)
    {
        if (noticeBarGameObject == null)
        {
            noticeBarGameObject = GameObject.Instantiate(Resources.Load("NoticeBarPrefab")) as GameObject;
            //noticeBarGameObject.transform.SetParent(CanvasTransform);
        }

        noticeBarGameObject.GetComponent<CanvasGroup>().alpha = 1;
        noticeBarGameObject.transform.SetParent(parent.transform, false);
       
    }
}
