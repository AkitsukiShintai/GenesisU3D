using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class ButtonExtention : Button
{

    private GameObject HoverTextGo;
    private GameObject textGo;
    private BoTree<string> root;
    private GameObject AdaptiveObject;
    /// <summary>
    /// 是否字体加强
    /// </summary>
    public bool isHoveringBoldFount =false;
    /// <summary>
    /// 是否显示文字悬浮窗口
    /// </summary>
    public bool isShowingSuspendWindow = false;
    /// <summary>
    /// 是否改变字体颜色
    /// </summary>
    public bool isChangingColor = false;
    /// <summary>
    /// 是否是在商店窗口按钮
    /// </summary>
    public bool isShopWindow = false;
 
    private bool isShowThirdLevelSift = false;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition( state, instant);
        switch (state)
        {
            case SelectionState.Normal:
                if (isHoveringBoldFount)
                {
                    gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
                }

                if (isShowingSuspendWindow)
                {
                    if (HoverTextGo == null) HoverTextGo = GameObject.FindGameObjectWithTag("HoverText");
                    HoverTextGo.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
                }

                if (isChangingColor)
                {
                    gameObject.GetComponentInChildren<Text>().color = Color.black;
                }

                if (isShopWindow)
                {
                    //gameObject.transform.Find("Text").gameObject.SetActive(false);
                    gameObject.GetComponentInChildren<Text>().color = Color.clear;
                }
                if (isShowThirdLevelSift)
                {
                    HideThirdLevelSift();
                }


                break;
            case SelectionState.Highlighted:
                if (isChangingColor)
                {
                    gameObject.GetComponentInChildren<Text>().color = Color.white;
                }
                if (isHoveringBoldFount)
                {
                    gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
                }

                if (isShopWindow)
                {
                    //gameObject.GetComponentInChildren<Text>().DOFade(1, 0.5f);
                    //gameObject.transform.Find("Text").gameObject.SetActive(true);
                    gameObject.GetComponentInChildren<Text>().color = Color.white;
                }
                if (isShowingSuspendWindow)
                {
                    HoverTextGo.transform.position = transform.position + Vector3.up * 30;
                    HoverTextGo.GetComponentInChildren<Text>().text = GetComponentInChildren<Text>().text;
                    HoverTextGo.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
                    HoverTextGo.transform.SetAsLastSibling();
                }
                if (isShowThirdLevelSift)
                {
                    ShowThirdLevelSift();
                }
                break;
            case SelectionState.Pressed:
                break;
            case SelectionState.Disabled:
                break;
        }
    }


    public void InitThirdSift(BoTree<string> _root)
    {
        isShowThirdLevelSift = true;
        //GetComponentInChildren<Text>().text = root
        root = _root;
        //onClick.AddListener(ChangeText);

    }

    private void ShowThirdLevelSift()
    {
        if (AdaptiveObject == null)
        {
            AdaptiveObject = GameObject.FindGameObjectWithTag("AdaptiveObject");
        }
        AdaptiveObject.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        AdaptiveObject.GetComponent<CanvasGroup>().interactable = true;
        AdaptiveObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        AdaptiveObject.AddComponent<ThirdLevelSift>().Init(root);
        // AdaptiveObject.transform.parent = transform;
        AdaptiveObject.transform.position = transform.position + Vector3.right * 90;
        AdaptiveObject.transform.SetAsLastSibling();
    }

    private void HideThirdLevelSift()
    {
        if (AdaptiveObject == null)
        {
            AdaptiveObject = GameObject.FindGameObjectWithTag("AdaptiveObject");
        }
        AdaptiveObject.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        AdaptiveObject.GetComponent<CanvasGroup>().interactable = false;
        AdaptiveObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    private void ChangeText()
    {
        transform.parent.parent.GetComponent<DropdownVersion_Self>().ShowName.text = root.Data;
    }
}






