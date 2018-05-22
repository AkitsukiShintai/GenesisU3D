using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class DropdownExtention : Dropdown
{
    [SerializeField] private GameObject defultText;

    [SerializeField] public GameObject AdaptiveObject;
    private BoTree<string> Node;
    private bool[] nextLevelDropdown;
    private int i = 0;
    private bool Firsted = false;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        //if(captionText == null)
        i++;
        Debug.Log("进入OnSelect"+i+"次");
        defultText.SetActive(false);
        captionText.gameObject.SetActive(true);

        if (!Firsted && Node.Nodes[0].Nodes.Count != 0)
        {
            CreateThirdSift();
            Firsted = true;
        }
    }

  

    /// <summary>
    /// 实现初始化
    /// </summary>
    public void Init(BoTree<string> rootNode = null)
    {
        if (defultText == null)
            defultText = transform.Find("GameObject/DefultText").gameObject;
        if (AdaptiveObject == null)
            AdaptiveObject = GameObject.FindGameObjectWithTag("AdaptiveObject");
        options.Clear();
        
        //-------------------------分割线-----------------------------//
        if (rootNode != null)
        {

            Node = rootNode;
            defultText.GetComponentInChildren<Text>().text = rootNode.Data;
            // nextLevelDropdown = new bool[Node.Nodes.Count];
            if (rootNode.Nodes.Count > 0)
            {
                foreach (var item in rootNode.Nodes)
                {
                    //Find(item, contant, ref result);
                    OptionData tData = new OptionData(item.Data);
                    options.Add(tData);
                }
            }
            value = -1;
            onValueChanged.AddListener(ValueChange);
        }

       
    }


    private void ValueChange(int ii)
    {
        if (Node.Nodes[value].Nodes.Count != 0)
        {
            CreateThirdSift();
        }
        //TODO
    }

    private void CreateThirdSift()
    {
        AdaptiveObject.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        AdaptiveObject.GetComponent<CanvasGroup>().interactable = true;
        AdaptiveObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        AdaptiveObject.AddComponent<ThirdLevelSift>().Init(Node.Nodes[value]);
       // AdaptiveObject.transform.parent = transform;
        AdaptiveObject.transform.position = transform.position + Vector3.right * 40 ;
        AdaptiveObject.transform.SetAsLastSibling();
        //nextLevelDropdown[value] = true;
    }

}

