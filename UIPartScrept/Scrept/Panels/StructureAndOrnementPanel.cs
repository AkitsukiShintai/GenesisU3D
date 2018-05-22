using System;
using System.Reflection;
using DG.Tweening;
using Page;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StructureAndOrnementPanel : BasePanel
{
    [Serializable]
    public class ColorSelectEvent : UnityEvent<Color>
    {
    }

    [SerializeField]
    private ColorSelectEvent m_onChangeColor = new ColorSelectEvent();

    public ColorSelectEvent onChangeColor
    { 
        get { return m_onChangeColor; }
        set { m_onChangeColor = value; }
    }

    /// <summary>
    /// 当前引用颜色的图片，通过ImageColor.color可以获得颜色
    /// </summary>
    public Color ImageColor
    {
        get { return Image.color; }
        set
        {
            Image.color = value;
            if (onChangeColor != null && state == ShowState.Showing)
            {
                onChangeColor.Invoke(value);
            }
        }
    }

    [SerializeField] private GameObject LiftGidePanel;
    [SerializeField] private GameObject ColorBar;
    [SerializeField] private Image Image;


   

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
            UISeletedData.Instance.onChangeColor.RemoveAllListeners();
            UISeletedData.Instance.onChangeColor.AddListener(co => ImageColor = co);
            //onChangeColor.AddListener();
            state = ShowState.Showing;
        }

        if (state == ShowState.Unable)
        {
            canvasGroup.DOFade(1, .5f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            UISeletedData.Instance.onChangeColor.RemoveAllListeners();
            UISeletedData.Instance.onChangeColor.AddListener(co => ImageColor = co);
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

        //TODO
        foreach (Page2Data t in UISeletedData.Instance.PageDatas.PageDatas[2].dPage2)
        {
            GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.LeftButton);
            go.AddComponent<ParameterButton>().Init(t, UISeletedData.Instance.PageDatas.PageDatas[2].PageIdx);
            //go.GetComponent<Button>().onClick.AddListener(()=> go.GetComponent<ParameterButton>().ButtonClick());
            go.SetActive(true);
            go.GetComponent<RectTransform>().SetParent(LiftGidePanel.transform, false);
            go.GetComponent<RectTransform>().anchoredPosition = UIPositionManager.Instance.SecondtPanelPositionList[i];
            i++;
        }

        LiftGidePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 40 + 117);
        float x = LiftGidePanel.GetComponent<RectTransform>().sizeDelta.x;
        float y = LiftGidePanel.GetComponent<RectTransform>().sizeDelta.y;
        LiftGidePanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(x / 2f, -y / 2, 0);
        GetComponent<RectTransform>().SetAsFirstSibling();
        //ColorBar.transform.SetAsFirstSibling();
        //LiftGidePanel.GetComponent<RectTransform>().SetAsFirstSibling();
        //Debug.Log(gameObject.GetComponent<RectTransform>().GetSiblingIndex());
    }

    /// <summary>
    /// 选颜色按钮点击
    /// </summary>
    public void ColorButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ColorPickerPanel);
        UISeletedData.Instance.CurrentParameterImage = Image;
        //UISeletedData.Instance.CurrentImage = Image;
        //UISeletedData.Instance.CurrentColor = Activator.CreateInstance(ImageColor);
    }

    public void NoteButtonClick()
    {

    }

   
}
