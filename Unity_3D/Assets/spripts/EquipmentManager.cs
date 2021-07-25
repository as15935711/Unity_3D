using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("裝備道具1 - 5")]
    public Transform[] traEquipmentItem;

    
    /// <summary>
    /// 當前裝備的編號: 0 - 4
    /// </summary>
    private int indexEquipment;
    /// <summary>
    /// 選取邊框 - 顯示選取裝備道具用
    /// </summary>
    private Transform traSelectionOutline;
    /// <summary>
    /// 選取邊框 - 選取邊框的UI 變型元件
    /// </summary>
    private RectTransform rectSelectionOutline;
    /// <summary>
    /// 道具管理器 : 需要取得裡面的陣列資料 Item 陣列
    /// </summary>
    private Inventory inventory;
    /// <summary>
    /// 顯示道具的位置 : 手部骨架內的空物件
    /// </summary>
    private Transform traPropPosition;


    private void Start()
    {
        traSelectionOutline = GameObject.Find("選取邊框").transform;
        rectSelectionOutline = traSelectionOutline.GetComponent<RectTransform>();
        inventory = GameObject.Find("道具管理器").GetComponent<Inventory>();
        traPropPosition = GameObject.Find("顯示道具的位置").transform;
    }
    private void Update()
    {
        SwitchEquipment();
    }
    /// <summary>
    /// 切換裝備
    /// 透過滾輪切換
    /// </summary>
    private void SwitchEquipment()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");          //滾輪的值

        if (wheel < 0)                                         //如果 往後捲
        {
            indexEquipment++;                                     //編號遞增
            if (indexEquipment == 5) indexEquipment = 0;          //如果編號超出範圍就回到零
            SetSelectionEquipment();
        }
        else if (wheel > 0)
        {
            indexEquipment--;                                     //編號遞增
            if (indexEquipment == -1) indexEquipment = 4;          //如果編號超出範圍就回到零
            SetSelectionEquipment();
        }
    }
    private void SetSelectionEquipment()
    {
        traSelectionOutline.SetParent(traEquipmentItem[indexEquipment]);
        rectSelectionOutline.anchoredPosition = Vector2.zero;
    }
}
