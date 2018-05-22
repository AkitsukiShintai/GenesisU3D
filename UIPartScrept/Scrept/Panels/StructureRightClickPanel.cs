using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StructureRightClickPanel : BasePanel {


    [SerializeField]private List<ButtonExtention> Buttons = new List<ButtonExtention>();
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
        //RushALL();
        Buttons[0].onClick.AddListener(CutButtonClick);
        Buttons[1].onClick.AddListener(CopyButtonClick);
        Buttons[2].onClick.AddListener(PasteButtonClick);
        Buttons[3].onClick.AddListener(MarkButtonClick);
        Buttons[4].onClick.AddListener(StructureLineButtonClick);
        Buttons[5].onClick.AddListener(OrnementLineButtonClick);
        Buttons[6].onClick.AddListener(ChangeColorButtonClick);
        state = ShowState.Showing;
    }

    public override void OnContinue()
    {
        //FormationAdjustButton.Select();
        if (state == ShowState.Showing) return;
        canvasGroup.DOFade(1, .5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //RushALL();
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

    private void CutButtonClick()
    {
    }

    private void CopyButtonClick()
    {
    }

    private void PasteButtonClick()
    {
    }

    private void MarkButtonClick()
    {
    }

    private void StructureLineButtonClick()
    {
    }

    private void OrnementLineButtonClick()
    {
    }

    private void ChangeColorButtonClick()
    {
    }
}
