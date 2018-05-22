using System.Collections;
using System.Collections.Generic;
using GenesisWinForm.G3DObject.Parameter;
using UnityEngine;
using UnityEngine.UI;


public class ColorSelect : MonoBehaviour
{
    [SerializeField]
    private Text Text;

    [SerializeField] private Slider slider;
    [SerializeField] private GameObject Button;
    [SerializeField] private Image Image;
    [SerializeField] private TextExtention Name;
    private ParameterColor4 parameter;
    //private Color color;

    public void Init(ParameterBase _par, GenesisWinForm.G3DObject.G3DObjectPartBase _AboutShoe)
    {
        parameter = (ParameterColor4) _par;
        Name.text = parameter.ShowName;
        Image.color = parameter.Value;
        slider.value = 1-parameter.Value.a;
        slider.onValueChanged.AddListener(ValueChange);
    }



    private void ValueChange(float value)
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 1-value);
        Text.text = (value*100).ToString("f1")+"%";
        parameter.Value = Image.color;
    }


    public void ButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ColorPickerPanel);
        UISeletedData.Instance.CurrentParameterImage = Image;
    }
}
