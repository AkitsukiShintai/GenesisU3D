using System;
using Page;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 保存所有的UI所对应的显示信息
/// </summary>
public class UISeletedData
{
    /// <summary>
    /// 单例
    /// </summary>
    private static UISeletedData _instance;

    public static UISeletedData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UISeletedData();
            }
            return _instance;
        }
    }

    private UISeletedData()
    {
        PageDatas.InitData();
    }

    #region 颜色选择事件

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
    public Color SelectedColor
    {
        get { return _SelectedColor; }
        set
        {
            _SelectedColor = value;
            if (onChangeColor != null)
            {
                onChangeColor.Invoke(value);
            }
        }
    }

    #endregion




    //public BasePanel seletedPanel;

    /// <summary>
    /// 选材料第一个跟按钮初状态
    /// </summary>
    public bool isSeInit = false;

    /// <summary>
    /// 所有页面的信息
    /// </summary>
    public PageData PageDatas = new PageData();


    /// <summary>
    /// 调色板当前选中的颜色
    /// </summary>
    private Color _SelectedColor = Color.blue;
    /// <summary>
    /// 调色板返回后需要修改颜色的Image
    /// </summary>
    public Image CurrentParameterImage;

    /// <summary>
    /// 当前选择的对象
    /// </summary>
    public GameObject nowObject; //EventSystem.current.currentSelectedGameObject;   

   
    public object CurrentColor = null;
}




