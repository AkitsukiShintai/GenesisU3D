using System.Collections.Generic;
using DG.Tweening;
using GenesisWinForm.G3DObject;
using GenesisWinForm.G3DObject.Parameter;
using Page;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorMatchParameterPanel : BasePanel, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private G3DObjectPartBase aboutShoe;
    [SerializeField]
    private GameObject Contant;

    [SerializeField] private Text Name;
    //[SerializeField] private Button FormationAdjustButton;
    //[SerializeField] private Button DetailAdjustButton;

    private float HeightOfContant = 300;
    private float AllHeightOfChildren = 0;
    private Page2Data pgData;


    #region override BasePanel
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
        RushALL();
        state = ShowState.Showing;
    }

    public override void OnContinue()
    {
        //FormationAdjustButton.Select();
        if (state == ShowState.Showing) return;
        canvasGroup.DOFade(1, .5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        RushALL();
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
#endregion

    #region 拖拽
    // begin dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    // during dragging
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    // end dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    /// <summary>
    /// set position of the dragged game object
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();

        // transform the screen point to world point int rectangle
        Vector3 globalMousePos;
        //把屏幕坐标转换为UGUI对应的坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos + Vector3.down * 190;
        }
    }
    #endregion



    public void Init(Page2Data _pgData)
    {
        aboutShoe = _pgData.ParameteraboutShoe;
        pgData = _pgData;
        //形态调整显示切换
        Name.text = pgData.Name + "调整";
        //RushALL();
    }



    /// <summary>
    /// 整体调整
    /// </summary>
    void RushALL()
    {
        //清理content
        List<GameObject> tGameObjects = new List<GameObject>();
        foreach (Transform VARIAtBLE in Contant.transform)
        {
            tGameObjects.Add(VARIAtBLE.gameObject);
        }
        for (int i = 0; i < tGameObjects.Count; i++)
        {
            Destroy(tGameObjects[i].gameObject);
        }
        //遍历所有参数，根据参数不同进行初始化
        for (int i = 0; i < aboutShoe.ParameterList.Count; i++)
        {
            if (!aboutShoe.ParameterList[i].IsShow)
                continue;
            if (aboutShoe.ParameterList[i].ShowPosX != -1)
                continue;

            if (aboutShoe.ParameterList[i].ShowInterfacePart == ParameterBase.ShowInterfacePositon.Fabric && aboutShoe.ParameterList[i].fabricParameterType == ParameterBase.FabricParameterType.Parameter && aboutShoe.ParameterList[i].IsShowInEdit)
            {


                if (aboutShoe.ParameterList[i].ValueType == ParameterBase.eValueType.Double
                    && !((ParameterDouble)aboutShoe.ParameterList[i]).IsComboxDoubleUnion)
                {
                    GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.DoubleBox);
                    go.GetComponent<DoubleBox>().Init(aboutShoe.ParameterList[i], aboutShoe);
                    go.transform.SetParent(Contant.transform, false);
                    //根据子物体高度重新设置Contant位置
                    AllHeightOfChildren += go.GetComponent<RectTransform>().sizeDelta.y;
                    if (AllHeightOfChildren > HeightOfContant)
                    {
                        Contant.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -9 + (HeightOfContant - AllHeightOfChildren) / 2);
                    }
                }

                else if (aboutShoe.ParameterList[i].ValueType == ParameterBase.eValueType.Combo)
                {
                    ParameterCombo com = (ParameterCombo)aboutShoe.ParameterList[i];
                    if (com.ComboxDoubleUnion.Count > 0)//选择+滚动条类型
                    {

                        GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.ComboBoxDouble);
                        go.GetComponent<ComboBoxDouble>().Init(aboutShoe.ParameterList[i], aboutShoe);
                        go.transform.SetParent(Contant.transform, false);
                        //根据子物体高度重新设置Contant位置
                        AllHeightOfChildren += go.GetComponent<RectTransform>().sizeDelta.y;
                        if (AllHeightOfChildren > HeightOfContant)
                        {
                            Contant.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -9 + (HeightOfContant - AllHeightOfChildren) / 2);
                        }
                    }
                    else if (com.Items.Length > 8)//TODO 选择为最多个数的类型
                    {

                        GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.ComboBoxMultiple);
                        go.GetComponent<ComboBoxMultiple>().Init(aboutShoe.ParameterList[i], aboutShoe);
                        go.transform.SetParent(Contant.transform, false);
                        //根据子物体高度重新设置Contant位置
                        AllHeightOfChildren += go.GetComponent<RectTransform>().sizeDelta.y;
                        if (AllHeightOfChildren > HeightOfContant)
                        {
                            Contant.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -9 + (HeightOfContant - AllHeightOfChildren) / 2);
                        }
                    }
                    else if (com.Items.Length > 4)//TODO 选择为中等个数的类型
                    {
                        GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.ComboBoxMid);
                        go.GetComponent<ComboBoxMid>().Init(aboutShoe.ParameterList[i], aboutShoe);
                        go.transform.SetParent(Contant.transform, false);
                        //根据子物体高度重新设置Contant位置
                        AllHeightOfChildren += go.GetComponent<RectTransform>().sizeDelta.y;
                        if (AllHeightOfChildren > HeightOfContant)
                        {
                            Contant.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -9 + (HeightOfContant - AllHeightOfChildren) / 2);
                        }
                    }
                    else //TODO 选择为最少的类型
                    {
                        GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.ComboBoxLess);
                        go.GetComponent<ComboBoxLess>().Init(aboutShoe.ParameterList[i], aboutShoe);
                        go.transform.SetParent(Contant.transform, false);
                        //根据子物体高度重新设置Contant位置
                        AllHeightOfChildren += go.GetComponent<RectTransform>().sizeDelta.y;
                        if (AllHeightOfChildren > HeightOfContant)
                        {
                            Contant.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -9 + (HeightOfContant - AllHeightOfChildren) / 2);
                        }
                    }
                }
                else if (aboutShoe.ParameterList[i].ValueType == ParameterBase.eValueType.Color4)
                {

                    GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.ComboBoxLess);
                    go.GetComponent<ColorSelect>().Init(aboutShoe.ParameterList[i], aboutShoe);
                    go.transform.SetParent(Contant.transform);
                    //根据子物体高度重新设置Contant位置
                    AllHeightOfChildren += go.GetComponent<RectTransform>().sizeDelta.y;
                    if (AllHeightOfChildren > HeightOfContant)
                    {
                        Contant.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -9 + (HeightOfContant - AllHeightOfChildren) / 2);
                    }
                }
            }


        }

    }


}
