using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenesisWinForm;
using GenesisWinForm.G3DObject.Ornament.Function;
using UnityEngine.EventSystems;

public class ParameterTest : MonoSingleton<ParameterTest> {

    public GenesisWinForm.G3DObject.G3DUnionManager shou;
    public List<GenesisWinForm.G3DObject.Parameter.ParameterBase> parList;
    public int showCount = 0;


    public GameObject testPrefab;
    public GameObject lablePrefab;
    public GameObject sliderPrefab;
    public static GameObject UIRoot;

    private bool isValueChanged = false;


    private void Awake()
    {



        //UIRoot = GameObject.FindGameObjectWithTag("UIRoot");
        ////Debug.Log(default(Vector3));
        //GameObject go = Instantiate(lablePrefab);
        //go.GetComponent<UILable>().InitLable("测试1");

        
        //go = Instantiate(lablePrefab);
        //Debug.Log("初始化第二个");
        //go.GetComponent<UILable>().InitLable("测试2","Logo");


        // GameObject go = Instantiate(testPrefab);
    }

    //    void Update()
    //    {
    //        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
    //        {
    //#if IPHONE || ANDROID
    //			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
    //#else
    //            if (EventSystem.current.IsPointerOverGameObject())
    //#endif
    //                Debug.Log("当前触摸在UI上");

    //            else
    //                Debug.Log("当前没有触摸在UI上");
    //        }
    //    }
    //private void ChangeHeel(int value)
    //{
    //    shou = ApplicationManager.Instance.GetCreaterManager();
    //    par = shou.ObjShoe.Heel.Subject.M_HeelType;

    //    par.Value = value;



    //    //shou.ObjShoe.Heel.Subject.M_HeelType.Value = value;
    //}

    public void Click() {

        shou = ApplicationManager.Instance.GetCreaterManager();
        Debug.Log("click");
       // GenesisWinForm.G3DObject.G3DCore.G3DCore3DObjectOptimize op = new GenesisWinForm.G3DObject.G3DCore.G3DCore3DObjectOptimize();

        //To3Dfunction.CreateCube(1f,1f,1f,);
        //parList = shou.ObjShoe.Heel.Subject.ParameterList;
        //for (int i=  0;i<parList.Count; i++){
        //    if (parList[i].IsShow && parList[i].ValueType == GenesisWinForm.G3DObject.Parameter.ParameterBase.eValueType.Combo) {
        //        CreateSlider1(parList[i]);
        //        showCount++;
        //    }

        //}
        //ApplicationManager.Instance.s
        //To3Dfunction.JianMian(, 0.01f, 0.001f, 10000);
        //Debug.Log(shou.ObjShoe.g3dObject[0])
        //CreateSlider(par);
        //shou.ObjShoe.Heel;

    }

    public void IsSliderValuseChanged() {
        isValueChanged = true;

    }



    public void CreateSlider1(GenesisWinForm.G3DObject.Parameter.ParameterBase _par)
    {

        GameObject sliderGo = Instantiate(sliderPrefab);
        sliderGo.GetComponent<SliderControl>().Init1(_par);

    }


    public void CreateSlider(GenesisWinForm.G3DObject.Parameter.ParameterBase _par,float _x=0,float _y=0,float _width=160,float _height=20) {
      
        GameObject sliderGo = Instantiate(sliderPrefab);
        sliderGo.GetComponent<SliderControl>().Init(_par,_x,_y,_width,_height);

    }

    
}
