using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    [HideInInspector]     //將public 公開的欄位隱藏
    /// <summary>
    /// 是否有道具
    /// </summary>
    public bool hasProp;

    /// <summary>
    /// 道具圖示
    /// </summary>
    private Image imgProp;
    /// <summary>
    /// 道具數量
    /// </summary>
    private Text textProp;

    private void Start()
    {
        imgProp = transform.Find("道具圖示").GetComponent<Image>();
        textProp = transform.Find("道具數量").GetComponent<Text>();
    }
}
