using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保存所有的UI面板
/// </summary>
public class AllUIManager {

    /// <summary>
    /// 单例
    /// </summary>
    private static AllUIManager _instance;

    public static AllUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AllUIManager();
            }
            return _instance;
        }
    }

    private AllUIManager()
    {
        LoadDic();
    }


    //private Dictionary<UIObjectName, GameObject> AllUIObjectDic = new Dictionary<UIObjectName, GameObject>();
    private Dictionary<UIObjectName, string> UITypeToPathDic = new Dictionary<UIObjectName, string>();
    

    private void LoadDic() {

        List<UIObject> uIObject =  LoadJson.LoadJsonFromFile<List<UIObject>>(Application.streamingAssetsPath + "/UIPart/UIObject.json");
        //UIName = uIObject.name;
        foreach (UIObject t in uIObject) {

            UITypeToPathDic.Add(t.type,t.path);
        }
    }
    /// <summary>
    /// 根据需要的UI对象名字获取到UI物体
    /// </summary>
    /// <param name="name">UI名字</param>
    /// <returns></returns>
    public GameObject GetUIObject(UIObjectName name) {
        if (UITypeToPathDic.ContainsKey(name))
        {
            GameObject go = GameObject.Instantiate(Resources.Load(UITypeToPathDic[name])) as GameObject;
            //AllUIObjectDic.Add(name, go);
            return go;
        }
        else
        {
            Debug.Log("UI字典中没有该对象");
            GameObject go = new GameObject();

            return go;
        }


    }


}



[Serializable]
public class UIObject
{
    public string path;
    public UIObjectName type;

}
