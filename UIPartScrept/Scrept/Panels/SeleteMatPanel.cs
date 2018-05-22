using System.Collections.Generic;
using DG.Tweening;
using Page;
using UnityEngine;
using UnityEngine.UI;

public class SeleteMatPanel : BasePanel
{

    private Dictionary<string,GameObject> Buttons = new Dictionary<string, GameObject>();


    void Start()
    {
        
    }

    public override void OnShow()
    {
        if (state == ShowState.Showing)
        {
            return;
        }

        if (state == ShowState.None)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, .5f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            ShowUIs();
            state = ShowState.Showing;
        }

        if (state == ShowState.Unable)
        {
            canvasGroup.DOFade(1, .5f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            state = ShowState.Showing;
        }
    }

    public override void OnContinue()
    {
        if (state == ShowState.Showing) return;
        canvasGroup.DOFade(1, .5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        state = ShowState.Showing;
    }

    public override void OnDelete()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.DOFade(0, .5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        state = ShowState.Unable;
    }

    public override void OnPause()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.DOFade(0.5f, .5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        state = ShowState.Pausing;
    }

    

    private void ShowUIs()
    {
    
        int i =0 ;

        if (UISeletedData.Instance.PageDatas.PageDatas[0].dPage2.Count == 0)
        {
            return;
        }

        foreach (Page2Data t in UISeletedData.Instance.PageDatas.PageDatas[0].dPage2) {
            
                GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.LeftButton);
                go.SetActive(true);
                go.AddComponent<ParameterButton>().Init(t, UISeletedData.Instance.PageDatas.PageDatas[0].PageIdx);
                go.GetComponent<RectTransform>().SetParent(gameObject.transform, false);
                go.GetComponent<RectTransform>().anchoredPosition = UIPositionManager.Instance.SecondtPanelPositionList[i];
                i++;
                Buttons.Add(t.Name,go);
        }

        if (!UISeletedData.Instance.isSeInit)
        {
            //Buttons[UISeletedData.Instance.seletedPanelUIs[0]].GetComponent<Button>().onClick.Invoke();
            Buttons[UISeletedData.Instance.PageDatas.PageDatas[0].dPage2[0].Name].GetComponent<Button>().Select();
            UISeletedData.Instance.isSeInit = true;
        }


        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 40 + 117);
        float x = GetComponent<RectTransform>().sizeDelta.x;
        float y = GetComponent<RectTransform>().sizeDelta.y;
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(x / 2f, -y / 2, 0);
        gameObject.GetComponent<RectTransform>().SetAsFirstSibling();
        //Debug.Log(gameObject.GetComponent<RectTransform>().GetSiblingIndex());
    }

}
