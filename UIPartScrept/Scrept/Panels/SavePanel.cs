using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : BasePanel
{

    [SerializeField] private InputField name;
    [SerializeField] private InputField address;

    private string Name;
    private string Path;

    public void GetName()
    {

        Name = name.text;
    }

    public void GetPath()
    {

        Path = address.text;
    }

    public void SaveButtonClick()
    {
        GetName();
        GetPath();
        //TODO
        UIManager.Instance.PopPanel();
    }

    public void CancelButtonClick()
    {
        UIManager.Instance.PopPanel();
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


}
