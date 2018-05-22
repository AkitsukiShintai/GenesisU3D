using System.Collections.Generic;
using System.Linq;
using GenesisWinForm;
using GenesisWinForm.G3DObject.G3DCore;
//using System.Threading.Tasks;
using GenesisWinForm.MathG3D;
using UnityEngine;

public static class  JianMian{

    /// <summary>
    /// mesh减面算法,threshold表示粗糙程度阈值，用来衡量能不能收缩，当d<threshold时不能收缩，sigma用来确定点与边的位置,targetCount表示目标点数量
    /// </summary>
    public static void JianMianSuanfa(G3DCore3DObjectOptimize object3D, float threshold, float sigma, int targetCount)
    {

        int count = object3D.AllVertexsX.Count;//顶点个数

        foreach (KeyValuePair<int, TriangleP> t in object3D.AllTrianglesOld)
        {
            t.Value.ComputeNormalByPostion();

        }
        foreach (Vertex t in object3D.AllVertexsX)
        {
            t.ComputeNormal();

        }

        List<ContractileValue> contractileValueList = new List<ContractileValue>();
        //List<Vertex> vertices1 = new List<Vertex>();//保存已经遍历过的边
        //List<Vertex> vertices2 = new List<Vertex>();
        //Vertex D = new Vertex(new Vector3(0,0,0),new Vector2(0,0),0,new Vector2(0,0));
        // Vector3 D = new Vector3();

        // if（count）

        for (int i = 0; i < count; i++)//遍历面片所有顶点,计算边的价
        {
            //Debug.Log("当前已经计算" + (i+1) + "个点，剩余" + (count - i-1) + "个点");

            List<ContractileValue> tempList = CalculatContractileValueByOneVertex(object3D.AllVertexsX[i], threshold, sigma);
            Debug.Log("tempList.Count = " + tempList.Count);

            foreach (ContractileValue t in tempList)
            {
                contractileValueList.Add(t);
            }

        }//遍历结束
        List<ContractileValue> OrderedList = SortContractileValueList(contractileValueList);//将收缩价按从小到大排序，需要删除收缩价低的边对应的点
                                                                                            //删除收缩价最小的边
       // Debug.Log("OrderList.Count = " + OrderedList.Count);
       // int targetNumber = count - targetCount;
        //while (targetNumber != 0) {
        //    targetNumber--;
        //    if (OrderedList.Count > 0)
        //    {
        //        Vertex temp = OrderedList[0].v1;

        //        bool b = DeleteVertexs(object3D, OrderedList[0].v1, OrderedList[0].v2, OrderedList[0].v3, OrderedList[0].v4);
        //        if (b)
        //        {//删除成功,更新收缩价

        //            List<ContractileValue> tempList = CalculatContractileValueByOneVertex(OrderedList[0].v2,threshold,sigma);//计算v2的边的收缩价
        //            //删除OrderList中与v1有关的边
        //            UpdateContractileValueList(OrderedList, temp);
        //            //添加新的项目到orderlist中
        //            foreach (ContractileValue t in tempList)
        //            {
        //                OrderedList.Add(t);
        //            }
        //            OrderedList.Sort();
        //        }
        //    }

        //}
        Debug.Log("结束");

    }

    /// <summary>
    /// 删除ContractileValueList中关于某一顶点的边
    /// </summary>
    public static void UpdateContractileValueList(List<ContractileValue> _list, Vertex _v)
    {

        foreach (ContractileValue t in _list)
        {
            if (t.v1 == _v || t.v1.collapse == _v)
            {
                _list.Remove(t);
            }

        }

    }

