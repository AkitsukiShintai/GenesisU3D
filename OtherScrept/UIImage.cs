using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImage : MonoBehaviour {

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 设置Text位置，用于做自适应
    /// </summary>
    public void SetPositionFit(GameObject _fitFather)
    {

        if (_fitFather.GetComponent<VerticalLayoutGroup>() == null || _fitFather.GetComponent<GridLayoutGroup>() == null || _fitFather.GetComponent<HorizontalLayoutGroup>() == null)
        {
            Debug.Log("Cannot find the Father's any <LayoutGroup> component, please check.");
            return;
        }
        else
        {
            if (GetComponent<LayoutElement>() == null)
            {
                gameObject.AddComponent<LayoutElement>();
            }
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.parent = _fitFather.transform;
        }
    }

    public void InitImage(string _imagePath) {
        if (_imagePath == null)
        {

        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>(_imagePath);
            if (image != null)
            {
                image.sprite = sprite;
            }
            else
            {
                Debug.Log("加载图片出错");
            }
            //image.enabled = true;
        }
    }

    public void InitImage(Sprite _sprite)
    {
        image.sprite = _sprite;
    }

    /// <summary>
    /// 设定Image 全部属性
    /// </summary>
    public void SetImage(Vector2 _size = default(Vector2), Color _color = default(Color), Sprite _sprite = null, Material _material = null)
    {
        if (_size == default(Vector2))
        {

        }
        else
        {
            SetImage(_size);
        }
        if (_color == default(Color))
        {

        }
        else
        {
            SetImage(_color);
        }
        if (_sprite == null)
        {

        }
        else
        {
            SetImage(_sprite);
        }
        if (_material == null)
        {

        }
        else
        {
            SetImage(_material);
        }
    }


    /// <summary>
    /// 设定Image sprite属性
    /// </summary>
    public void SetImage(Sprite _sprite)
    {
        image.sprite = _sprite;
    }


    /// <summary>
    /// 设定Image sprite属性
    /// </summary>
    public void SetImage(string _imagePath)
    {
        if (_imagePath == null)
        {

        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>(_imagePath);
            if (image != null)
            {
                image.sprite = sprite;
            }
            else
            {
                Debug.Log("加载图片出错");
            }
            //image.enabled = true;
        }
    }


        /// <summary>
        /// 设定Image color属性
        /// </summary>
        public void SetImage(Color _color)
    {
        image.color = _color;
    }
    /// <summary>
    /// 设定Image material属性
    /// </summary>
    public void SetImage(Material _material)
    {
        image.material = _material;
    }

    /// <summary>
    /// 设定Image Size属性
    /// </summary>
    public void SetImage(float width, float height)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    /// <summary>
    /// 设定Image Size属性
    /// </summary>
    public void SetImage(Vector2 _size)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = _size;

    }





}
