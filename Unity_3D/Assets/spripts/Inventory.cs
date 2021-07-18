using UnityEngine;
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
    public void AddProp(Prop propName)
    {
     

        props.Add(propName);          //添加吃到的道具到清單內
        ObjectPoolUseing(propName.gameObject);    //丟進物件池
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