    /// <summary>
    /// 计算一个顶点对应所有边的收缩价
    /// </summary>
    public static List<ContractileValue> CalculatContractileValueByOneVertex(Vertex _v1, float threshold, float sigma)
    {

        List<ContractileValue> contractileValueList = new List<ContractileValue>();


        DD: for (int i = 0; i < _v1.neighbor.Count; i++)
        {//在顶点中遍历所有的neighbor点     

            Vertex v1 = _v1;
            Vertex v2 = _v1.neighbor[i];
            //Debug.Log("当前在CalculatContractileValueByOneVertex函数中，当前v1的id为" + _v1.id + ",v2的id为" +v2.id);

            //判断是否v1点为三角形面片内部点
            if (IsPointInTriangle(_v1))
            {
                //Debug.Log("顶点" + v1.id + " 在面片内");
                

                Vector3 E = v2.position - v1.position;
                //v1.ComputeNormal();
                Vector3 N = v1.Normal;

                Vector3 D = Vector3.Normalize(Vector3.Cross(N, E));//得到D向量
                //Debug.Log("E = " + E + ",N = " + N + ",D = " + D);
                //如果在内部,比较v3；v4与v1，v2构成的三角形是否在v1 - v2同一侧
                //查找V3和V4
                List<Vertex> listV3V4 = new List<Vertex>();
                listV3V4 = FindVertexsHavingSameSide(v1, v2);

                if (listV3V4.Count == 2)
                {

                   // Debug.Log("ListV3V4数量为2");
                    if (IsTriangleAtSameSide(D, v1, listV3V4[0], listV3V4[1]))//如果同侧
                    {
                        //Debug.Log("三角形在同侧");
                        goto DD;//下一个点
                    }
                    else
                    {
                        //Debug.Log("三角形不在同侧");
                        //得到正负两个三角形
                        TriangleP tPosTr;
                        TriangleP tNegTr;
                        bool bool1 = Vector3.Dot(D, listV3V4[0].position - v1.position) >= 0;

                        if (bool1)
                        {
                            tPosTr = FindTriangleByVertexs(v1, v2, listV3V4[0]);
                            tNegTr = FindTriangleByVertexs(v1, v2, listV3V4[1]);
                        }
                        else
                        {
                            tPosTr = FindTriangleByVertexs(v1, v2, listV3V4[1]);
                            tNegTr = FindTriangleByVertexs(v1, v2, listV3V4[0]);

                        }

                        if (tPosTr != null && tNegTr != null)
                        {

                            Vector3 tPos = tPosTr.normal;
                            Vector3 tNeg = tNegTr.normal;
                            float d = GetDValue(D, v1, v2, tPos, tNeg, sigma);
                            Debug.Log("d = " + d + ",threshold = " + threshold);
                            if (d < threshold)
                            {
                                //不能收缩，进行下一个顶点
                                goto DD;
                            }
                            else
                            {//计算收缩价
                                CalculatContractileValue(d, E, v1, v2, listV3V4[0], listV3V4[1], contractileValueList);
                            }
                        }
                    }
                }
            }
            else
            {
                //Debug.Log("不在内部");
            }
        }
        return contractileValueList;

    }

    /// <summary>
    /// 计算收缩价
    /// </summary>
    public static void CalculatContractileValue(float _d, Vector3 _E, Vertex v1, Vertex v2, Vertex v3, Vertex v4, List<ContractileValue> _list)
    {


        float c = (1 - _d) * _E.sqrMagnitude;
        ContractileValue CC = new ContractileValue(c, v1);
        Debug.Log("边" + v1.id + " " + v2.id + "的收缩价为" + c);
        _list.Add(CC);
        return;
    }

