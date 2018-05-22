using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenesisWinForm;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class SliderControl : MonoBehaviour {

    //private GenesisWinForm.G3DObject.G3DUnionManager shou;
    public float width;//该slider的宽
    public float height;//该slider的高
    public int count = 0;//如果为combo，表示段数，double无意义
    public List<float> pointList = new List<float>();//存放所有点所占有的百分比，combo用
    public GenesisWinForm.G3DObject.Parameter.ParameterBase par;//保存该Slider所对应的par
    public GameObject textPrefab;
    private void Awake()
    {
        width = GetComponent<RectTransform>().rect.width;
        height = GetComponent<RectTransform>().rect.height;

        //SetSize(0f, 0f,100f,100f);

        //GetComponent<RectTransform>().sizeDelta = new Vector2(100, 200);

        //Debug.Log(GetComponent<RectTransform>().rect.width);

    }

    public void SetSize(float _width,float _height) {//设置该slider的大小

        GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _height);
       // GetComponent<RectTransform>().rect.
        width = _width;
        height = _height;
    }

    public void SetPosition(float _x,float _y) {//设置slider位置
        GetComponent<RectTransform>().localPosition = new Vector3(_x, _y, 0);

    }

    public void Init(GenesisWinForm.G3DObject.Parameter.ParameterBase _par,float _x= 0,float _y= 0,float _width= 160,float _height = 20)//初始化该Slider
    {
        this.transform.parent = ParameterTest.UIRoot.transform;
        par = _par;
        //设置position
       
         SetPosition(_x, _y);
        
        //设置size
        if (_width == 0 || _height == 0)
        {

        }
        else
        {
            SetSize(_width, _height);
        }



        //shou = ApplicationManager.Instance.GetCreaterManager();
        if (_par.ValueType == GenesisWinForm.G3DObject.Parameter.ParameterBase.eValueType.Combo) {//如果为Combo类型的参数
            GenesisWinForm.G3DObject.Parameter.ParameterCombo combo = (GenesisWinForm.G3DObject.Parameter.ParameterCombo)_par;
            count = combo.Items.Length;
            if (count > 0) {//

                for (int i = 0; i < count; i++) {//做循环，将combo字段显示到sliderbar下面
                    GameObject go = GameObject.Instantiate(textPrefab);
                    go.transform.parent = this.transform;
                    go.GetComponent<Text>().text = combo.Items[i];
                    go.transform.localPosition = new Vector3(-width / 2 + width / (count - 1) * i, -height / 4, 0);
                    pointList.Add((width / (count - 1) * i) / width);
                }
            }
            if(gameObject.GetComponent<ComboSlider>() == null)
                gameObject.AddComponent<ComboSlider>();
            //动态添加事件相应
            // UnityAction<float> unityAction;
            // unityAction = ChangeValue;
            EventTrigger eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {

                gameObject.AddComponent<EventTrigger>();
            }
            //UnityAction<float> action = new UnityAction<float>(GetComponent<ComboSlider>().ChangeValue);
            eventTrigger.triggers = new List<EventTrigger.Entry>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.EndDrag;
            entry2.eventID = EventTriggerType.PointerUp;
            entry.callback = new EventTrigger.TriggerEvent();
            entry2.callback = new EventTrigger.TriggerEvent();
            //UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(GetComponent<ComboSlider>().ChangeValue);
            entry.callback.AddListener(ComboAction);
            entry2.callback.AddListener(ComboAction);
            eventTrigger.triggers.Add(entry);
            eventTrigger.triggers.Add(entry2);
            //GetComponent<Slider>().OnPointerUp(ComboAction);//. AddListener(GetComponent<ComboSlider>().ChangeValue);
            Debug.Log("添加事件完成");


        }
    }



    public void Init1(GenesisWinForm.G3DObject.Parameter.ParameterBase _par)//初始化该Slider
    {
        this.transform.parent = GameObject.FindGameObjectWithTag("HeelLayoutGroup").transform;
        par = _par;
        //shou = ApplicationManager.Instance.GetCreaterManager();
        if (_par.ValueType == GenesisWinForm.G3DObject.Parameter.ParameterBase.eValueType.Combo)
        {//如果为Combo类型的参数
            GenesisWinForm.G3DObject.Parameter.ParameterCombo combo = (GenesisWinForm.G3DObject.Parameter.ParameterCombo)_par;
            count = combo.Items.Length;
            if (count > 0)
            {//

                for (int i = 0; i < count; i++)
                {//做循环，将combo字段显示到sliderbar下面
                    GameObject go = GameObject.Instantiate(textPrefab);
                    go.transform.parent = this.transform.Find("Panel").transform;
                    go.GetComponent<Text>().text = combo.Items[i];
                    go.transform.localPosition = new Vector3(-width / 2 + width / (count - 1) * i, 0 , 0);
                    pointList.Add((width / (count - 1) * i) / width);
                }
                //transform.Find("Panel").GetComponent<HorizontalLayoutGroup>().spacing = (width-(35/(count-1))*count) /count;
            }
            if (gameObject.GetComponent<ComboSlider>() == null)
                gameObject.AddComponent<ComboSlider>();
            //动态添加事件相应
            // UnityAction<float> unityAction;
            // unityAction = ChangeValue;
            EventTrigger eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {

                gameObject.AddComponent<EventTrigger>();
            }
            //UnityAction<float> action = new UnityAction<float>(GetComponent<ComboSlider>().ChangeValue);
            eventTrigger.triggers = new List<EventTrigger.Entry>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.EndDrag;
            entry2.eventID = EventTriggerType.PointerUp;
            entry.callback = new EventTrigger.TriggerEvent();
            entry2.callback = new EventTrigger.TriggerEvent();
            //UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(GetComponent<ComboSlider>().ChangeValue);
            entry.callback.AddListener(ComboAction);
            entry2.callback.AddListener(ComboAction);
            eventTrigger.triggers.Add(entry);
            eventTrigger.triggers.Add(entry2);
            //GetComponent<Slider>().OnPointerUp(ComboAction);//. AddListener(GetComponent<ComboSlider>().ChangeValue);
            Debug.Log("添加事件完成");


        }
    }


    public void ComboAction(BaseEventData x) {

        GetComponent<ComboSlider>().ChangeValue();

    }

    //public void Init(GenesisWinForm.G3DObject.Parameter.ParameterBase par,
    

}
