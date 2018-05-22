using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class BasePanel : MonoBehaviour {


    protected CanvasGroup canvasGroup;
    protected ShowState state  = ShowState.None;

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
       
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnContinue()
    {
    }

    /// <summary>
    /// 界面显示
    /// </summary>
    public virtual void OnShow()
    {
    }

    /// <summary>
    /// 界面删除
    /// </summary>
    public virtual void OnDelete()
    {

    }
}
