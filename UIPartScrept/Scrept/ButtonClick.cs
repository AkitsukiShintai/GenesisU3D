using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenesisWinForm;
using GenesisWinForm.G3DObject;
using GenesisWinForm.G3DObject.ModelCreater;
using GenesisWinForm.G3DObject.Ornament;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{

    #region 选材料

    public void SeleteMatButtonClick()
    {
        //UIManager.Instance.PopPanel();
        UIManager.Instance.PushPanel(UIPanelType.SeleteMatPanel);
    }


    #endregion

    #region 保存面板


    public void SavePanelButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.SaveMenuPanel);
    }

    public void OpenButtonClick()
    {
    }

    public void SaveButtonClick()
    {
    }

    public void ExporButtonClick()
    {
    }

    public void SaveAsButtonClick()
    {
    }

    #endregion

    #region 预览选项



    #endregion

    #region 楦型调整


    #region 面板按钮

    public void LastAdjustButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.LastAdjustPanel);
    }










    #endregion

    #region 鞋面调整面板


    #endregion





    #endregion

    #region 结构与装饰

    public void StructureAndOrnementButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.StructureAndOrnementPanel);
    }

    #endregion

    #region 工艺

    public void TechnicButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.TechnicPanel);
    }



    #endregion


    #region 配色

    public void ColorMatchButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ColorMatchPanel);
    }


    #endregion

    #region 商城

    public void ShopButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ShopPanel);
    }

    #endregion

    #region 功能

    public void RecallButtonClick()
    {

    }

    public void RedoButton()
    {

    }

    #endregion


    public void TestCreateRibbon()
    {

        G3DObjectShoe Shoe = ApplicationManager.Instance.GetCreaterManager().ObjShoe;
        int groupID;
        G3DObjectPartBase.ICompute3DMesh++; ////6566666
        G3DObjectPartBase.IComputeMeshParameter++; ////6566666
        List<G3DObjectPartBase>
            addItems = SimCreateOrnament(1, OrnamentBase.OrnamentType.PomPom, out groupID); ////6566666
        G3DObjectPartBase.ICompute3DMesh--; ////6566666
        G3DObjectPartBase.IComputeMeshParameter--; ////6566666
        for (int i = 0; i < addItems.Count; i++)
        {
            Shoe.Vamp.Subject.PartChilden.Add(addItems[i]); ////6566666
        }

        Shoe.Vamp.Subject.OrnamentAddNumber += addItems.Count; ////6566666

        Shoe.Vamp.Subject.ComputeMeshParameterAnd3DMesh(); ////6566666
        ApplicationManager.Instance.GetCreaterManager().ShowMesh(); ////6566666
    }

    List<G3DObjectPartBase> SimCreateOrnament(int AddNumber, OrnamentBase.OrnamentType type, out int groupID)
    {
        G3DObjectPartBase PartAbout = ApplicationManager.Instance.GetCreaterManager().ObjShoe.Vamp.Subject;

        groupID = CommonFunction.GetTimeStamp();
        List<G3DObjectPartBase> addItems = new List<G3DObjectPartBase>();
        for (int i = 0; i < AddNumber; i++)
        {
            OrnamentBase ornOther = OrnamentBase.CreateByType(type, PartAbout); ////6566666
            ornOther.GroupID = groupID; ////6566666
            ornOther.SetG3DObjectIsChoose(PartAbout.GetIsChoose()); ////6566666
            ornOther.ParentID = PartAbout.ID; ////6566666
            if (PartAbout.ShowName == "鞋面")
            {
                ornOther.PostionXY = new Vector2(0.1949809f, 0.8379121f); ////6566666
            }

            ornOther.PostionX.Value += i * 0.025f;
            addItems.Add(ornOther);
        }

        return addItems;
    }

    #region 测试参数段落

    //private  List<GameObject> dddd = new List<GameObject>();
    private float m_1 = 0.945f;

    public float M1
    {
        get { return m_1; }
        set
        {
            m_1 = value;
            CreateValue();

        }
    }


    private float m_2 = 0.75f;

    public float M2
    {
        get { return m_2; }
        set
        {
            m_2 = value;
            CreateValue();

        }
    }

    private float m_3 = 0.6f;

    public float M3
    {
        get { return m_3; }
        set
        {
            m_3 = value;
            CreateValue();

        }
    }

    private float m_4 = 0.35f;

    public float M4
    {
        get { return m_4; }
        set
        {
            m_4 = value;
            CreateValue();

        }
    }

    private float m_5 = 0.1f;

    public float M5
    {
        get { return m_5; }
        set
        {
            m_5 = value;
            CreateValue();

        }
    }

    private float m_6 = 0;

    public float M6
    {
        get { return m_6; }
        set
        {
            m_6 = value;
            CreateValue();

        }
    }

    private float m_7 = default(float);

    public float M7
    {
        get { return m_7; }
        set
        {
            m_7 = value;
            Debug.Log("7" + value);
            CreateValue();

        }
    }

    private float m_8 = default(float);

    public float M8
    {
        get { return m_8; }
        set
        {
            m_8 = value;
            CreateValue();

        }
    }

    private float m_9 = 0;

    public float M9
    {
        get { return m_9; }
        set
        {
            m_9 = value;
            CreateValue();

        }
    }

    private float m_10 = 0;

    public float M10
    {
        get { return m_10; }
        set
        {
            m_10 = value;
            CreateValue();

        }
    }

    private float m_11 = 0;

    public float M11
    {
        get { return m_11; }
        set
        {
            m_11 = value;
            CreateValue();

        }
    }

    private float m_12 = 0;

    public float M12
    {
        get { return m_12; }
        set
        {
            m_12 = value;
            CreateValue();

        }
    }

    private float m_13 = 0;

    public float M13
    {
        get { return m_13; }
        set
        {
            m_13 = value;
            CreateValue();

        }
    }

    private float m_14 = 0;

    public float M14
    {
        get { return m_14; }
        set
        {
            m_14 = value;
            CreateValue();

        }
    }

    private float m_15 = 0;

    public float M15
    {
        get { return m_15; }
        set
        {
            m_15 = value;
            CreateValue();

        }
    }

    private float m_16 = 0;

    public float M16
    {
        get { return m_16; }
        set
        {
            m_16 = value;
            CreateValue();

        }
    }

    private float m_17 = 0;

    public float M17
    {
        get { return m_17; }
        set
        {
            m_17 = value;
            CreateValue();

        }
    }

    private float m_18 = 0;

    public float M18
    {
        get { return m_18; }
        set
        {
            m_18 = value;
            CreateValue();

        }
    }

    private float m_19 = 0;

    public float M19
    {
        get { return m_19; }
        set
        {
            m_19 = value;
            CreateValue();

        }
    }

    private float m_20 = 0;

    public float M20
    {
        get { return m_20; }
        set
        {
            m_20 = value;
            CreateValue();

        }
    }


    private int m_21 = 0;

    /// <summary>
    /// 层数
    /// </summary>
    public int M21
    {
        get { return m_21; }
        set
        {
            m_21 = value;
            CreateValue();

        }
    }

    #endregion


    public void CreateValue()
    {
        {
            //清理content
            List<GameObject> tGameObjects = new List<GameObject>();
            foreach (Transform VARIAtBLE in transform)
            {
                tGameObjects.Add(VARIAtBLE.gameObject);
            }

            for (int i = 0; i < tGameObjects.Count; i++)
            {
                Destroy(tGameObjects[i].gameObject);
            }

            tGameObjects = GameObject.FindGameObjectsWithTag("sphere3").ToList();
            for (int i = 0; i < tGameObjects.Count; i++)
            {
                Destroy(tGameObjects[i].gameObject);
            }
        }







        float percentBegin = 0;
        float percentEnd = 360;
        percentBegin = Mathf.Deg2Rad * percentBegin;
        percentEnd = Mathf.Deg2Rad * percentEnd;
        float percentBegin1 = M1*90;
        float percentEnd1 = M2*90;
        percentBegin1 = Mathf.Deg2Rad * percentBegin1;
        percentEnd1 = Mathf.Deg2Rad * percentEnd1;
        int slices = 10;
        float radius = 2f;
        float step1 = (percentEnd - percentBegin) / slices;
        float step2 = (percentEnd1 - percentBegin1) / slices;

        float theta1 = 0;//与xy平面夹角
        float theta2 = 0;//与xz平面夹角

        List<Vector3> p = new List<Vector3>();
        Vector3[,] points = new Vector3[slices, slices];
        for (int i = 0; i < slices; i++)
        {
            theta1 = percentBegin + i * step1;
            for (int j = 0; j < slices; j++)
            {
                theta2 = percentBegin1 + j * step2;
                Vector3 xx = new Vector3(radius * Mathf.Cos(theta2) * Mathf.Cos(theta1), radius * Mathf.Sin(theta2), radius * Mathf.Cos(theta2) * Mathf.Sin(theta1));

                CreateTriangularPyramid(1, 0.1f, ref xx, ref p,true);
                points[i, j] = xx;
            }
        }

        Vector3 t = new Vector3(0, radius, 0);

        for (int i = 0; i < slices; i++)
        {
            for (int j = 0; j < slices; j++)
            {
                GameObject go = GameObject.Instantiate(Resources.Load("Sphere2") as GameObject);
                go.transform.position = points[i, j];
                go.transform.SetParent(transform, true);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject go1 = GameObject.Instantiate(Resources.Load("Sphere3") as GameObject);
            go1.transform.position = p[i];
            //go.GetComponent<MeshRenderer>().material.color = new Color();
            go1.transform.SetParent(transform, true);

        }
    }

    public static void CreateTriangularPyramid(float height, float a, ref Vector3 move, ref List<Vector3> p,bool isOffseted = false)
    {
        float sqrt3 = Mathf.Sqrt(3);
        List<Vector3> points = new List<Vector3>();
        Vector3 pA = new Vector3(0, 0, a * sqrt3 / 3);
        Vector3 pB = new Vector3(-a / 2, 0, -a * sqrt3 / 6);
        Vector3 pC = new Vector3(a / 2, 0, -a * sqrt3 / 6);
        Vector3 pD = new Vector3(0, height, 0);
        points.Add(pA);
        points.Add(pB);
        points.Add(pC);
        points.Add(pD);
        Quaternion q = Quaternion.FromToRotation(Vector3.up, move);

        for (int i = 0; i < points.Count; i++)
        {
            //Quaternion q = Quaternion.FromToRotation(points[i], move);

            if (isOffseted)
            {
                float percentBegin = 0;
                float percentEnd = 360;
                percentBegin = Mathf.Deg2Rad * percentBegin;
                percentEnd = Mathf.Deg2Rad * percentEnd;
                float percentBegin1 = 90;
                float percentEnd1 = 85;
                percentBegin1 = Mathf.Deg2Rad * percentBegin1;
                percentEnd1 = Mathf.Deg2Rad * percentEnd1;

                float theta1 = percentBegin + UnityEngine.Random.Range(0f, 1f) * (percentEnd - percentBegin);
                float theta2 = percentBegin1 + UnityEngine.Random.Range(0f, 1f) * (percentEnd1 - percentBegin1);
                Vector3 x = new Vector3(height * Mathf.Cos(theta2) * Mathf.Cos(theta1), height * Mathf.Sin(theta2), height * Mathf.Cos(theta2) * Mathf.Sin(theta1));
                Quaternion q1 = Quaternion.FromToRotation(Vector3.up, x);
                points[i] = Matrix4x4.Rotate(q1) * points[i];
            }
            points[i] = Matrix4x4.Rotate(q) * points[i];
            //points[i] = q* move;
            points[i] = points[i] + move;
        }

        foreach (Vector3 VARIABLE in points)
        {
            p.Add(VARIABLE);
        }

    }
}