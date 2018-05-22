using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageGuid : MonoBehaviour {

    [Serializable]
    public class PageGuidEvent : UnityEvent<int>
    {
    }

    [SerializeField]
    private PageGuidEvent m_onChangePage = new PageGuidEvent();

    public PageGuidEvent onChangePage
    {
        get { return m_onChangePage; }
        set { m_onChangePage = value; }
    }

    [SerializeField]
    private Button lastPageButton;
    [SerializeField]
    private Button nextPageButton;

    [SerializeField] private List<GameObject> pageList;

    private List<int> pageIndexList = new List<int> {1,2,3,4,5,6,7,8,9,10,11};


   
    private int m_Int = 1;

    
    /// <summary>
    /// 当前页码
    /// </summary>
    public int Number
    {
        get { return m_Int; }
       private set
        {
            //m_Int = value;
            SetPage(value);
            if (null != onChangePage)
                onChangePage.Invoke(m_Int);
            //Debug.Log("Page "+ Number);
        }
    }

    public void Init<T>(List<T> indexList)
    {
        int x = (int)Mathf.Ceil((float)indexList.Count /12 );
        pageIndexList.Clear();
        for (int i = 1; i <= x; i++)
        {
            pageIndexList.Add(i);
        }
        lastPageButton.onClick.AddListener(() => PageDown());
        nextPageButton.onClick.AddListener(() => PageUp());
        SetPage(1);
    }

    

    private void PageDown()
    {

        if (Number == 1)
            return;

            Number--;

    }

    private void PageUp()
    {

        if (Number == pageIndexList[pageIndexList.Count - 1])
            return;

        Number++;

        //SetPage(Number);
    }


    private void SetPage(int pgNumber)
    {
        m_Int = pgNumber;
        if (Number > pageIndexList.Last())
        {
            Debug.Log("错误：超出页码范围，无法设置页码");
            return;
        }

        if (Number + 3<= pageIndexList.Last())
        {
            int start = pageIndexList.IndexOf(Number);
            for (int i = 0; i < 3; i++)
            {
                pageList[i].GetComponentInChildren<Text>().text = pageIndexList[start + i].ToString();
                pageList[i].SetActive(true);
            }
            pageList[3].SetActive(true);
        }
        else
        {
            int start = pageIndexList.IndexOf(Number);
            for (int i = 0; i < pageIndexList.Last() - Number+1; i++)
            {
                pageList[i].GetComponentInChildren<Text>().text = pageIndexList[start + i].ToString();
                pageList[i].SetActive(true);
            }

            for (int i = pageIndexList.Last() - Number+1; i < 4; i++)
            {
                pageList[i].SetActive(false);
            }
        }


    }

    /// <summary>
    /// 返回最大页码数
    /// </summary>
    /// <returns></returns>
    public int PageCount()
    {
        return pageIndexList.Last();
    }

    public void PageButtonClick()
    {
        int page = int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text) ;
        Number = page;
    }

}
