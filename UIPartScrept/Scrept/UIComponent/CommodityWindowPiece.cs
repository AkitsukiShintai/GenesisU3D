using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CommodityWindowPiece : MonoBehaviour
{

    [SerializeField] private Image showImage;
    [SerializeField] private Text showText;
    [SerializeField] private Button tryToUseButton;
    [SerializeField] private Button collectButton;


    public void Init(Commodity commodity)
    {
        StartCoroutine(LoadImag(commodity));
        showText.text = commodity.showName;
        tryToUseButton.onClick.AddListener(UseButtonClick);
        collectButton.onClick.AddListener(CollectionButtonClick);
    }


    private IEnumerator LoadImag(Commodity commodity)
    {
        WWW www = new WWW(commodity.imagePath);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.isDone)
            {
                //Debug.Log("加载完成");
                Texture2D tex = new Texture2D(100, 100);
                www.LoadImageIntoTexture(tex);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, 100,100), Vector2.zero);
                showImage.sprite = sprite;
            }
        }
    }

    private void CollectionButtonClick()
    {
        //TODO
    }

    private void UseButtonClick()
    {
        //TODO
    }


}
