using UnityEngine;
/// <summary>
/// 採集物件:儲存採集物件資料以及功能
/// </summary>
public class ObjectCollection : MonoBehaviour
{
    [Header("採集物件資料")]
    public DataCollection data;
    [Header("採集物爆炸")]
    public GameObject ObjExplosion;
    private float hp;

    private void Start()
    {
        hp = data.hp;
    }
    /// <summary>
    /// 受到攻擊傷害
    /// </summary>
    /// <param name="damage">受到的傷害值</param>
    public void Hit(float damage)
    {
        hp -= damage;

        if (hp <= 0) Dead();
    }


    /// <summary>
    /// 採集物件毀損死亡
    /// </summary>
    private void Dead()
    {
        Destroy(gameObject);
        Instantiate(data.objDrop, transform.position, Quaternion.Euler(0, 45, 0));
        Instantiate(ObjExplosion, transform.position, Quaternion.identity);
    }


}
