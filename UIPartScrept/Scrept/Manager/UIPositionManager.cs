using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 记录所有UI位置
/// </summary>
public class UIPositionManager : Singleton<UIPositionManager>
{

    /// <summary>
    /// 二级面板下UI anchor为左上角，顺序排列时Y的坐标值
    /// </summary>
    public readonly List<Vector3> SecondtPanelPositionList = new List<Vector3> { new Vector3(-4.7f, -96), new Vector3(-4.7f, -136), new Vector3(-4.7f, -176), new Vector3(-4.7f, -216), new Vector3(-4.7f, -256), new Vector3(-4.7f, -296), new Vector3(-4.7f, -336), new Vector3(-4.7f, -376), new Vector3(-4.7f, -416), new Vector3(-4.7f, -456), new Vector3(-4.7f, -496), new Vector3(-4.7f, -536) };
    //public float SeletMatPanel_X = -4.7f;


    /// <summary>
    /// 商店商品窗口刷新位置
    /// </summary>
    public readonly List<List<Vector3>> ShopCommodityWindowPosition = new List<List<Vector3>> { new List<Vector3> { new Vector3(-82, 195), new Vector3(8, 195), new Vector3(98, 195)}, new List<Vector3> { new Vector3(-82, 66), new Vector3(8, 66), new Vector3(98, 66)}, new List<Vector3> { new Vector3(-82, -63), new Vector3(8, -63), new Vector3(98, -63),}, new List<Vector3> { new Vector3(-82, -192), new Vector3(8, -192), new Vector3(98, -192) } };
    //public Button bt;

    /// <summary>
    /// 三级Dropdown菜单显示位置
    /// </summary>
    public readonly Vector3 ThirdDropdownPosition= new Vector3(50,0,0);

    
    //public readonly List<Vector3> ShopDropdowns = new List<Vector3>{};
    public void Start()
    {
        Vector3 x = new Vector3(-4.7f, -96);
        //x.x = 4;
        bool i = SecondtPanelPositionList.Contains(x);

        SecondtPanelPositionList.TrueForAll(xx => xx.x == 1);
    }
}

