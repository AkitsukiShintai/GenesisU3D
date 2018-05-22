using DG.Tweening;
using SpringGUI;
using UnityEngine;

public class ColorPickerPanel : BasePanel
{
    void Awake()
    {
        ColorPicer.onPicker.AddListener(color => { UISeletedData.Instance.SelectedColor = color; });
    }

    [SerializeField] private ColorPicker ColorPicer;
    public void CloseButtonClick()
    {
        //UISeletedData.Instance.SelectedColor = ColorPicer.Color;
        UIManager.Instance.PopPanel();
        if (UISeletedData.Instance.CurrentParameterImage != null)
        {
            //UISeletedData.Instance.ImageColor = UISeletedData.Instance.SelectedColor;
            //UISeletedData.Instance.CurrentParameterImage = UISeletedData.Instance.CurrentParameterImage;
            //Debug.Log(UISeletedData.Instance.SelectedColor);
        }      
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
        //ShowUIs();
        state = ShowState.Showing;
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
