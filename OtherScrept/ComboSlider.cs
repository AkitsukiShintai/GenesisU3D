using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GenesisWinForm;


public class ComboSlider : MonoBehaviour
{

    private Slider slider;
    private int count;//记录分段数
    private List<float> pointList = new List<float>();
    private bool isChanging = false;
    public GenesisWinForm.G3DObject.Parameter.ParameterCombo par;

    int index = 0;//记录当前选择的第几个



    public int GetIndex()
    {

        return index;

    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        //count = 3;
        //pointList.Add(0f);
        //pointList.Add(0.5f);
        //pointList.Add(1f);//测试

        count = GetComponent<SliderControl>().count;
        pointList = GetComponent<SliderControl>().pointList;
        par = (GenesisWinForm.G3DObject.Parameter.ParameterCombo)GetComponent<SliderControl>().par;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            // RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            //if(hit.collider == )
            // Debug.Log("jinru");
            //Control();
        }
    }

    //由于Combo只能取某些值，所以该脚本用于控制ComboSlider的进度条的位置并更新
    public void ChangeValue()//GenesisWinForm.G3DObject.Parameter.ParameterCombo par
    {
        if (!isChanging)
        {
            isChanging = true;
            float value = slider.value;
            float temp = 0f;
            index = 0;
            if (count > 0)
            {
                foreach (float t in pointList)//找到一个小于value的拥有最大x值的位置，存储在temp中
                {
                    if (t < value && t > temp)
                    {
                        temp = t;
                    }
                }
            }
            index = pointList.IndexOf(temp);
            //Debug.Log("temp:" + temp);
            //Debug.Log(temp + GetComponent<RectTransform>().rect.width / (count - 1));

            if (value - temp <= pointList[index + 1] - value)
            {
                slider.value = temp;
            }
            else
            {
                slider.value = pointList[index + 1];
                index++;
            }
            par.Value = index;
            ParameterTest.Instance.shou.ShowMesh();
            Debug.Log("Value:" + index);
            isChanging = false;
        }
    }


}
