using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SiftingButton : MonoBehaviour
{
    [SerializeField] private ButtonExtention button;
    private bool isThird = false;

    private bool notEnd = false;
    private BoTree<string> root;
    public void Init(BoTree<string> _node,bool _isThird = false)
    {
        root = _node;

        button.gameObject.transform.GetComponentInChildren<Text>().text = _node.Data;
        button.onClick.AddListener(Click);
        button.onClick.AddListener(ChangeText);
        if (root.Nodes.Count != 0)
        {
            button.InitThirdSift(root);
            notEnd = true;
        }

        isThird = _isThird;

    }

    private void Click()
    {
        if (!isThird)
        {
            //TODO
            ShopPanel panel = (ShopPanel) UIManager.Instance.GetPanelInstance(UIPanelType.ShopPanel);
            panel.Init();
            
            Debug.Log("点击了"+root.Data);
        }else if (isThird)
        {
            CreateThirdSift();
        }
    }

    private void CreateThirdSift()
    {
        GameObject AdaptiveObject = GameObject.FindGameObjectWithTag("AdaptiveObject");
        AdaptiveObject.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        AdaptiveObject.GetComponent<CanvasGroup>().interactable = true;
        AdaptiveObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        AdaptiveObject.AddComponent<ThirdLevelSift>().Init(root);
        // AdaptiveObject.transform.parent = transform;
        AdaptiveObject.transform.position = transform.position + Vector3.right * 90;
        AdaptiveObject.transform.SetAsLastSibling();
        //nextLevelDropdown[value] = true;
    }

    private void ChangeText()
    {
        if(!isThird)
            transform.parent.parent.GetComponent<DropdownVersion_Self>().ShowName.text = root.Data;
    }
}
