using System;
using System.Collections.Generic;
using System.Linq;
using GenesisWinForm.G3DObject.G3DCore;
//using System.Threading.Tasks;
using GenesisWinForm.MathG3D;
using UnityEngine;

public static class CollapseVertex
{


    /// <summary>
    /// 计算边的坍塌cost
    /// </summary>
    public static float ComputeEdgeCollapseCost(Vertex u, Vertex v)
    {

        // if we collapse edge uv by moving u to v then how

        // much different will the model change, i.e. the “error”.

        float edgelength = (v.position - u.position).magnitude;

        float curvature = 0;

        // find the “sides” triangles that are on the edge uv

        List<TriangleP> sides = new List<TriangleP>();

        for (int i = 0; i < u.Face.Count; i++)
        {

            if (u.Face[i].HasVertex(v.id))
            {

                sides.Add(u.Face[i]);

            }

        }

        // use the triangle facing most away from the sides

        // to determine our curvature term

        for (int i = 0; i < u.Face.Count; i++)
        {

            float mincurv = 1;

            for (int j = 0; j < sides.Count; j++)
            {

                // use dot product of face normals.

                float dotprod = Vector3.Dot(u.Face[i].normal, sides[j].normal);

                mincurv = Mathf.Min(mincurv, (1 - dotprod) / 2.0f);

            }

            curvature = Mathf.Max(curvature, mincurv);

        }
        ////Debug.Log("curvature" + curvature);
        return edgelength * curvature;

    }

    /// <summary>
    /// 通过顶点计算坍塌cost
    /// </summary>
    public static void ComputeEdgeCostAtVertex(Vertex v)
    {

        if (v.neighbor.Count == 0)
        {

            v.collapse = null;

            v.cost = -0.01f;

            return;

        }

        v.cost = 1000000;

        v.collapse = null;

        // search all neighboring edges for “least cost” edge

        for (int i = 0; i < v.neighbor.Count; i++)
        {

            float c;

            c = ComputeEdgeCollapseCost(v, v.neighbor[i]);

            if (c < v.cost)
            {

                v.collapse = v.neighbor[i];

                v.cost = c;

            }

        }

    }


    #region
    /// <summary>
    /// 通过三个顶点查找一个三角形
    /// </summary>
    //public static int FindTriangleByVertexs(Vertex v1, Vertex v2, Vertex v3)
    //{
    //    TriangleP t = new TriangleP(0);
    //    for (int i = 0; i < v1.Face.Count; i++)
    //    {
    //        for (int j = 0; j < v2.Face.Count; j++)
    //        {
    //            for (int k = 0; k < v3.Face.Count; k++)
    //            {
    //                if (v1.Face[i] == v2.Face[j] && v2.Face[j] == v3.Face[k])
    //                {

    //                    return v1.Face[i].id;
    //                }
    //            }

    //        }

    //    }

    //    return t.id;
    //}


    //public static bool DeleteFace(G3DCore3DObjectOptimize _mesh, TriangleP _triangleP)
    //{

    //    _mesh.DeleteTriangleP(_triangleP.id);

    //    return true;

    //}

    //public static bool DeleteVertexs(G3DCore3DObjectOptimize _mesh, Vertex _v1)
    //{
    //    #region
    //    //Vertex v1 = _v1;
    //    //Vertex v2 = _v1.collapse;

    //    //List<Vertex> v3v4 = FindVertexsHavingSameSide(v1, v2);
    //    //Vertex v3 = v3v4[0];
    //    //Vertex v4 = v3v4[1];
    //    //Vertex v5;//v1v3共边的另一个点，需要更新v1v3v5面的neighbor
    //    //Vertex v6;//v1v4共边的另一个点，需要更新v1v4v6面的neighbor
    //    //Vertex v7;//v2v3共边的另一个点，需要更新v2v3v7面的neighbor
    //    //Vertex v8;//v2v4共边的另一个点，需要更新v2v4v7面的neighbor

