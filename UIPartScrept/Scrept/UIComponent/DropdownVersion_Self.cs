using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownVersion_Self : UIBehaviour
{
    [SerializeField]private GameObject content;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Text showName;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button arrowButton;
    private bool firsted = false;

    public Text ShowName
    {
        get
        {
            return showName;
        }
    }

    public void Init(BoTree<string> root)
    {
        showName.text = root.Data;
        for (int i = 0; i < root.Nodes.Count; i++)
        {
            GameObject go = AllUIManager.Instance.GetUIObject(UIObjectName.SiftingButton);
            go.GetComponent<SiftingButton>().Init(root.Nodes[i]);
            go.transform.parent = content.transform;
            go.GetComponent<ButtonExtention>().onClick.AddListener(Exit);
            //go.GetComponent<ButtonExtention>().onClick.AddListener(ChangeText(root.Nodes[i]));
        }
        titleButton.onClick.AddListener(Click);
        arrowButton.onClick.AddListener(Click );
    }

    private void Click()
    {
        arrowImage.rectTransform.Rotate(arrowImage.rectTransform.pivot,180);
        content.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        content.GetComponent<CanvasGroup>().interactable = true;
        content.GetComponent<CanvasGroup>().blocksRaycasts = true;
        content.transform.SetAsLastSibling();
    }

    private void Exit()
    {
        arrowImage.rectTransform.Rotate(arrowImage.rectTransform.pivot, 180);
        content.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        content.GetComponent<CanvasGroup>().interactable = false;
        content.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
