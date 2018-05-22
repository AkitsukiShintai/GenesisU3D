using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class ShopPanel : BasePanel
{

    [SerializeField] private Button recommondButton;
    [SerializeField] private List<DropdownExtention> dropdownExtentionList;
    [SerializeField] private PageGuid pg;
    [SerializeField] private Transform dropdowns;

    private BoTree<string> siftingData = new BoTree<string>("Root");
    private List<GameObject> windows = new List<GameObject>();
    private ShopDataClass shopData = new ShopDataClass();
    private int siftIndex = 0;//筛选顺序
    public override void OnShow()
    {
        if (state == ShowState.Showing)
        {
            return;
        }

        if (state == ShowState.None)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, .5f);
            GetComponent<RectTransform>().DOAnchorPosX(-163, 0.5f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            RushAll();
            state = ShowState.Showing;
            pg.onChangePage.AddListener(x => SortWindows());
        }
        if (state == ShowState.Unable)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, .5f);
            GetComponent<RectTransform>().DOAnchorPosX(-163, 0.5f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            RushAll();
            state = ShowState.Showing;
        }


    }

    public override void OnContinue()
    {
        //FormationAdjustButton.Select();
        if (state == ShowState.Showing) return;
        canvasGroup.DOFade(1, .5f);
        GetComponent<RectTransform>().DOAnchorPosX(-163, 0.5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //SortWindows();
        //InitSiftingData();
        //ShowSiftingData
        state = ShowState.Showing;
    }

    public override void OnDelete()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.DOFade(0, .5f);
        GetComponent<RectTransform>().DOAnchorPosX(150, 0.5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        state = ShowState.Unable;
    }

    public override void OnPause()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.DOFade(0.5f, .5f);
        GetComponent<RectTransform>().DOAnchorPosX(150, 0.5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        state = ShowState.Pausing;
    }


    public void Init(string json = null)
    {
       
        if (json == null)
        {
            shopData = LoadJson.LoadJsonFromFile<ShopDataClass>(Application.streamingAssetsPath + "/UIPart/testShopJson.json");          
        }
        else
        {
            shopData = LoadJson.LoadJsonFromNet<ShopDataClass>(json);
            recommondButton.Select();
            pg.Init(shopData.recommend);
        }
    }
    ///页面排序

    private void SortWindows(int part = 0)//参数为显示哪块，比如推荐，筛选或收藏,
    {
        //清理content
        List<GameObject> tGameObjects = new List<GameObject>();

        for (int i = 0; i < windows.Count; i++)
        {
            tGameObjects.Add(windows[i]);

        }
        for (int i = 0; i < tGameObjects.Count; i++)
        {
            Destroy(tGameObjects[i].gameObject);
        }

        //TODO 判断刷新哪个面板
        
        List<Commodity> tlist = new List<Commodity>();
        if (part == 0)
        {
            tlist = shopData.recommend;
        }else if (part == 1)//筛选
        {
        }else if (part == 2)//收藏
        {

        }

        int dd = 0;
        int current = 0;
        int minIndex = (pg.Number - 1) * 12 + 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                current = minIndex + i * 3 + j;
                //Debug.Log("current:"+current);
                if (tlist.Count < current)
                {
                    return;
                }
                GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.CommodityWindow);
                dd++;
                windows.Add(go);
                //Debug.Log("实例化"+dd);
                go.transform.SetParent(this.transform);
                go.transform.localPosition = UIPositionManager.Instance.ShopCommodityWindowPosition[i][j];
                go.GetComponent<CommodityWindowPiece>().Init(shopData.recommend[current - 1]);//TODO

            }
        }
    }

    /// <summary>
    /// 将筛选信息初始化为树结构
    /// </summary>
    private void InitSiftingData()
    {
        for (int i = 0; i < shopData.siftingData.Count; i++)
        {
            BoTree<string> temp = new BoTree<string>(shopData.siftingData[i].showName);

            if (shopData.siftingData[i].root == "")
            {
                siftingData.AddNode(temp);
            }
            else
            {
                BoTree<string> tempRoot = new BoTree<string>();
                BoTree<string>.Find(siftingData, shopData.siftingData[i].root, ref tempRoot);
                if (tempRoot.Data == null)
                {
                    throw new ArgumentOutOfRangeException("不存在 " + shopData.siftingData[i].root + " 根节点");
                }
                else
                {
                    tempRoot.AddNode(temp);
                }
            }


        }
    }
    /// <summary>
    /// 刷新筛选按钮
    /// </summary>
    private void ShowDropDown()
    {
        //清理content
        List<GameObject> tGameObjects = new List<GameObject>();

        foreach (Transform t in dropdowns)
        {

            tGameObjects.Add(t.gameObject);
        }
        for (int i = 0; i < tGameObjects.Count; i++)
        {
            Destroy(tGameObjects[i].gameObject);
        }

        for (int i = 0; i < siftingData.Nodes.Count; i++)
        {
            GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.UIDropdownSelfVersionComponent);
            go.GetComponent<DropdownVersion_Self>().Init(siftingData.Nodes[i]);
            go.transform.SetParent(dropdowns);
            go.transform.SetSiblingIndex(siftIndex);
            siftIndex++;
        }
        dropdowns.transform.SetAsLastSibling();
    }
    //切换面板时调用
    private void RushAll()
    {
        recommondButton.Select();
        pg.Init(shopData.recommend);
        InitSiftingData();
        SortWindows();
        ShowDropDown();
    }

    //界面数据变化时调用
    private void RushWindows(int part = 0)
    {
        List<Commodity> tlist = new List<Commodity>();
        if (part == 0)
        {
            tlist = shopData.recommend;
        }
        else if (part == 1)//筛选
        {
        }
        else if (part == 2)//收藏
        {

        }
        // List<Commodity> d = new List<Commodity>();
        pg.Init(tlist);
        SortWindows(part);
    }


    public void RecommendButtonClick()
    {
        RushWindows(0);
    }
    
    public void CollectionButtonClick()
    {
        RushWindows(2);
    }
}
