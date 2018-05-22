using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeelOfSeButton : MonoBehaviour {
    /// <summary>
    /// 选材料-鞋跟按钮点击
    /// </summary>
    public void HeelOfSeButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.LastAdjustParameterPanel);
    }

}
