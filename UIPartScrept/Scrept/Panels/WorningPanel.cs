using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WorningPanel : BasePanel
{

    [SerializeField] private Text text;


    public override void OnShow()
    {
        if (state == ShowState.Showing)
        {
            return;
        }

        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, .5f);
        UIManager.Instance.PopPanel();
        //canvasGroup.DOFade(0, .5f).Delay();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //RushALL();
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
        canvasGroup.DOFade(0, 1f).SetDelay(2f);
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


    public void Init(string x = "四周有标尺无法关闭")
    {
        text.text = x;
    }
}