    //    //List<Vertex> V2V5 = FindVertexsHavingSameSide(v1, v3);
    //    //List<Vertex> V2V6 = FindVertexsHavingSameSide(v1, v4);
    //    //List<Vertex> V7V1 = FindVertexsHavingSameSide(v2, v3);
    //    //List<Vertex> V8V1 = FindVertexsHavingSameSide(v2, v4);
    //    ////v5
    //    //if (V2V5[0] == v2)
    //    //{
    //    //    v5 = V2V5[1];
    //    //}
    //    //else
    //    //{
    //    //    v5 = V2V5[0];
    //    //}
    //    ////v6
    //    //if (V2V6[0] == v2)
    //    //{
    //    //    v6 = V2V6[1];
    //    //}
    //    //else
    //    //{
    //    //    v6 = V2V6[0];
    //    //}
    //    ////v7
    //    //if (V7V1[0] == v1)
    //    //{
    //    //    v7 = V7V1[1];
    //    //}
    //    //else
    //    //{
    //    //    v7 = V7V1[0];
    //    //}
    //    ////v8
    //    //if (V8V1[0] == v1)
    //    //{
    //    //    v8 = V8V1[1];
    //    //}
    //    //else
    //    //{
    //    //    v8 = V8V1[0];
    //    //}
    //    ////获取v135,v146,v237,v248三角形的id
    //    //int v135 = FindTriangleByVertexs(v1, v3, v5);
    //    //int v146 = FindTriangleByVertexs(v1, v4, v6);
    //    //int v237 = FindTriangleByVertexs(v2, v3, v7);
    //    //int v248 = FindTriangleByVertexs(v2, v4, v8);
    //    //int v123 = FindTriangleByVertexs(v1, v2, v3);
    //    //int v124 = FindTriangleByVertexs(v1, v2, v4);
    //    ////更新face->neighbor
    //    //for (int i = 0; i < _mesh.AllTrianglesOld[v135].NeighborID.Length; i++)
    //    //{
    //    //    if (_mesh.AllTrianglesOld[v135].NeighborID[i] == v123)
    //    //    {//更新v135
    //    //        _mesh.AllTrianglesOld[v135].NeighborID[i] = v237;
    //    //    }

    //    //}
    //    //for (int i = 0; i < _mesh.AllTrianglesOld[v146].NeighborID.Length; i++)
    //    //{
    //    //    if (_mesh.AllTrianglesOld[v146].NeighborID[i] == v124)
    //    //    {//更新v146
    //    //        _mesh.AllTrianglesOld[v146].NeighborID[i] = v248;
    //    //    }

    //    //}
    //    //for (int i = 0; i < _mesh.AllTrianglesOld[v237].NeighborID.Length; i++)
    //    //{
    //    //    if (_mesh.AllTrianglesOld[v237].NeighborID[i] == v123)
    //    //    {//更新v237
    //    //        _mesh.AllTrianglesOld[v237].NeighborID[i] = v135;
    //    //    }

    //    //}
    //    //for (int i = 0; i < _mesh.AllTrianglesOld[v248].NeighborID.Length; i++)
    //    //{
    //    //    if (_mesh.AllTrianglesOld[v248].NeighborID[i] == v124)
    //    //    {//更新v248
    //    //        _mesh.AllTrianglesOld[v248].NeighborID[i] = v146;
    //    //    }

    //    //}




    //    //if (v135 == 0 || v146 == 0 || v237 == 0 || v248 == 0)
    //    //{

    //    //    return false;
    //    //}
    //    #endregion

    //    List<Vertex> v3v4 = FindVertexsHavingSameSide(_v1, _v1.collapse);

    //    int v123 = FindTriangleByVertexs(_v1, _v1.collapse, v3v4[0]);
    //    int v124 = FindTriangleByVertexs(_v1, _v1.collapse, v3v4[1]);

