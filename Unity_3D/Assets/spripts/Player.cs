using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    private Rigidbody rig;
    private Animator ani;
    private Transform traCamera;
    #endregion


    #region 事件
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = transform.Find("男生").GetComponent<Animator>();
        traCamera = GameObject.Find("攝影機").transform;
    }
    #endregion


    #region 方法

    #endregion
}
