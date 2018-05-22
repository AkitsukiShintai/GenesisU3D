using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThirdLevelSift : MonoBehaviour {

    public void Init(BoTree<string> root)
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

        if (root.Nodes.Count == 0)
        {
            Exit();
        }
        else
        {
            //创建新的go
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.SiftingButton);
                go.GetComponent<SiftingButton>().Init(root.Nodes[i],true);
                go.transform.parent = transform;
                go.GetComponent<ButtonExtention>().onClick.AddListener(Exit);
            }
        }
        //go.GetComponent<DropdownExtention>().OnSelect();
    }


    public void Exit()
    {
        GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

}