    /// <summary>
    /// 删除不需要的点
    /// </summary>
    public static bool DeleteVertexs(G3DCore3DObjectOptimize _mesh, Vertex _v1, Vertex _v2, List<Vertex> _V3V4)
    {

        Vertex v1 = _v1;
        Vertex v2 = _v2;
        Vertex v3 = _V3V4[0];
        Vertex v4 = _V3V4[1];
        Vertex v5;//v1v3共边的另一个点，需要更新v1v3v5面的neighbor
        Vertex v6;//v1v4共边的另一个点，需要更新v1v4v6面的neighbor
        Vertex v7;//v2v3共边的另一个点，需要更新v2v3v7面的neighbor
        Vertex v8;//v2v4共边的另一个点，需要更新v2v4v7面的neighbor

        List<Vertex> V2V5 = FindVertexsHavingSameSide(v1, v3);
        List<Vertex> V2V6 = FindVertexsHavingSameSide(v1, v4);
        List<Vertex> V7V1 = FindVertexsHavingSameSide(v2, v3);
        List<Vertex> V8V1 = FindVertexsHavingSameSide(v2, v4);
        //v5
        if (V2V5[0] == v2)
        {
            v5 = V2V5[1];
        }
        else
        {
            v5 = V2V5[0];
        }
        //v6
        if (V2V6[0] == v2)
        {
            v6 = V2V6[1];
        }
        else
        {
            v6 = V2V6[0];
        }
        //v7
        if (V7V1[0] == v1)
        {
            v7 = V7V1[1];
        }
        else
        {
            v7 = V7V1[0];
        }
        //v8
        if (V8V1[0] == v1)
        {
            v8 = V8V1[1];
        }
        else
        {
            v8 = V8V1[0];
        }
        //获取v135,v146,v237,v248三角形的id
        int v135 = FindTriangleByVertexs(v1, v3, v5).id;
        int v146 = FindTriangleByVertexs(v1, v4, v6).id;
        int v237 = FindTriangleByVertexs(v2, v3, v7).id;
        int v248 = FindTriangleByVertexs(v2, v4, v8).id;
        int v123 = FindTriangleByVertexs(v1, v2, v3).id;
        int v124 = FindTriangleByVertexs(v1, v2, v4).id;

        if (v135 == 0 || v146 == 0 || v237 == 0 || v248 == 0)
        {

            return false;
        }

        for (int i = 0; i < _v1.neighbor.Count; i++)
        {//删除neighbor中的v1，添加v2为neighbor
            _v1.neighbor[i].RemoveNeighbor(v1);
            _v1.neighbor[i].AddNeighbor(v2);
            v2.AddNeighbor(_v1.neighbor[i]);
            for (int j = 0; j < _v1.neighbor[i].Face.Count; j++)
            {
                //更新neighbor的face,neighbor的所有面中有v1节点的更新为v2
                for (int k = 0; k < 3; k++)
                {
                    if (_v1.neighbor[i].Face[j].vertex[k] == _v1)
                    {
                        _v1.neighbor[i].Face[j].vertex[k] = _v2;
                    }
                }


            }
        }


        //更新face->neighbor
        for (int i = 0; i < _mesh.AllTrianglesOld[v135].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v135].NeighborID[i] == v123)
            {//更新v135
                _mesh.AllTrianglesOld[v135].NeighborID[i] = v237;
            }

        }
        for (int i = 0; i < _mesh.AllTrianglesOld[v146].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v146].NeighborID[i] == v124)
            {//更新v146
                _mesh.AllTrianglesOld[v146].NeighborID[i] = v248;
            }

        }
        for (int i = 0; i < _mesh.AllTrianglesOld[v237].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v237].NeighborID[i] == v123)
            {//更新v237
                _mesh.AllTrianglesOld[v237].NeighborID[i] = v135;
            }

        }
        for (int i = 0; i < _mesh.AllTrianglesOld[v248].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v248].NeighborID[i] == v124)
            {//更新v248
                _mesh.AllTrianglesOld[v248].NeighborID[i] = v146;
            }

        }
        //更新v2所有面的normal,所有点的normal
        for (int i = 0; i < v2.Face.Count; i++)
        {

            v2.Face[i].ComputeNormalByPostion();
        }
        //更新v2所有neighbor的normal
        for (int i = 0; i < v2.neighbor.Count; i++)
        {

            v2.neighbor[i].ComputeNormal();
        }
        //更新v2norma
        v2.ComputeNormal();

        //删除面v123,v124和顶点v1
        _mesh.DeleteVertex(v1.id);
        _mesh.DeleteTriangleP(v123);
        _mesh.DeleteTriangleP(v124);
        _mesh.Smoth();

        return true;

    }

    /// <summary>
    /// 删除不需要的点
    /// </summary>
    public static bool DeleteVertexs(G3DCore3DObjectOptimize _mesh, Vertex _v1, Vertex _v2, Vertex _v3, Vertex _v4)
    {

        Vertex v1 = _v1;
        Vertex v2 = _v2;
        Vertex v3 = _v3;
        Vertex v4 = _v4;
        Vertex v5;//v1v3共边的另一个点，需要更新v1v3v5面的neighbor
        Vertex v6;//v1v4共边的另一个点，需要更新v1v4v6面的neighbor
        Vertex v7;//v2v3共边的另一个点，需要更新v2v3v7面的neighbor
        Vertex v8;//v2v4共边的另一个点，需要更新v2v4v7面的neighbor

        List<Vertex> V2V5 = FindVertexsHavingSameSide(v1, v3);
        List<Vertex> V2V6 = FindVertexsHavingSameSide(v1, v4);
        List<Vertex> V7V1 = FindVertexsHavingSameSide(v2, v3);
        List<Vertex> V8V1 = FindVertexsHavingSameSide(v2, v4);
        //v5
        if (V2V5[0] == v2)
        {
            v5 = V2V5[1];
        }
        else
        {
            v5 = V2V5[0];
        }
        //v6
        if (V2V6[0] == v2)
        {
            v6 = V2V6[1];
        }
        else
        {
            v6 = V2V6[0];
        }
        //v7
        if (V7V1[0] == v1)
        {
            v7 = V7V1[1];
        }
        else
        {
            v7 = V7V1[0];
        }
        //v8
        if (V8V1[0] == v1)
        {
            v8 = V8V1[1];
        }
        else
        {
            v8 = V8V1[0];
        }
        //获取v135,v146,v237,v248三角形的id
        int v135 = FindTriangleByVertexs(v1, v3, v5).id;
        int v146 = FindTriangleByVertexs(v1, v4, v6).id;
        int v237 = FindTriangleByVertexs(v2, v3, v7).id;
        int v248 = FindTriangleByVertexs(v2, v4, v8).id;
        int v123 = FindTriangleByVertexs(v1, v2, v3).id;
        int v124 = FindTriangleByVertexs(v1, v2, v4).id;

        if (v135 == 0 || v146 == 0 || v237 == 0 || v248 == 0)
        {

            return false;
        }

        for (int i = 0; i < _v1.neighbor.Count; i++)
        {//删除neighbor中的v1，添加v2为neighbor
            _v1.neighbor[i].RemoveNeighbor(v1);
            _v1.neighbor[i].AddNeighbor(v2);
            v2.AddNeighbor(_v1.neighbor[i]);
            for (int j = 0; j < _v1.neighbor[i].Face.Count; j++)
            {
                //更新neighbor的face,neighbor的所有面中有v1节点的更新为v2
                for (int k = 0; k < 3; k++)
                {
                    if (_v1.neighbor[i].Face[j].vertex[k] == _v1)
                    {
                        _v1.neighbor[i].Face[j].vertex[k] = _v2;
                    }
                }


            }
        }


        //更新face->neighbor
        for (int i = 0; i < _mesh.AllTrianglesOld[v135].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v135].NeighborID[i] == v123)
            {//更新v135
                _mesh.AllTrianglesOld[v135].NeighborID[i] = v237;
            }

        }
        for (int i = 0; i < _mesh.AllTrianglesOld[v146].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v146].NeighborID[i] == v124)
            {//更新v146
                _mesh.AllTrianglesOld[v146].NeighborID[i] = v248;
            }

        }
        for (int i = 0; i < _mesh.AllTrianglesOld[v237].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v237].NeighborID[i] == v123)
            {//更新v237
                _mesh.AllTrianglesOld[v237].NeighborID[i] = v135;
            }

        }
        for (int i = 0; i < _mesh.AllTrianglesOld[v248].NeighborID.Length; i++)
        {
            if (_mesh.AllTrianglesOld[v248].NeighborID[i] == v124)
            {//更新v248
                _mesh.AllTrianglesOld[v248].NeighborID[i] = v146;
            }

        }
        //更新v2所有面的normal,所有点的normal
        for (int i = 0; i < v2.Face.Count; i++)
        {

            v2.Face[i].ComputeNormalByPostion();
        }
        //更新v2所有neighbor的normal
        for (int i = 0; i < v2.neighbor.Count; i++)
        {

            v2.neighbor[i].ComputeNormal();
        }
        //更新v2norma
        v2.ComputeNormal();

        //删除面v123,v124和顶点v1
        _mesh.DeleteVertex(v1.id);
        _mesh.DeleteTriangleP(v123);
        _mesh.DeleteTriangleP(v124);
        _mesh.Smoth();

        return true;

    }



    /// <summary>
    /// 判断一个顶点是否在三角形网格内部
    /// </summary>
    public static bool IsPointInTriangle(Vertex _v1)
    {
        //Debug.Log("当前在函数IsPointInTriangle中，_v1编号为" + _v1.id);
        if (_v1.neighbor.Count < 3)
        {

            return false;
        }

        for (int vertexCont = 0; vertexCont < _v1.neighbor.Count; vertexCont++)
        {//遍历v1所有顶点,即遍历v1的相邻边
            
            List<Vertex> v3v4 = FindVertexsHavingSameSide(_v1, _v1.neighbor[vertexCont]);

            if (v3v4.Count < 2)//如果没有公用边，则v1不在网格内部
            {
                return false;
            }
            else if (vertexCont < _v1.neighbor.Count-1)
            {//表明没有遍历完所有的边，继续遍历
                //Debug.Log("当前遍历边数为" + _v1.neighbor.Count+ ",已经遍历第"+ vertexCont+"个");
                
            }
            if(vertexCont == _v1.neighbor.Count-1)
            { //如果已经遍历到最后一条边发现所有边的数量都超过了2，则v1在内部
                return true;
            }


        }

        return false;
    }



    /// <summary>
    /// 判断三角形是否在一条线段两侧
    /// </summary>
    public static bool IsTriangleAtSameSide(Vertex v1, Vertex v2, Vertex v3, Vertex v4)
    {
        if (v1 == null || v2 == null || v3 == null || v4 == null)
        {

            return false;
        }

        Vector3 E = v2.position - v1.position;
        Vector3 N = v1.Normal;
        Vector3 D = Vector3.Normalize(Vector3.Cross(N, E));
       // Debug.Log("E = " + E + ",N = " + N + ",D = " + D);

        bool bool1 = Vector3.Dot(D, v3.position - v1.position) >= 0;
        bool bool2 = Vector3.Dot(D, v4.position - v1.position) >= 0;

        if (bool1 != bool2)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    /// <summary>
    /// 判断两个三角形是否在一条线段两侧
    /// </summary>
    public static bool IsTriangleAtSameSide(Vector3 D, Vertex v1, Vertex v3, Vertex v4)
    {
        if (v3 == null || v4 == null) {

            return false;
        }

        float a = Vector3.Dot(D, v3.position - v1.position);
        float b = Vector3.Dot(D, v4.position - v1.position);
        bool bool1 = a >= 0;
        bool bool2 = b>= 0;

        Debug.Log("D:" + D + " a:" + a + "  b" + b);

        //if (a == 0 && b == 0) {

        //    return false;
        //}

        if (bool1 != bool2)
        {
            return false;
        }
        else
        {
            return true;
        }

    }



    /// <summary>
    /// 计算d值（最小内积）D 为收缩边和v1法相量对应的面的法向量，v1，v2构成收缩边，tPos为v1v2正法向量，tNeg为v1v2负法向量
    /// </summary>
    public static float GetDValue(Vector3 D, Vertex v1, Vertex v2, Vector3 tPos, Vector3 tNeg, float threshold)
    {
        float d = 1;
        DD: for (int i = 0; i < v1.Face.Count; i++)
        {
            //判断三角形不是由v1v2构成的
            int pointCount = 0;
            for (int j = 0; j < 3; j++)
            {
                if (v1.Face[i].vertex[j] == v2 || v1.Face[i].vertex[j] == v1)
                {
                    pointCount++;
                }
                if (pointCount >= 2)
                {
                    pointCount = 0;
                    goto DD;//如果发现有两个顶点为v1v2，则跳过该三角形进行下一个三角形的判断

                }
            }

            //计算另外两个点AB相关的内积a,b
            float a = -1;
            float b = -1;
            for (int k = 0; k < 3; k++)
            {
                if (v1.Face[i].vertex[k] == v1)
                {
                    continue;
                }
                if (a == -1)
                {
                    a = Vector3.Dot(v1.Face[i].normal, tPos);
                }
                else
                {
                    b = Vector3.Dot(v1.Face[i].normal, tPos);
                }
            }
            int positionOfVertexA = 0;//标识点A所在收缩边的正负侧
            if (a > threshold)
            {

                positionOfVertexA = 1;//正
            }
            else if (a < -threshold)
            {

                positionOfVertexA = -1;//负
            }
            else
            {
                positionOfVertexA = 0;//边上
            }
            int positionOfVertexB = 0;//标识点B所在收缩边的正负侧
            if (b > threshold)
            {

                positionOfVertexB = 1;
            }
            else if (b < -threshold)
            {

                positionOfVertexB = -1;
            }
            else
            {
                positionOfVertexB = 0;
            }
            if (positionOfVertexA == 1 || positionOfVertexB == 1)
            {

                d = d > Vector3.Dot(v1.Face[i].normal, tPos) ? Vector3.Dot(v1.Face[i].normal, tPos) : d;
            }
            else if (positionOfVertexA == -1 || positionOfVertexB == -1)
            {
                d = d > Vector3.Dot(v1.Face[i].normal, tNeg) ? Vector3.Dot(v1.Face[i].normal, tNeg) : d;
            }

        }
        return d;
    }

    /// <summary>
    /// 查找共边三角形的另外两个顶点
    /// </summary>
    public static List<Vertex> FindVertexsHavingSameSide(Vertex v1, Vertex v2)
    {
        List<Vertex> list = new List<Vertex>();
        for (int i = 0; i < v1.neighbor.Count; i++)
        {
            for (int j = 0; j < v2.neighbor.Count; j++)
            {

                if (v1.neighbor[i].id == v2.neighbor[j].id)
                {
                    list.Add(v1.neighbor[i]);
                }
                if (list.Count == 2)
                {
                    return list;
                }
            }

        }


        return list;
    }

    /// <summary>
    /// 通过三个顶点查找一个三角形
    /// </summary>
    public static TriangleP FindTriangleByVertexs(Vertex v1, Vertex v2, Vertex v3)
    {
        TriangleP t = new TriangleP(0);
        for (int i = 0; i < v1.Face.Count; i++)
        {
            for (int j = 0; j < v2.Face.Count; j++)
            {
                for (int k = 0; k < v3.Face.Count; k++)
                {
                    if (v1.Face[i] == v2.Face[j] && v2.Face[j] == v3.Face[k])
                    {

                        return v1.Face[i];
                    }
                }

            }

        }

        return t;
    }


    /// <summary>
    /// ContractileValueList排序
    /// </summary>
    public static List<ContractileValue> SortContractileValueList(List<ContractileValue> _list)
    {

        Dictionary<float, int> dic = new Dictionary<float, int>();
        List<ContractileValue> newList = new List<ContractileValue>();
        List<float> valueList = new List<float>();
        int index = -1;
        foreach (ContractileValue t in _list)
        {
            index++;
            dic.Add(t.value, index);
            valueList.Add(t.value);
        }

        valueList.Sort();
        int indexx = -1;
        foreach (ContractileValue t in _list)
        {
            indexx++;
            newList.Add(_list[dic[valueList[indexx]]]);
        }


        return newList;
    }
}

