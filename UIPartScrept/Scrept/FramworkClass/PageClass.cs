using System.Collections;
using System.Collections.Generic;
using GenesisWinForm;
using GenesisWinForm.G3DObject;
using GenesisWinForm.G3DObject.Parameter;
using UnityEngine;

namespace Page
{
    public class Page1Data
    {
        /// <summary>
        /// 0选材料 1选型调整 2结构与装饰 3工艺 4配色
        /// </summary>
        public int PageIdx;
        public List<Page2Data> dPage2 = new List<Page2Data>();

        public Page1Data(int _PageIdx, params Page2Data[] page2Data)
        {
            PageIdx = _PageIdx;
            for (int i = 0; i < page2Data.Length; i++)
            {
                dPage2.Add(page2Data[i]);
            }
        }
    }

    /// <summary>
    /// 包含按钮对应的部位，参数面板名字和对应的按钮对象
    /// </summary>
    public class Page2Data
    {

        /// <summary>
        /// 需要高亮显示的部位
        /// </summary>
        public List<G3DObjectPartBase> HightlightClass = new List<G3DObjectPartBase>();
        //public List<Page3Data> dPage3 = new List<EditStructTree.Page3Data>();
        /// <summary>
        /// button点击对应部位名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 对应鞋子参数部位
        /// </summary>
        public G3DObjectPartBase ParameteraboutShoe;


        private List<ParameterBase> _FabricParameterList = new List<ParameterBase>();


        public List<ParameterBase> FabricParameterList
        {
            get { return _FabricParameterList; }
        }

        public Page2Data(string name, G3DObjectPartBase part, G3DObjectPartBase _hightlightPart)
        {

            Name = name;

            ParameteraboutShoe = part;
            HightlightClass.Add(_hightlightPart);
            //HightlightClass.Add(_HightlightClass2);        
        }

        public Page2Data(string name, G3DObjectPartBase part, G3DObjectPartBase _hightlightPart1, G3DObjectPartBase _hightlightPart2)
        {
            Name = name;

            ParameteraboutShoe = part;
            HightlightClass.Add(_hightlightPart1);
            HightlightClass.Add(_hightlightPart2);
        }

        /// <summary>
        /// 测试用初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="object"></param>
        public Page2Data(string name)
        {
            Name = name;
            //HightlightClass.Add(_HightlightClass2);        
        }

        /// <summary>
        /// 只有当page为配色时调用，初始化FabricParameterList
        /// </summary>
        public void GetFabricParameterList()
        {
            for (int i = 0; i < ParameteraboutShoe.ParameterList.Count; i++)
            {

                if (!ParameteraboutShoe.ParameterList[i].IsShow)
                    continue;
                if (ParameteraboutShoe.ParameterList[i].ShowPosX != -1)
                    continue;

                if (ParameteraboutShoe.ParameterList[i].ShowInterfacePart ==
                    ParameterBase.ShowInterfacePositon.Fabric && ParameteraboutShoe.ParameterList[i].IsShowInEdit)
                {
                    FabricParameterList.Add(ParameteraboutShoe.ParameterList[i]);
                }

            }
        }
    }


    public class PageData
    {
        public List<Page1Data> PageDatas = new List<Page1Data>();

