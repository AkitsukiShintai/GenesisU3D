using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum UIPanelType {
    //panal名字
    Base,
    /// <summary>
    /// 选材料面板
    /// </summary>
    SeleteMatPanel,
    /// <summary>
    /// 主面板
    /// </summary>
    MainPanel,
    /// <summary>
    /// 保存菜单面板
    /// </summary>
    SaveMenuPanel,
    /// <summary>
    /// 结构与装饰面板
    /// </summary>
    StructureAndOrnementPanel,
    /// <summary>
    /// 配色面板
    /// </summary>
    ColorMatchPanel,
    /// <summary>
    /// 工艺面版
    /// </summary>
    TechnicPanel,
    /// <summary>
    /// 楦型调整面板
    /// </summary>
    LastAdjustPanel,
    /// <summary>
    /// 调色面板
    /// </summary>
    ColorPickerPanel,
    /// <summary>
    /// 楦型调整面板
    /// </summary>
    LastAdjustParameterPanel,
    /// <summary>
    /// 楦型调整参数面板
    /// </summary>
    StructureAndOrnementPerameterPanel,
    /// <summary>
    /// 保存面板
    /// </summary>
    SavePanel,
    /// <summary>
    /// 商城面板
    /// </summary>
    ShopPanel,
    /// <summary>
    /// 预览面板，五个点
    /// </summary>
    PerviewPanel,
    /// <summary>
    ///工艺参数面板
    /// </summary>
    TechnicParameterPanel,
    /// <summary>
    ///调色参数面板，ParameterBase.FabricParameterTpye .Parameter类型弹出的面板
    /// </summary>
    ColorMatchParameterPanel,
    /// <summary>
    ///结构与装饰中右键面板
    /// </summary>
    StructureRightClickPanel,
    /// <summary>
    ///警告面板
    /// </summary>
    WorningPanel
}

/// <summary>
/// 保存所有UI按钮的名字
/// </summary>
[Serializable]
public enum UIObjectName
{
    /// <summary>
    /// 商城界面单个商品窗口
    /// </summary>
    CommodityWindow,

    /// <summary>
    ///color类型控件-颜色选择
    /// </summary>
    DoubleBox ,
    /// <summary>
    ///color类型控件-颜色选择
    /// </summary>
    ColorSelect,
    /// <summary>
    /// combo类型控件-combo+double
    /// </summary>
    ComboBoxDouble,
    /// <summary>
    /// combo类型控件-大量选择
    /// </summary>
    ComboBoxMultiple,
    /// <summary>
    /// combo类型控件-少量选择
    /// </summary>
    ComboBoxLess,
    /// <summary>
    /// combo类型控件-中量选择
    /// </summary>
    ComboBoxMid,  
    /// <summary>
    /// 左边栏的Button Prefab
    /// </summary>
    LeftButton,
    /// <summary>
    /// Dropdown组件 Prefab
    /// </summary>
    UIDropdownComponent,
    /// <summary>
    /// 自适应父物体 Prefab
    /// </summary>
    AdaptiveObject,
    /// <summary>
    /// 商场筛选button Prefab
    /// </summary>
    SiftingButton,
    /// <summary>
    /// Dropdown手写组建 Prefab
    /// </summary>
    UIDropdownSelfVersionComponent,
}
 
public enum ShowState{
    None,
    Showing,
    Pausing,  
    Unable
}

/// <summary>
/// 鞋子的部位类型
/// </summary>
[Serializable]
public enum PartType
{
    //panal名字
    /// <summary>
    /// 选材料-跟按钮
    /// </summary>
    HeelOfSeButton,
    /// <summary>
    /// 选材料-大底按钮
    /// </summary>
    OutsoleOfSeButton,
    /// <summary>
    /// 楦型调整-底按钮
    /// </summary>
    EvaSoleOfLaButton,
    /// <summary>
    /// 楦型调整-水台按钮
    /// </summary>
    PlatformOfLaButton,
    /// <summary>
    /// 楦型调整-鞋头按钮
    /// </summary>
    ToeOfLaButton,
    /// <summary>
    /// 楦型调整-帮面按钮
    /// </summary>
    UpperOfLaButton,
    /// <summary>
    /// 选材料-片底按钮
    /// </summary>
    UnitSoleOfSeButton,
    /// <summary>
    /// combo类型控件-大量选择
    /// </summary>
    ComboBoxMultiple,
    /// <summary>
    /// combo类型控件-少量选择
    /// </summary>
    ComboBoxLess
}

/// <summary>
/// 选项类型（分为一级和二级）
/// </summary>
[Serializable]
public enum Options
{
    /// <summary>
    /// 一级选项-选材料
    /// </summary>
    SelectsMat,
    /// <summary>
    /// 一级选项-楦型调整
    /// </summary>
    LastAdjust,
    /// <summary>
    /// 一级选项-结构与装饰
    /// </summary>
    StructureAndOrnement,
    /// <summary>
    /// 一级选项-工艺
    /// </summary>
    Technic,
    /// <summary>
    /// 一级选项-配色
    /// </summary>
    ColorMatch,
    /// <summary>
    /// 二级选项-选材料-鞋面
    /// </summary>
    HeelOfSe,
    /// <summary>
    /// 二级选项-选材料-底
    /// </summary>
    SoleOfSe,
    /// <summary>
    /// combo类型控件-大量选择
    /// </summary>
    ComboBoxMultiple,
    /// <summary>
    /// combo类型控件-少量选择
    /// </summary>
    ComboBoxLess
}