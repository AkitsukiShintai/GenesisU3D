using GenesisWinForm;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using GenesisWinForm.G3DObject.Parameter;

public class ComboBoxMultiple : MonoBehaviour
{
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text[] Number;
    [SerializeField]
    private Button LeftButton;
    [SerializeField]
    private Button RightButton;

    private List<string> IndexString = new List<string>{};
    private List<int> Index = new List<int>();

    //private float OriginNumber;
    private int value = 0;
    private ParameterCombo parameter;


    public void Init(ParameterBase _par, GenesisWinForm.G3DObject.G3DObjectPartBase _AboutShoe)
    {
        parameter = (ParameterCombo)_par;
        Name.text = parameter.ShowName;
        IndexString = parameter.Items.ToList();
        //Number.text = string.Empty;
        value = parameter.Value;
        SetValue(parameter.Value);
    //Number.text =  Number.text.Trim();
     }

    private void SetValue(int x)
    {
        Index.Clear();
        if (x < 2 || x > parameter.Items.Length - 3)
        {
            int start = x - 2;
            for (int i = 0; i < 5; i++)
            {
                if (start + i < 0)
                {
                    Index.Add(parameter.Items.Length + start + i); 
                }
                else if(start + i> parameter.Items.Length -1)
                {
                    Index.Add(start + i - parameter.Items.Length);
                }
                else
                {
                    Index.Add(start + i);
                }
            }
        }
        else
        {
            int start = x - 2;
            for (int i = 0; i < 5; i++)
            {
                Index.Add(start + i);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            Number[i].text = IndexString[Index[i]];
            //Index.Add(i);
        }
    }



    private void ValueDown()
    {
        value--;
        if (value < 0)
        {
            value = parameter.Items.Length - 1;
        }

        SetValue(value);
        parameter.Value = value;
    }


    private void ValueUp()
    {
        value++;
        if (value > parameter.Items.Length - 1)
        {
            value = 0;
        }
        SetValue(value);
        parameter.Value = value;
    }


    /// <summary>
    /// 左键点击
    /// </summary>
    public void LiftButtonClick()
    {
       // Debug.Log("Click");
         ValueDown();
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
        Debug.Log(parameter.Value);
    }

    /// <summary>
    /// 右键点击
    /// </summary>
    public void RightButtonClick()
    {
        //Debug.Log("Click");
        ValueUp();
        GenesisWinForm.ApplicationManager.Instance.GetCreaterManager().ShowMesh();
         Debug.Log(parameter.Value);
    }

}
