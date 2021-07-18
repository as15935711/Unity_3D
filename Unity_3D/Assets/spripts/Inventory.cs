using UnityEngine;
using System.Linq;
using System.Collections.Generic; //引用 系統 集合 API
/// <summary>
/// 道具欄管理系統
/// 吃到道具後累加
/// 道具欄顯示系統
/// 裝備道具欄
/// </summary>
public class Inventory : MonoBehaviour
{
    #region 欄位
    [Header("道具清單")]
    /// <summary>
    /// 道具清單
    /// </summary>
    public List<Prop> props = new List<Prop>();
    [Header("道具欄")]
    public GameObject goInventory;
    [Header("裝備的道具欄 - 5 個")]
    public InventoryItem[] itemEquipment;
    [Header("道具欄 - 24 個")]
    public InventoryItem[] itemProp;
    #endregion


    #region 事件
    private void Start()
    {
        goInventory.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        goInventory.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        SwitchInventory();
    }

    #endregion



    #region 方法
    /// <summary>
    /// 切換道具欄戒面顯示與隱藏
    /// </summary>
    private void SwitchInventory()
    {
        //如果 按下 E 道具就設定為相反的顯示狀態
        if (Input.GetKeyDown(KeyCode.E)) goInventory.SetActive(!goInventory.activeInHierarchy);
    }

    /// <summary>
    /// 添加道具: 玩家吃到道具後呼叫
    /// </summary>
    public void AddProp(Prop prop)
    {
     

        props.Add(prop);          //添加吃到的道具到清單內
        ObjectPoolUseing(prop.gameObject);    //丟進物件池
        ShowPropInInventory(prop);
    }

    private void ShowPropInInventory(Prop prop)
    {
       if( UpdateItem(prop, itemEquipment))
        UpdateItem(prop, itemProp);
    }
    /// <summary>
    /// 更新裝備與道具欄每一格欄位
    /// </summary>
    /// <param name="prop">吃到的道具資訊</param>
    /// <param name="items">道具欄陣列 - 裝被或者道具</param>
    /// <returns>是否道具已放滿</returns>
    private bool UpdateItem(Prop prop, InventoryItem[] items)
    {
        for (int i = 0; i < items.Length; i++)                                   //迴圈執行 裝被道具欄 - 5個
        {
            if (items[i].hasProp && items[i].imgProp.sprite == prop.sprProp)    //如果格子內有道具 並且 跟當前吃到的道具香同 就累加
            {
                //(x => ***)  Lambda 減寫
                // 數量 = 道具清單.查找(查找與 當前道具. 圖片 香彤的道具資料).轉清單().數量
               int count = props.Where(x => x.sprProp == prop.sprProp).ToList().Count;   

                   items[i].textProp.text = count + "";                        
                   return false;
            }
            else if (!items[i].hasProp)                                        //如果 裝被道具欄 沒有道具 才可以放道具
            {
                items[i].hasProp = true;
                items[i].imgProp.enabled = true;                              //更新圖片
                items[i].imgProp.sprite = prop.sprProp;                       //放入圖片
                items[i].textProp.text = 1 + "";                              //更新數量
                return false;                                                 //跳出  break 僅跳出迴圈,  return 跳出整個方法
            }
        }
        return true;                                                         //已經塞滿道具
    }
    /// <summary>
    /// 物件池使用中的道具: 放在遠處
    /// </summary>
    /// <param name="obProp"></param>
    private void ObjectPoolUseing(GameObject obProp)
    {
        obProp.transform.position = Vector3.one * -99999;


        #region 關閉元件
        obProp.GetComponent<Collider>().enabled = false;
        obProp.GetComponent<ConstantForce>().enabled = false;
        obProp.GetComponent<Rigidbody>().Sleep();
        obProp.GetComponent<Rigidbody>().useGravity = false;
        #endregion
    }

    #endregion
}
