using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

    //对Text组建进行再次封装
    private Text text = null;

    private void Awake()
    {
        Debug.Log("Awake");
        text = GetComponent<Text>();



        //CharacterBasicPropertise();
    }


    /// <summary>
    /// 对Character的操作，标准版本，包括字体，字体形式，字号，行间距，是否为富文本，其中显示文字text为必须参数
    /// </summary>
    public void SetCharacter(
        string _text = "",
        string _fontPath = "", //字体路径
        Font _font = null,//字体文件
        FontStyle _fontStyle = FontStyle.Normal, //字体形式，如加粗，倾斜
        int _fontSize = 0, //字号
        float _lineSpacing = 0f,//行间距
        bool _isRich = true, //是否为富文本
        Material _material = null)
    {
        if (text == null)
        {
            text = gameObject.AddComponent<Text>();
        }
        text.text = _text;
        if (_fontPath == "")
        {
            //defult
        }
        else
        {
            Font font = Resources.Load<Font>(_fontPath);
            text.font = font;
        }
        if (_font == null)
        {
            //defult
        }
        else
        {
            text.font = _font;
        }

        text.fontStyle = _fontStyle;

        if (_fontSize == 0)
        {
            //defult
        }
        else
        {
            text.fontSize = _fontSize;
        }

        if (_lineSpacing == 0f)
        {
            //defult
        }
        else
        {
            text.lineSpacing = _lineSpacing;

        }
        text.supportRichText = _isRich;

        if (_material == null)
        {
            //defult
        }
        else
        {
            text.material = _material;
        }


    }


    /// <summary>
    /// 对Character的操作，设置字号和行间距
    /// </summary>
    public void SetCharacter(int _fontSize, //字号
        float _lineSpacing//行间距
        )
    {
        text.lineSpacing = _lineSpacing;
        text.fontSize = _fontSize;

    }

    /// <summary>
    /// 对Character的操作，设置字体
    /// </summary>
    public void SetCharacter(Font _font)
    {
        text.font = _font;
    }

    /// <summary>
    /// 对Character的操作，设置颜色
    /// </summary>
    public void SetCharacter(Color _color)
    {
        text.color = _color;
    }

    /// <summary>
    /// 对Character的操作，在标准版本之上加入了字体颜色，为必须参数
    /// </summary>
    public void SetCharacter(
        string _text,
        Color _color,
        string _fontPath = "",
        Font _font = null,
        FontStyle _fontStyle = FontStyle.Normal,
        int _fontSize = 0,
        float _lineSpacing = 0f,
        bool _isRich = true,
        Material _material = null)
    {

        text = GetComponent<Text>();
        if (text == null)
        {
            text = gameObject.AddComponent<Text>();
        }

        text.color = _color;
        text.text = _text;


        if (_fontPath == "")
        {
            //defult
        }
        else
        {
            Font font = Resources.Load<Font>(_fontPath);
            text.font = font;
        }
        if (_font == null)
        {
            //defult
        }
        else
        {
            text.font = _font;
        }

        text.fontStyle = _fontStyle;

        if (_fontSize == 0)
        {
            //defult
        }
        else
        {
            text.fontSize = _fontSize;
        }

        if (_lineSpacing == 0f)
        {
            //defult
        }
        else
        {
            text.lineSpacing = _lineSpacing;

        }
        text.supportRichText = _isRich;
        if (_material == null)
        {
            //defult
        }
        else
        {
            text.material = _material;
        }

    }

    /// <summary>
    /// 对Paragraph的操作
    /// </summary>
    public void SetParagraph(
        TextAnchor _textAlignment = TextAnchor.MiddleCenter,//段落对齐方式
        HorizontalWrapMode _horizontalWrapMode = HorizontalWrapMode.Overflow,//段落水平回卷模式
        VerticalWrapMode _verticalWrapMode = VerticalWrapMode.Truncate,//段落垂直回卷模式   
        bool _bestFit = false,//是否字号自适应
        int _minSize = 0,//自适应最小字号
        int _maxSize = 0,//自适应最大字号          
        bool _alignByGeometry = false
        )
    {
        text.alignment = _textAlignment;
        text.alignByGeometry = _alignByGeometry;
        text.horizontalOverflow = _horizontalWrapMode;
        text.verticalOverflow = _verticalWrapMode;
        if (_bestFit == true)
        {
            if (_minSize > 0 && _maxSize >= _minSize && _maxSize > 1)
            {
                text.resizeTextForBestFit = true;
                text.resizeTextMaxSize = _maxSize;
                text.resizeTextMinSize = _minSize;
            }
            else
            {
                //defult
                text.resizeTextForBestFit = false;
            }
        }
    }


    /// <summary>
    /// 对Paragraph的操作,设置段落对齐方式
    /// </summary>
    public void SetParagraph(TextAnchor _textAlignmen)
    {
        text.alignment = _textAlignmen;
    }

    /// <summary>
    /// 对Paragraph的操作,设置段落垂直回滚方式
    /// </summary>
    public void SetParagraph(HorizontalWrapMode _horizontalWrapMode)
    {
        text.horizontalOverflow = _horizontalWrapMode;
    }

    /// <summary>
    /// 对Paragraph的操作,设置段落水平回滚方式
    /// </summary>
    public void SetParagraph(VerticalWrapMode _verticalWrapMode)
    {
        text.verticalOverflow = _verticalWrapMode;
    }


    /// <summary>
    /// 对Paragraph的操作,设置字体自适应大小
    /// </summary>
    public void SetParagraph(int _minSize, int _maxSize)
    {
        text.resizeTextForBestFit = true;
        if (_minSize > 0 && _maxSize >= _minSize && _maxSize > 1)
        {
            text.resizeTextForBestFit = true;
            text.resizeTextMaxSize = _maxSize;
            text.resizeTextMinSize = _minSize;
        }
        else
        {
            //defult
            text.resizeTextForBestFit = false;
            Debug.Log("设置字体自适应BestFit失败");
        }
    }


    /// <summary>
    /// 初始化Text,传入参数为需要放置的父物体对象以及localPosition，如果是Lable等prefab上的text则不需要用到
    /// </summary>
    public void InitText(string _text)
    {  
        text.text = _text;
    }


  
    /// <summary>
    /// 设置Text位置，用于做自适应
    /// </summary>
    public void SetPositionFit(GameObject _fitFather) {

        if (_fitFather.GetComponent<VerticalLayoutGroup>() == null || _fitFather.GetComponent<GridLayoutGroup>() == null || _fitFather.GetComponent<HorizontalLayoutGroup>() == null)
        {
            Debug.Log("Cannot find the Father's any <LayoutGroup> component, please check.");
            return;
        }
        else
        {
            if (GetComponent<LayoutElement>() == null)
            {
                gameObject.AddComponent<LayoutElement>();
            }
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.parent = _fitFather.transform;
        }
    }
}
