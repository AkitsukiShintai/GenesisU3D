using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class UIManager
{
    /// <summary>
    /// 单例
    /// </summary>
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }




    private Dictionary<UIPanelType, string> panelPathDic;
    private Dictionary<UIPanelType, BasePanel> panelDic;
    private Transform canvasTransform;
    private Stack<BasePanel> panelStack;

    /// <summary>
    /// 入栈，显示界面
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        
        BasePanel panel = GetPanel(panelType);
        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {

            //页面暂停
            
            BasePanel topPanel = panelStack.Peek();
            if (topPanel == panel)
            {
                //Debug.Log("同一个面板");
               // topPanel.OnContinue();
                //Debug.Log("当前栈数量" + panelStack.Count);
                PopPanel();
                return;
            }

            

            if (panelType == UIPanelType.SaveMenuPanel || panelType == UIPanelType.ColorPickerPanel)
            {
                topPanel.OnPause();
                panel.OnShow();
                panelStack.Push(panel);
                //Debug.Log("当前栈数量" + panelStack.Count);
                return;
            }
            //topPanel.OnDelete();

            if (topPanel.GetComponent<BasePanel>() is SaveMenuPanel)
            {
                PopPanel();
                PushPanel(panelType);
                //Debug.Log("当前栈数量" + panelStack.Count);

                return;
            }


        }
        PopPanel();
        panel.OnShow();
        panelStack.Push(panel);
        //Debug.Log("当前栈数量" + panelStack.Count);
        //panel.OnShow();

    }

    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        
        topPanel.OnDelete();

        //栈第二个页面继续运行
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnContinue();
        //Debug.Log("当前栈数量" + panelStack.Count);
    }


    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDic == null)
        {
            panelDic = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDic.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDic.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            //instPanel.transform.localPosition = new Vector3(27.5f, -98.5f, 0);
            //Debug.Log(instPanel.transform.localPosition);
            panelDic.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }

        return panel;


    }

    private Transform CanvasTransform
    {
        get
        {

            if (canvasTransform == null)
            {
                canvasTransform = GameObject.FindGameObjectWithTag("UIRoot").transform;
            }

            return canvasTransform;
        }
    }







    private void ParseUIPanelTypeJson()
    {
        panelPathDic = new Dictionary<UIPanelType, string>();

        List<UIPanelInfo> jsonObject = LoadJson.LoadJsonFromFile<List<UIPanelInfo>>(Application.streamingAssetsPath + "/UIPart/UIPanelType.json");

        foreach (UIPanelInfo info in jsonObject)
        {
            //Debug.Log(info.panelType);
            panelPathDic.Add(info.type, info.path);
        }
    }

    public BasePanel GetPanelInstance(UIPanelType type)
    {
        return GetPanel(type);                           
    }
}


[Serializable]
public class UIPanelInfo
{
    public string path;
    public UIPanelType type;

}

