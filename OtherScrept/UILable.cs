using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILable : MonoBehaviour
{

    private GameObject text;
    public UIText uIText;
    //private Image image;
    public UIImage image;

    private void Awake()
    {
        text = transform.Find("Text").gameObject;
        uIText = GetComponentInChildren<UIText>();
        if (uIText == null) {
            uIText = gameObject.transform.Find("Text").gameObject.AddComponent<UIText>();
        }
        image = GetComponent<UIImage>();

        if (image == null)
        {
            image = gameObject.AddComponent<UIImage>();
        }
    }


    /// <summary>
    /// 初始化Lable，通过图片路径，包括背景Image显示和text,需要详细的设置text用uIText的Character和Paragraph方法来修改
    /// </summary>
    public void InitLable(string _text, string _imagePath)
    {
        //text = transform.Find("Text").gameObject;
        //uIText = GetComponentInChildren<UIText>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (ParameterTest.UIRoot != null)
        {
            rectTransform.parent = ParameterTest.UIRoot.transform;
        }
        else
        {
            Debug.Log("UIROOT没有获取到");
            return;
            //rectTransform.parent = _father.transform;
        }
        rectTransform.localPosition = Vector3.zero;
        if (_imagePath == null)
        {

        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>(_imagePath);
            if (image != null)
            {
                image.SetImage(sprite);
            }
            else
            {
                Debug.Log("加载图片出错");
            }

            GetComponent<Image>().enabled = true;

        }

        uIText.SetCharacter(_text);
    }


    /// <summary>
    /// 初始化Lable，最基本的方式通过text,需要详细的设置text用uIText的Character和Paragraph方法来修改,需要修改image通过image属性修改
    /// </summary>
    public void InitLable(string _text)
    {
        //text = transform.Find("Text").gameObject;
        //uIText = GetComponentInChildren<UIText>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (ParameterTest.UIRoot != null)
        {
            rectTransform.parent = ParameterTest.UIRoot.transform;
        }
        else
        {
            Debug.Log("UIROOT没有获取到");
            return;
            //rectTransform.parent = _father.transform;
        }
        rectTransform.localPosition = Vector3.zero;      
        uIText.SetCharacter(_text);
    }

    /// <summary>
    /// 初始化Lable，通过图片对象，包括背景Image显示和text,需要详细的设置text用uIText的Character和Paragraph方法来修改,需要修改image通过image属性修改
    /// </summary>
    public void InitLable(string _text, Sprite _sprite)
    {
        //text = transform.Find("Text").gameObject;
        //uIText = GetComponentInChildren<UIText>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (ParameterTest.UIRoot != null)
        {
            rectTransform.parent = ParameterTest.UIRoot.transform;
        }
        else
        {
            Debug.Log("UIROOT没有获取到");
            return;
            //rectTransform.parent = _father.transform;
        }
        rectTransform.localPosition = Vector3.zero;

        image.SetImage( _sprite);
        GetComponent<Image>().enabled = true;
        uIText.SetCharacter(_text);
    }

    /// <summary>
    /// 设定Lable位置，用于自适应
    /// </summary>
    public void SetPositionFit(GameObject _father)
    {
        //text = transform.Find("Text").gameObject;
        //uIText = GetComponentInChildren<UIText>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (_father == null)
        {
            rectTransform.parent = ParameterTest.UIRoot.transform;
        }
        else
        {
            if (!gameObject.GetComponent < LayoutElement>())
            {
                gameObject.AddComponent<LayoutElement>();
            }
            rectTransform.parent = _father.transform;
        }
        //rectTransform.localPosition = _localPosition;

        //uIText.Character(_text);
    }


}
