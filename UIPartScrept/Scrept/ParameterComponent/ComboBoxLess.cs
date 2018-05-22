using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenesisWinForm.G3DObject.Parameter;
using UnityEngine;
using UnityEngine.UI;

public class ComboBoxLess : MonoBehaviour
{

    [SerializeField]
    private Text Name;
    [SerializeField] private List<GameObject> Buttons;

    private List<string> IndexString = new List<string> { };
    private ParameterCombo parameter;

    public void Init(ParameterBase _par, GenesisWinForm.G3DObject.G3DObjectPartBase _AboutShoe)
    {
        parameter = (ParameterCombo) _par;
        Name.text = parameter.ShowName;
        IndexString = parameter.Items.ToList();
        for (int i = 0; i < IndexString.Count; i++)
        {
            Buttons[i].GetComponentInChildren<Text>().text = IndexString[i];
            Buttons[i].name = IndexString[i];
            Buttons[i].SetActive(true);
            Buttons[i].GetComponent<Button>();
        }

        for (int i = IndexString.Count; i < Buttons.Count; i++)
        {
            Buttons[i].SetActive(false);
        }

        

    }


    public void ButtonOneClick()
    {
        parameter.Value = 0;
    }

    public void ButtonTwoClick()
    {
        parameter.Value = 1;
    }

    public void ButtonThreeClick()
    {
        parameter.Value = 2;
    }

    public void ButtonFourClick()
    {
        parameter.Value = 3;
    }

}