    //    for (int i = 0; i < _v1.Face.Count; i++)
    //    {
    //        for (int j = 0; j < _v1.collapse.Face.Count; j++)
    //        {
    //            if (_v1.Face[i].HasVertex(v3v4[0].id) == _v1.collapse.Face[j].HasVertex(v3v4[0].id))
    //            {
    //                for (int k = 0; k < _v1.Face[i].NeighborID.Length; k++)
    //                {
    //                    if (_v1.Face[i].NeighborID[k] == v123)
    //                    {
    //                        _v1.Face[i].NeighborID[k] = _v1.collapse.Face[j].id;
    //                        break;
    //                    }
    //                }

    //                for (int k = 0; k < _v1.collapse.Face[j].NeighborID.Length; k++)
    //                {
    //                    if (_v1.Face[i].NeighborID[k] == v123)
    //                    {
    //                        _v1.Face[i].NeighborID[k] = _v1.collapse.Face[j].id;
    //                        break;
    //                    }
    //                }

    //            }

    //            if (_v1.Face[i].HasVertex(v3v4[1].id) == _v1.collapse.Face[j].HasVertex(v3v4[1].id))
    //            {
    //                for (int k = 0; k < _v1.Face[i].NeighborID.Length; k++)
    //                {
    //                    if (_v1.Face[i].NeighborID[k] == v124)
    //                    {
    //                        _v1.Face[i].NeighborID[k] = _v1.collapse.Face[j].id;
    //                        break;
    //                    }
    //                }

    //                for (int k = 0; k < _v1.collapse.Face[j].NeighborID.Length; k++)
    //                {
    //                    if (_v1.Face[i].NeighborID[k] == v124)
    //                    {
    //                        _v1.Face[i].NeighborID[k] = _v1.collapse.Face[j].id;
    //                        break;
    //                    }
    //                }

    //            }
    //        }

    //    }




    //    _mesh.DeleteVertex(_v1.id);

    //    return true;

    //}
    #endregion
    /// <summary>
    /// 更新mesh
    /// </summary>
    public static bool UpdateMesh(G3DCore3DObjectOptimize _mesh)
    {
        //更新VertexOld 和VertexX

        _mesh.AllVertexsX.Clear();
        foreach (KeyValuePair<int, Vertex> t in _mesh.AllVertexsOld)
        {

            _mesh.AllVertexsX.Add(t.Value);

        }

        foreach (KeyValuePair<int, TriangleP> t in _mesh.AllTrianglesOld)
        {



            t.Value.ComputeNormalByPostion();
        }


        for (int i = 0; i < _mesh.AllVertexsX.Count; i++)
        {

            _mesh.AllVertexsX[i].ComputeNormal();


        }

        return true;

    }


    /// <summary>
    /// 判断顶点是否在三角形网格内部
    /// </summary>
    public static bool IsPointInTriangle(Vertex _v1)
    {
        ////Debug.Log("当前在函数IsPointInTriangle中，_v1编号为" + _v1.id);
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
            else if (vertexCont < _v1.neighbor.Count - 1)
            {//表明没有遍历完所有的边，继续遍历
             ////Debug.Log("当前遍历边数为" + _v1.neighbor.Count+ ",已经遍历第"+ vertexCont+"个");

            }
            if (vertexCont == _v1.neighbor.Count - 1)
            { //如果已经遍历到最后一条边发现所有边的数量都超过了2，则v1在内部
                return true;
            }


        }

        return false;
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
    /// mesh减面算法,threshold表示粗糙程度阈值，用来衡量能不能收缩，当d<threshold时不能收缩,sigma用来确定点与边的位置,targetCount表示目标点数量
    /// </summary>
    public static void Start111(G3DCore3DObjectOptimize object3D, int targetCount)
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        int count = object3D.AllVertexsX.Count;//顶点个数

        //UpdateMesh(object3D);
        //object3D.Vertexall = DeepCopy(object3D.AllVertexsX);
        //object3D.Vertexall = DeepCopy(object3D.AllVertexsX);
        //object3D.AllVertexsX.ForEach(i=>object3D.Vertexall.Add(i));
        //List<ContractileValue> contractileValueList = new List<ContractileValue>();
        //SortedList<Vertex,float> contractileValueList1 = new SortedList<Vertex, float>();//有序链表,
        //Dictionary<int, float> VertexIDToCost = new Dictionary<int, float>();
        CollapseValue.Instance.VertexIDToCost.Clear();
        CollapseValue.Instance.VertexIDToCostTemp.Clear();

