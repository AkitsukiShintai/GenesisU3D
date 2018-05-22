using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SaveMenuPanel : BasePanel {


    public override void OnContinue()
    {
        if (state == ShowState.Showing) return;

        canvasGroup.DOFade(1, .5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        state = ShowState.Showing;
    }

    public override void OnPause()
    {
        canvasGroup.DOFade(0, .5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        state = ShowState.Pausing;
    }

    public override void OnDelete()
    {
        canvasGroup.DOFade(0, .5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        state = ShowState.Unable;
    }

    public override void OnShow()
    {
        if (state == ShowState.Showing)
        {
            return;
        }


        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, .5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        state = ShowState.Showing;
    }



    public void SaveButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.SavePanel);
        
    }

    public void OpenButtonClick()
    {

    }

    public void ExportButtonClick()
    {

    }

    public void SaveAsButtonClick()
    {

    }

}
