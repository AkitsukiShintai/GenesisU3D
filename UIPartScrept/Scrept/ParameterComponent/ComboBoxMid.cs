using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenesisWinForm.G3DObject.Parameter;
using UnityEngine;
using UnityEngine.UI;

public class ComboBoxMid : MonoBehaviour {

    [SerializeField]
    private Text Name;
    [SerializeField] private List<GameObject> Buttons;

    private List<string> IndexString = new List<string> { };
    private ParameterCombo parameter;

    public void Init(ParameterBase _par, GenesisWinForm.G3DObject.G3DObjectPartBase _AboutShoe)
    {
        parameter = (ParameterCombo)_par;
        Name.text = parameter.ShowName;
        IndexString = parameter.Items.ToList();
        for (int i = 0; i < IndexString.Count; i++)
        {
            Buttons[i].GetComponentInChildren<Text>().text = IndexString[i];
            Buttons[i].SetActive(true);
        }

        for (int i = IndexString.Count; i < Buttons.Count; i++)
        {
            Buttons[i].SetActive(false);
        }

        Buttons[parameter.Value].GetComponent<Button>().Select();
    }

    public void ButtonOneClick()
    {
        parameter.Value = 0;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }

    public void ButtonTwoClick()
    {
        parameter.Value = 1;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }

    public void ButtonThreeClick()
    {
        parameter.Value = 2;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }

    public void ButtonFourClick()
    {
        parameter.Value = 3;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }
    public void ButtonFiveClick()
    {
        parameter.Value = 4;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }
    public void ButtonSixClick()
    {
        parameter.Value = 5;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }
    public void ButtonSevenClick()
    {
        parameter.Value = 6;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }
    public void ButtonEightClick()
    {
        parameter.Value = 7;
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
    }

}