        public void InitData()//TODO
        {
            G3DObjectShoe Shoe = ApplicationManager.Instance.GetCreaterManager().ObjShoe;
            if (Shoe == null)
            {
                return;
            }

            //选材料            
            {
                Page1Data p = new Page1Data(0,
                    new Page2Data("跟"),
                    new Page2Data("底"),
                    new Page2Data("水台"),
                    new Page2Data("帮面"));
                PageDatas.Add(p);
            }
            //楦型调整            
            {

                Page2Data d1 = new Page2Data("跟", Shoe.Heel.Subject, Shoe.Heel);
                Page2Data d2 = new Page2Data("大底", Shoe.Sole.Outsole, Shoe.Sole.Outsole);
                Page2Data d3 = new Page2Data("成型底", Shoe.Sole.Unitsole, Shoe.Sole.Outsole, Shoe.Heel.Subject);
                Page2Data d4 = new Page2Data("鞋条", Shoe.Sole.Glue, Shoe.Sole.Glue);
                Page2Data d5 = new Page2Data("膛底", Shoe.Sole.Inside, Shoe.Sole.Inside);
                Page2Data d6 = new Page2Data("鞋头", Shoe.Vamp.Subject.Head, Shoe.Vamp);
                Page2Data d7 = new Page2Data("水台", Shoe.Sole.Platform, Shoe.Sole.Platform);

                Page2Data d8 = null;
                Page2Data d9 = null;
                Page2Data d10 = null;

                if (Shoe.Vamp.Subject.Vamp_Type.Value == 0)
                {
                    d8 = new Page2Data("帮面", Shoe.Vamp.Subject, Shoe.Vamp.Subject, Shoe.Vamp.EdgePiping);
                    d9 = new Page2Data("内里", Shoe.Vamp.Inside, Shoe.Vamp.Inside);
                    d10 = new Page2Data("鞋舌", Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue);
                }
                else
                {
                    d8 = new Page2Data("帮面", Shoe.Vamp.Subject, Shoe.Vamp.Subject, Shoe.Vamp.EdgePiping);
                    d9 = new Page2Data("领口里", Shoe.Vamp.Inside.InsideNeck, Shoe.Vamp.Inside.InsideNeck);
                    d10 = new Page2Data("鞋舌", Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue);
                }

                Page1Data p = new Page1Data(1, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10);
                PageDatas.Add(p);
            }
            //结构与装饰
            {
                Page1Data p = new Page1Data(2, new Page2Data("结构线"), new Page2Data("装饰线"));
                PageDatas.Add(p);
            }
            //工艺
            {
                Page2Data d1 = new Page2Data("跟", Shoe.Heel.Subject, Shoe.Heel);
                Page2Data d2 = new Page2Data("大底", Shoe.Sole.Outsole, Shoe.Sole.Outsole);
                Page2Data d3 = new Page2Data("成型底", Shoe.Sole.Unitsole, Shoe.Sole.Outsole, Shoe.Heel.Subject);
                Page2Data d4 = new Page2Data("鞋条", Shoe.Sole.Glue, Shoe.Sole.Glue);
                Page2Data d5 = new Page2Data("膛底", Shoe.Sole.Inside, Shoe.Sole.Inside);
                Page2Data d6 = new Page2Data("鞋头", Shoe.Vamp.Subject.Head, Shoe.Vamp);
                Page2Data d7 = new Page2Data("水台", Shoe.Sole.Platform, Shoe.Sole.Platform);

                Page2Data d8 = null;
                Page2Data d9 = null;
                Page2Data d10 = null;

                if (Shoe.Vamp.Subject.Vamp_Type.Value == 0)
                {
                    d8 = new Page2Data("帮面", Shoe.Vamp.Subject, Shoe.Vamp.Subject, Shoe.Vamp.EdgePiping);
                    d9 = new Page2Data("内里", Shoe.Vamp.Inside, Shoe.Vamp.Inside);
                    d10 = new Page2Data("鞋舌", Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue);
                }
                else
                {
                    d8 = new Page2Data("帮面", Shoe.Vamp.Subject, Shoe.Vamp.Subject, Shoe.Vamp.EdgePiping);
                    d9 = new Page2Data("领口里", Shoe.Vamp.Inside.InsideNeck, Shoe.Vamp.Inside.InsideNeck);
                    d10 = new Page2Data("鞋舌", Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue, Shoe.Vamp.Subject.Tongue);
                }

                Page1Data p = new Page1Data(3, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10);
                PageDatas.Add(p);
            }

            {//配色
                Page2Data d1 = new Page2Data("跟", Shoe.Heel.Subject, Shoe.Heel);
                d1.GetFabricParameterList();
                Page2Data d2 = new Page2Data("大底", Shoe.Sole.Outsole, Shoe.Sole.Outsole);
                d2.GetFabricParameterList();
                Page2Data d3 = new Page2Data("成型底", Shoe.Sole.Unitsole, Shoe.Sole.Outsole, Shoe.Heel.Subject);
                d3.GetFabricParameterList();
                Page2Data d4 = new Page2Data("鞋条", Shoe.Sole.Glue, Shoe.Sole.Glue);
                d4.GetFabricParameterList();
                Page2Data d5 = new Page2Data("膛底", Shoe.Sole.Inside, Shoe.Sole.Inside);
                d5.GetFabricParameterList();
                Page2Data d6 = new Page2Data("鞋头", Shoe.Vamp.Subject.Head, Shoe.Vamp);
                d6.GetFabricParameterList();
                Page2Data d7 = new Page2Data("水台", Shoe.Sole.Platform, Shoe.Sole.Platform);
                d7.GetFabricParameterList();
                Page1Data p = new Page1Data(4, d1, d2, d3, d4, d5, d6, d7);
                PageDatas.Add(p);
            }

        }

    }



}

