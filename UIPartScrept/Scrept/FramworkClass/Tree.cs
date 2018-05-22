using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 树结构，不要用Nodes.Add(T item)来添加结点，而是用AddNode方法来添加结点。AddNode方法将对Parent进行赋值，保证父结点可查询
/// </summary>
/// <typeparam name="T"></typeparam>
public class BoTree<T>
{
    public BoTree()
    {
        nodes = new List<BoTree<T>>();
    }

    public BoTree(T data)
    {
        this.Data = data;
        nodes = new List<BoTree<T>>();
    }

    private BoTree<T> parent;
    /// <summary>
    /// 父结点
    /// </summary>
    public BoTree<T> Parent
    {
        get { return parent; }
    }
    /// <summary>
    /// 结点数据
    /// </summary>
    public T Data { get; set; }

    private List<BoTree<T>> nodes;
    /// <summary>
    /// 子结点
    /// </summary>
    public List<BoTree<T>> Nodes
    {
        get { return nodes; }
    }
    /// <summary>
    /// 添加结点
    /// </summary>
    /// <param name="node">结点</param>
    public void AddNode(BoTree<T> node)
    {
        if (!nodes.Contains(node))
        {
            node.parent = this;
            nodes.Add(node);
        }
    }
    /// <summary>
    /// 添加结点
    /// </summary>
    /// <param name="nodes">结点集合</param>
    public void AddNode(List<BoTree<T>> nodes)
    {
        foreach (var node in nodes)
        {
            if (!nodes.Contains(node))
            {
                node.parent = this;
                nodes.Add(node);
            }
        }
    }
    /// <summary>
    /// 移除结点
    /// </summary>
    /// <param name="node"></param>
    public void Remove(BoTree<T> node)
    {
        if (nodes.Contains(node))
            nodes.Remove(node);
    }
    /// <summary>
    /// 清空结点集合
    /// </summary>
    public void RemoveAll()
    {
        nodes.Clear();
    }


    public static void Find (BoTree<T> tree,T contant, ref BoTree<T> result) 
    {
        //result = null;
        //Console.WriteLine("姓名：{0}，姓名：{1}，年龄：{2}", tree.Data.Name, tree.Data.Sex, tree.Data.Age);
        if (tree.Data.Equals(contant))
        {
            result = tree;
            return;
        }

        if (tree.Nodes.Count > 0)
        {
            foreach (var item in tree.Nodes)
            {
                Find(item, contant, ref result);
            }
        }
       if(result.Data == null)
        {
            Debug.Log("没有找到结果");
            return;
        }
    }

}
