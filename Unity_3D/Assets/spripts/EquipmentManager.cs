using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    /// <summary>
    /// 當前裝備的編號: 0 - 4
    /// </summary>
    private int indexEquipment;
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

        if (wheel < -0.1f)                                         //如果 往後捲
        {
            indexEquipment++;                                     //編號遞增
            if (indexEquipment == 5) indexEquipment = 0;          //如果編號超出範圍就回到零
            print("裝備編號 :" + indexEquipment);
        }

    }
}