        foreach (KeyValuePair<int, Vertex> t in object3D.AllVertexsOld)//遍历面片所有顶点,计算边的价
        {
            ////Debug.Log("当前已经计算" + (i+1) + "个点，剩余" + (count - i-1) + "个点");
            ComputeEdgeCostAtVertex(t.Value);
            //VertexIDToCost.Add(t.Value.id, t.Value.cost);
            CollapseValue.Instance.VertexIDToCost.Add(t.Value.id,t.Value.cost);

            //ContractileValue temp = new ContractileValue(t.Value);
            //idToIndex.Add(t.Key,indx);
            //contractileValueList.Add(temp);
            //contractileValueListOrigin.Add(temp);

        }//遍历结束

        //Dictionary<int, float> dic1_SortedByValue = VertexIDToCost.OrderBy(o => o.Value).ToDictionary(p => p.Key, o => o.Value);
        //VertexIDToCost = dic1_SortedByValue;
        CollapseValue.Instance.VertexIDToCostTemp = CollapseValue.Instance.VertexIDToCost.OrderBy(o => o.Value).ToDictionary(p => p.Key, o => o.Value);
        CollapseValue.Instance.VertexIDToCost = CollapseValue.Instance.VertexIDToCostTemp;

        //CollapseValue.Instance.VertexIDToCostOrigion = CollapseValue.Instance.VertexIDToCostTemp;

        //VertexIDToCost.OrderBy(i=>i.Value);
        //Print(VertexIDToCost);
        //SortContractileValueList(ref contractileValueList);//将收缩价按从小到大排序，需要删除收缩价低的边对应的点
        //contractileValueList.OrderBy(p => p.value);                                                 //删除收缩价最小的边
        ////Debug.Log("OrderList.Count = " + contractileValueList.Count);

        int targetNumber = object3D.AllVertexsOld.Count - targetCount;
        ////Debug.Log("targetNumber" + targetNumber + "object3D.AllTrianglesOld.Count - targetCount" + (object3D.AllTrianglesOld.Count - targetCount));

      

