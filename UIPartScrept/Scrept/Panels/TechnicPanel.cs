using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Page;
using UnityEngine;

public class TechnicPanel : BasePanel {

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
        int i = 0;

        
        foreach (Page2Data t in UISeletedData.Instance.PageDatas.PageDatas[3].dPage2)
        {
            GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.LeftButton);
            go.SetActive(true);
            go.AddComponent<ParameterButton>().Init(t, UISeletedData.Instance.PageDatas.PageDatas[3].PageIdx);
            go.GetComponent<RectTransform>().SetParent(gameObject.transform, false);
            go.GetComponent<RectTransform>().anchoredPosition = UIPositionManager.Instance.SecondtPanelPositionList[i];
            i++;
        }

        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 40 + 117);
        float x = GetComponent<RectTransform>().sizeDelta.x;
        float y = GetComponent<RectTransform>().sizeDelta.y;
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(x / 2f, -y / 2, 0);
        gameObject.GetComponent<RectTransform>().SetAsFirstSibling();
        //Debug.Log(gameObject.GetComponent<RectTransform>().GetSiblingIndex());
    }
}
