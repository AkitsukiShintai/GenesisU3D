using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenesisWinForm.G3DObject.Parameter;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class ComboBoxDouble : MonoBehaviour
{



    [SerializeField] private Text Name;
    [SerializeField] private List<GameObject> Buttons;
    [SerializeField]
    private Button LeftButton;
    [SerializeField]
    private Button RightButton;

    [SerializeField] private CanvasGroup BarCanvasGroup;
    [SerializeField] private List<Text> Texts;
    [SerializeField] private Slider Slider;
    private List<string> IndexString = new List<string> { "-0.3","-0.3","-0.2","-0.1","0","0.1" };//combo数据列表
    private List<float> IndexFloat = new List<float> {0,1,2,3,4,5,6 };//double数据列表

    private float max = 0;
    private float min = 0;
    private float step = 0;
    private List<int> Index = new List<int>{0,1,2,3,4};//double控件用来保存下方数字位置
    private int currentIndex = 0;
    private int changeIndex = 0;
    private ParameterCombo parameter1;
    private List<ParameterDouble>  parameter2 = new List<ParameterDouble>();



    void Start()
    {
        //BuildString(Index);
    }

    public void Init(ParameterBase _par, GenesisWinForm.G3DObject.G3DObjectPartBase _AboutShoe)
    {
        IndexFloat.Clear();
        parameter1 = (ParameterCombo)_par;
        


        foreach (var t in parameter1.ComboxDoubleUnion)
        {
            parameter2.Add((ParameterDouble)t);
        }
        //ParameterDouble defult = parameter2[parameter1.Value];

        //初始化combo参数
        Name.text = parameter1.ShowName;
        IndexString = parameter1.Items.ToList();
        for (int i = 0; i < IndexString.Count; i++)
        {
            Buttons[i].GetComponentInChildren<Text>().text = IndexString[i];
            Buttons[i].SetActive(true);
        }

        for (int i = IndexString.Count; i < Buttons.Count; i++)
        {
            Buttons[i].SetActive(false);
        }

        if (parameter2[parameter1.Value] != null)
        {
            ReStartDoubleBox();
        }
        else
        {
            StopDoubleBox();
        }
    }


    private void ReStartDoubleBox()
    {

        BarCanvasGroup.alpha = 1;
        //BarCanvasGroup.blocksRaycasts = false;
        BarCanvasGroup.interactable = true;
        //初始化double参数
        max = parameter2[parameter1.Value].Max;
        min = parameter2[parameter1.Value].Min;
        step = parameter2[parameter1.Value].Step;
        int count = (int)((max - min) / step) + 1;
        //Number.text = string.Empty;
        //获得滚动条列表
        for (int i = 0; i < count; i++)
        {
            //Number.text += [i];
            //Number.text += "          ";
            //Index.Add(i);
            float t = min + i * step;

            IndexFloat.Add(t);
        }

        if (count < 6)
        {
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }



        for (int i = 0; i < 5; i++)
        {
            Texts[i].text = IndexFloat[i].ToString();

            Index.Add(i);
        }

        Slider.value = 0.166f;
        //Number.text = Number.text.Trim();
    }

    private void StopDoubleBox()
    {
        BarCanvasGroup.alpha = 0.5f;
        //BarCanvasGroup.blocksRaycasts = false;
        BarCanvasGroup.interactable = false;
    }



    private void ValueDown()
    {
        for (int i = 0; i < 5; i++)
        {
            Index[i]--;
            if (Index[i] < 0)
            {
                Index[i] = IndexFloat.Count - 1;
            }

        }
        BuildString(Index);
        //return Index[2];

    }


    private void ValueUp()
    {
        for (int i = 0; i < 5; i++)
        {
            Index[i]++;
            if (Index[i] == IndexFloat.Count)
            {
                Index[i] = 0;
            }

        }

        BuildString(Index);
        //return Index[2];
    }


    /// <summary>
    /// 左键点击
    /// </summary>
    public void LiftButtonClick()
    {
        Debug.Log("Click");
        ValueDown();

        parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];
        Debug.Log(parameter2[parameter1.Value].Value);
    }

    /// <summary>
    /// 右键点击
    /// </summary>
    public void RightButtonClick()
    {
        Debug.Log("Click");
        ValueUp();
        parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];
        Debug.Log(parameter2[parameter1.Value].Value);
    }


    private void BuildString(List<int> indexList)
    {
        //string temp = string.Empty;
        for (int i = 0; i < Texts.Count; i++)
        {
            Texts[i].text = IndexFloat[indexList[i]].ToString();
        }
    }


    public void ButtonOneClick()
    {
        parameter1.Value = 0;
        if (parameter2[parameter1.Value] != null)
        {
            ReStartDoubleBox();
        }
        else
        {
            StopDoubleBox();
        }
    }

    public void ButtonTwoClick()
    {
        parameter1.Value = 1;
        if (parameter2[parameter1.Value] != null)
        {
            ReStartDoubleBox();
        }
        else
        {
            StopDoubleBox();
        }
    }

    public void ButtonThreeClick()
    {
        parameter1.Value = 2;
        if (parameter2[parameter1.Value] != null)
        {
            ReStartDoubleBox();
        }
        else
        {
            StopDoubleBox();
        }
    }

    public void ButtonFourClick()
    {
        parameter1.Value = 3;
        if (parameter2[parameter1.Value] != null)
        {
            ReStartDoubleBox();
        }
        else
        {
            StopDoubleBox();
        }
    }



    public void SliderValueChange(float value)
    {
        if (value < 0.249f)
        {
            
            Slider.value = 0.166f;
            changeIndex = 0;
            if (currentIndex == changeIndex)
            {
                return;
            }
            //Debug.Log("值改变");

            currentIndex = 0;
            parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];
        }
        else if (value<0.415f)
        {
            //Debug.Log("1");
            Slider.value = 0.332f;
            changeIndex = 1;
            if (currentIndex == changeIndex)
            {
                return;
            }
            currentIndex = 1;
            //Debug.Log("值改变");


            parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];

        }
        else if (value < 0.581f)
        {
            Slider.value = 0.5f;

            changeIndex = 2;
            if (currentIndex == changeIndex)
            {
                return;
            }
            //Debug.Log("值改变");

            currentIndex = 2;
            parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];

        }
        else if (value < 0.747f)
        {
            Slider.value = 0.666f;

            changeIndex = 3;
            if (currentIndex == changeIndex)
            {
                return;
            }
            //Debug.Log("值改变");

            currentIndex = 3;

            parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];

        }
        else 
        {
            Slider.value = 0.832f;

            changeIndex = 4;
            if (currentIndex == changeIndex)
            {
                return;
            }
            //Debug.Log("值改变");

            currentIndex = 4;
            parameter2[parameter1.Value].Value = IndexFloat[Index[currentIndex]];

        }
    }
}