        while (targetNumber != 0)
        {
            if (CollapseValue.Instance.VertexIDToCost.Count > 0)
            {

                ////Debug.Log("剩余" + targetNumber + "需要删除");
                int temp = CollapseValue.Instance.VertexIDToCost.Keys.First();
               // Debug.Log("当前点的id" + temp);
                //Debug.Log("if前costToVertexs大小" + VertexIDToCost.Count);

                //Debug.Log("temp   " + temp);


                if (IsPointInTriangle(object3D.AllVertexsOld[temp]))
                {
                    ////Debug.Log("删除前contractileValueList大小"+ contractileValueList.Count);
                    targetNumber--;
                    Collapse111(ref object3D, object3D.AllVertexsOld[temp], object3D.AllVertexsOld[temp].collapse);//, ref VertexIDToCost);
                }
                else
                {
                    CollapseValue.Instance.VertexIDToCost.Remove(temp);                
                    //contractileValueList.RemoveAt(0);

                }
            }
            else
            {
                break;
            }
        }
        //UpdateMesh(object3D);
        sw.Stop();
        Debug.Log("优化面片" + (count - object3D.AllVertexsX.Count) * 2 + "用时"+sw.ElapsedMilliseconds);
        //Debug.Log("结束");

    }


    /// <summary>
    /// 坍塌
    /// </summary>
    public static bool Collapse111(ref G3DCore3DObjectOptimize _mesh, Vertex u, Vertex v)//, ref Dictionary<int, float> dic)
    {

        List<Vertex> xxx = FindVertexsHavingSameSide(u, v);//v3v4
        TriangleP t1 = new TriangleP(-1);
        TriangleP t2 = new TriangleP(-1); //删除的两个面
        int i;
        List<Vertex> tmp = new List<Vertex>();//保存u的原始邻接顶点

        v.RemoveNeighbor(u);//v没有u
        u.RemoveNeighbor(v);//u 没有 v


        // Collapse the edge uv by moving vertex u onto v


        if (v == null)
        {

            // u is a vertex all by itself so just delete it

            _mesh.DeleteVertex(u.id);

            return true;

        }

        // List<TriangleP> tmpTList = new List<TriangleP>();
        // make tmp a list of all the neighbors of u
        for (i = 0; i < u.neighbor.Count; i++)
        {

            tmp.Add(u.neighbor[i]);

        }

        tmp.Remove(v);


        // delete triangles on edge uv:

        int iddd = 0;
        for (i = u.Face.Count - 1; i >= 0; i--)
        {

            if (u.Face[i].HasVertex(v.id))
            {
                // delete(u.face[i]);
                //删除u的邻接面中u.Face[i]

                iddd++;
                if (iddd == 1)
                {
                    t1 = u.Face[i];
                }
                if (iddd == 2)
                {
                    t2 = u.Face[i];
                    break;
                }


                //List<TriangleP> trianglePs = new List<TriangleP>()                           
            }

        }//找到删除的面t1 t2 并删除     u没有v，u没有t1t2       v没有u，v没有t1t2

        v.Face.Remove(t1);
        u.Face.Remove(t1);
        v.Face.Remove(t2);
        u.Face.Remove(t2);

        //修改t3t4t5t6的neighborID
        bool bo = false;
        for (int k = 0; k < u.Face.Count; k++)
        {
            for (int m = 0; m < v.Face.Count; m++)
            {
                if (u.Face[k].NeighborID.Contains(t1.id) && v.Face[m].NeighborID.Contains(t1.id))
                {

                    int tem1 = Array.IndexOf(u.Face[k].NeighborID, t1.id);
                    int tem2 = Array.IndexOf(v.Face[m].NeighborID, t1.id);
                    u.Face[k].NeighborID[tem1] = v.Face[m].id;
                    v.Face[m].NeighborID[tem2] = u.Face[k].id;
                    //_mesh.DeleteTriangleP(u.Face[i].id);
                    //iddd++;
                    bo = true;
                    //v.Face.Add(u.Face[k]);

                }
                if (bo)
                {
                    break;
                }

            }
            if (bo)
            {
                break;
            }

        }

        bo = false;
        for (int k = 0; k < u.Face.Count; k++)
        {
            for (int m = 0; m < v.Face.Count; m++)
            {
                if (u.Face[k].NeighborID.Contains(t2.id) && v.Face[m].NeighborID.Contains(t2.id))
                {

                    int tem1 = Array.IndexOf(u.Face[k].NeighborID, t2.id);
                    int tem2 = Array.IndexOf(v.Face[m].NeighborID, t2.id);
                    u.Face[k].NeighborID[tem1] = v.Face[m].id;
                    v.Face[m].NeighborID[tem2] = u.Face[k].id;
                    //_mesh.DeleteTriangleP(u.Face[i].id);
                    //iddd++;
                    bo = true;
                    //v.Face.Add(u.Face[k]);

                }
                if (bo)
                {
                    break;
                }

            }
            if (bo)
            {
                break;
            }

        }



        for (i = u.Face.Count - 1; i >= 0; i--)
        {
            u.Face[i].ReplaceVertex(u, v);
            u.Face[i].ComputeNormalByPostion();
        }//u的除t1t2之外所有面将v替换u并重新计算法向量

        foreach (TriangleP t in u.Face)
        {
            v.Face.Add(t);

        }//v除去t1t2后将所有的u的邻接面除t1t2外加入到v的face中
        ////Debug.Log("mesh面片数量" + _mesh.AllTrianglesOld.Count);
        // update remaining triangles to have v instead of u
        int j = u.neighbor.Count;//u除v之外所有顶点的数量
        for (i = 0; i < j; i++)
        {
            u.neighbor[i].RemoveNeighbor(u);
            u.neighbor[i].AddNeighbor(v);//u除v之外所有邻接顶点加v为neighbor
            v.AddNeighbor(u.neighbor[i]);//v加所有u的邻接顶点为neighbor
        }
        _mesh.DeleteTriangleP(t1.id);
        _mesh.DeleteTriangleP(t2.id);//mesh中删除t1t2
        _mesh.DeleteVertex(u.id);//mesh中删除u
        xxx[0].Face.Remove(t1);
        xxx[1].Face.Remove(t1);
        xxx[0].Face.Remove(t2);
        xxx[1].Face.Remove(t2);//v3v4删除邻接面t1t2

        v.ComputeNormal();//v当前包含以前的所有面除t1t2以及u除t1t2的所有面，计算法向量
        //if (dic.ContainsKey(v.id))
        //{
        //    ComputeEdgeCostAtVertex(v);//重新计算v的cost
        //    dic[v.id] = v.cost;

        //}
        if (CollapseValue.Instance.VertexIDToCost.ContainsKey(v.id))
        {
            ComputeEdgeCostAtVertex(v);//重新计算v的cost
            CollapseValue.Instance.VertexIDToCost[v.id] = v.cost;

        }
        //DeleteVertexs(_mesh, u);
        ////Debug.Log("删除后顶点数量" + _mesh.AllVertexsOld.Count);
        // recompute the edge collapse costs in neighborhood
        //UpdateMesh(_mesh);

        //Debug.Log("删除前contractileValueList大小" + _list.Count);
        //dic.Remove(dic.Keys.First());
        CollapseValue.Instance.VertexIDToCost.Remove(CollapseValue.Instance.VertexIDToCost.Keys.First());
        //UpdateMesh(_mesh);


        //for (i = 0; i < u.neighbor.Count; i++)
        //{
        //    u.neighbor[i].ComputeNormal();
        //    if (dic.ContainsKey(u.neighbor[i].id))
        //    {
        //        ComputeEdgeCostAtVertex(u.neighbor[i]);
        //        dic[u.neighbor[i].id] = u.neighbor[i].cost;
        //        //Debug.Log("更改了第" + u.neighbor[i].id + "号点的cost");
        //    }
        //}


        for (i = 0; i < u.neighbor.Count; i++)
        {
            u.neighbor[i].ComputeNormal();
            if (CollapseValue.Instance.VertexIDToCost.ContainsKey(u.neighbor[i].id))
            {
                ComputeEdgeCostAtVertex(u.neighbor[i]);
                CollapseValue.Instance.VertexIDToCost[u.neighbor[i].id] = u.neighbor[i].cost;
                //Debug.Log("更改了第" + u.neighbor[i].id + "号点的cost");
            }
        }


        //dic[v.id] = v.cost;
        //dic.OrderBy(p => p.Value);
       // CollapseValue.Instance.VertexIDToCostTemp.Clear();
        //Dictionary<int, float> t = dic.OrderBy(o => o.Value).ToDictionary(p => p.Key, o => o.Value);
        //dic = dic1_SortedByValue;

        CollapseValue.Instance.VertexIDToCostTemp = CollapseValue.Instance.VertexIDToCost.OrderBy(o => o.Value).ToDictionary(p => p.Key, o => o.Value);
        CollapseValue.Instance.VertexIDToCost = CollapseValue.Instance.VertexIDToCostTemp;

        //dic.OrderBy(i => i.Value);
        //Debug.Log("重新计算cost后" + dic.Keys.First() + "号顶点");

        return true;

    }

    public static void Print(Dictionary<int,float> k) {
        int i = 0;
        foreach (KeyValuePair<int, float> d in k) {

            if (i < 100)
            {

                Debug.Log("ID：" + d.Key + "Cost:" + d.Value);
            }
            else {
                break;
            }
        }
    }


    public class CollapseValue : MonoSingleton<CollapseValue> {

        public Dictionary<int, float> VertexIDToCost = new Dictionary<int, float>();
        public Dictionary<int, float> VertexIDToCostTemp = new Dictionary<int, float>();
        public Dictionary<int, float> VertexIDToCostOrigion = new Dictionary<int, float>();
    }


}



