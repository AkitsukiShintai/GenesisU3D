using GenesisWinForm.G3DObject.Parameter;
using Page;
using UnityEngine;
using UnityEngine.UI;

public class ParameterButton : MonoBehaviour
{

    public Text Name;
    private Page2Data pageData;
    private ParameterBase parameter;
    private int page1Type;

    /// <summary>
    /// 初始化，配色初始化请使用重载 public void Init(Page2Data pg, ParameterBase _parameter)
    /// </summary>
    /// <param name="pg"></param>
    /// <param name="Page1Type"></param>
    public void Init(Page2Data pg, int Page1Type)
    {
        Name = transform.Find("Text").GetComponent<Text>();
        pageData = pg;
        Name.text = pg.Name;
        page1Type = Page1Type;
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    /// <summary>
    /// 配色面板专用初始化
    /// </summary>
    /// <param name="pg"></param>
    /// <param name="_parameter"></param>
    public void Init(Page2Data pg, ParameterBase _parameter)
    {
        parameter = _parameter;
        Name = transform.Find("Text").GetComponent<Text>();
        pageData = pg;
        Name.text = _parameter.ShowName;
        page1Type = 4;
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }
    private void ButtonClick()
    {
        if (page1Type == 0)//选材料
        {
            ShopPanel panel = (ShopPanel)UIManager.Instance.GetPanelInstance(UIPanelType.ShopPanel);
            panel.Init();//TODO 传入json数据
            UIManager.Instance.PushPanel(UIPanelType.ShopPanel);
        }
        else if (page1Type == 1)//楦型调整
        {
            LastAdjustParameterPanel panel = (LastAdjustParameterPanel)UIManager.Instance.GetPanelInstance(UIPanelType.LastAdjustParameterPanel);
            panel.Init(pageData);
            UIManager.Instance.PushPanel(UIPanelType.LastAdjustParameterPanel);
        }
        else if (page1Type == 2) //结构与装饰
        {
            //TODO
            if (pageData.Name == "结构线")
            {

            }
            else if (pageData.Name == "装饰线")
            {

            }
        }
        else if (page1Type == 3) //工艺
        {
            TechnicParameterPanel panel = (TechnicParameterPanel)UIManager.Instance.GetPanelInstance(UIPanelType.TechnicParameterPanel);
            panel.Init(pageData);
            UIManager.Instance.PushPanel(UIPanelType.TechnicParameterPanel);
        }else if (page1Type == 4) //配色
        {//TODO 后面根据情况进行筛选或重写
            if (parameter.fabricParameterType == ParameterBase.FabricParameterType.Parameter)
            {
                ColorMatchParameterPanel panel = (ColorMatchParameterPanel)UIManager.Instance.GetPanelInstance(UIPanelType.ColorMatchParameterPanel);
                panel.Init(pageData);
                UIManager.Instance.PushPanel(UIPanelType.ColorMatchParameterPanel);
            }
            else if(parameter.fabricParameterType == ParameterBase.FabricParameterType.Shop)
            {
                ShopPanel panel = (ShopPanel)UIManager.Instance.GetPanelInstance(UIPanelType.ShopPanel);
                panel.Init();
                UIManager.Instance.PushPanel(UIPanelType.ShopPanel);
            }
            else if (parameter.fabricParameterType == ParameterBase.FabricParameterType.ColorPanel)
            {
                //ColorPickerPanel panel = (ColorPickerPanel)UIManager.Instance.GetPanelInstance(UIPanelType.ColorPickerPanel);
                //panel
                UIManager.Instance.PushPanel(UIPanelType.ColorPickerPanel);
                UISeletedData.Instance.CurrentParameterImage = null;
            }
            else
            {
                ShopPanel panel = (ShopPanel)UIManager.Instance.GetPanelInstance(UIPanelType.ShopPanel);
                panel.Init();
                UIManager.Instance.PushPanel(UIPanelType.ShopPanel);
            }
        }else if (true)
        {

        }

    }
}
