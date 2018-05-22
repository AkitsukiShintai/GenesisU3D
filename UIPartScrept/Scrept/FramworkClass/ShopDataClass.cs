using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopDataClass
{
    public List<Commodity> recommend;
    public List<SingleSiftingData> siftingData;
}

[Serializable]
public class Commodity
{
    public string showName;//展示名字
    public string imagePath;//对应图片名字
    
}

/// <summary>
/// 筛选内容
/// </summary>
[Serializable]
public class SingleSiftingData
{
    public string root;
    public string showName;
}

