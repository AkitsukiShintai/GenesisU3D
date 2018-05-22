using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerRoot : MonoBehaviour {

    public GameObject seletePanel;
	// Use this for initialization
    public Button SeleteMatButton;

	void Start () {
        //GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.HeelButton);
        //go.transform.SetParent(seletePanel.transform);
        //UIManager.Instance.PushPanel(UIPanelType.SeleteMatPanel);
	    SeleteMatButton.onClick.Invoke();
        //Debug.Log("dd");
	}

    
	
	
}
