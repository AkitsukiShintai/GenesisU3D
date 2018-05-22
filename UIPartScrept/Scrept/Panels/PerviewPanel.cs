using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerviewPanel : BasePanel {

    // Use this for initialization
    public override void OnContinue()
    {
        base.OnContinue();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnDelete()
    {
        base.OnDelete();

    }

    public override void OnShow()
    {
        if (state == ShowState.Showing)
        {
            return;
        }
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        state = ShowState.Showing;
        
    }
    
    #region 几个按钮的选择

    /// <summary>
    /// 动态调用，整体预览
    /// </summary>
    /// <param name="bo"></param>
    public void FullPerviewToggleClick(bool bo)
    {
        if (bo)
        {
            //TODO 调用整体预览
            //Debug.Log("xxx");
        }
    }
    /// <summary>
    /// 动态调用，左视图
    /// </summary>
    /// <param name="bo"></param>
    public void LeftPerviewToggleClick(bool bo)
    {
        if (bo)
        {
            //TODO 调用左视图预览
            //Debug.Log("xxx");
        }
    }

    /// <summary>
    /// 动态调用，右视图
    /// </summary>
    /// <param name="bo"></param>
    public void RightPerviewToggleClick(bool bo)
    {
        if (bo)
        {
            //TODO 调用右视图预览
            // Debug.Log("xxx");
        }
    }

    /// <summary>
    /// 动态调用，前视图
    /// </summary>
    /// <param name="bo"></param>
    public void FrontPerviewToggleClick(bool bo)
    {
        if (bo)
        {
            //TODO 调用前视图预览
            //Debug.Log("xxx");
        }
    }
    /// <summary>
    /// 动态调用，后视图
    /// </summary>
    /// <param name="bo"></param>
    public void BackPerviewToggleClick(bool bo)
    {
        if (bo)
        {
            //TODO 调用后视图预览
            //Debug.Log("xxx");
        }
    }

    #endregion



}
