using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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

        ShowEquipment();
    }
    /// <summary>
    /// 使用過的道具物件資訊
    /// </summary>
    public List<GameObject> listUsingItem = new List<GameObject>();
    /// <summary>
    /// 顯示裝備
    /// 還沒用過的裝備升成出來放進資料庫內，已經用過的從資料庫內拿
    /// 並且顯示在手上調整:座標 角度與尺寸
    /// </summary>
    private void ShowEquipment()
    {
        Item itemData = inventory.itemDataEquipment[indexEquipment];               //目前的道具資料

        if (itemData.goItem)
        {
           //判斷 清單內的道具名稱 是否包含當前選取的道具名稱. 例: 草地(Clone) 包含草地 就表示用過
            int count = listUsingItem.Where(x => x.name.Contains(itemData.goItem.name)).ToList().Count;
          

            //隱藏所有使用過的道具
            for (int i = 0; i < listUsingItem.Count; i++) listUsingItem[i].SetActive(false);
            
            // 如果 清單內 沒有使用過此道句 就處理生成
            if (count == 0)
            {
                //還沒使用過先生成
                GameObject equip = Instantiate(itemData.goItem, traPropPosition);
                equip.transform.localScale = Vector3.one * 0.3f;
                equip.transform.localPosition = Vector3.zero;

                listUsingItem.Add(equip);
            }
            
           
            else
            {
                // 否則 就處理 隱藏與顯示
                print("已經有此道具");
            }
         

            print("目前裝備的道具名稱:" + itemData.goItem.name);

            print("使用過的道具名稱:" + listUsingItem[0].name);
        }
    }
}
