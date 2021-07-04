using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    #region 欄位
    private Transform traTerrainGroup;
    [Header("地形大小: 長")]
    public int x;
    [Header("地形大小:寬")]
    public int z;
    [Header("地形細節:坡度的起伏"),Tooltip("數字越大起伏越低，數字 1 為沒有起伏"),Range(1, 100)]
    public float detial;
    [Header("地形高度")]
    public float height;
    [Header("地形物件的高度")]
    public float heightTerrain = 0.8f;

    //陣列
    //語法: 類型後加上中括號[]
    //用途: 存放箱同類型的多筆資料
    //陣列內的資料都有編號，並且從0開始
    /// <summary>
    /// 用於存放地形物件，按順序為0草地、1泥土、2洞穴、3石頭、4岩漿
    /// </summary>
    [Header("地形物件:0 草地、1 泥土、2 洞穴、3 石頭、4 岩漿")]
    public GameObject[] objTerrains;

    [Header("玩家")]
    public Transform traPlayer;

    [Header("泥土範圍")]
    public Vector2 v2Dirty = new Vector2(1, 6);

   
    /// <summary>
    /// 隨機地形數值
    /// </summary>
    private int randomTerrain;

   
    private void Start()
    {
        traTerrainGroup = GameObject.Find("地形群組").transform;

        randomTerrain = Random.Range(1, 10000);                //取得隨機地形數值
        Generate();

        GeneratePlayer();

    }
    #endregion
    #region 方法
    /// <summary>
    /// 生成地形
    /// </summary>
    private void Generate()
    {
        // 取得陣列資料語法: 陣列名稱 [編號]


        for (int posX = 0; posX < x; posX++)
        {
            for (int posZ = 0; posZ < z; posZ++)
            {
                //浮點數轉整數
                int posY = (int)(Mathf.PerlinNoise(posX / detial + randomTerrain, posZ / detial + randomTerrain) * height);

                Vector3 pos = new Vector3(posX, posY * heightTerrain, posZ);
                Instantiate(objTerrains[0], pos, Quaternion.identity, traTerrainGroup);       // 生成(物件·座標·角度·父物件) 

                #region 第一層底下的物件處理: 泥土與石頭
                for (int y=0; y< posY; y++)                                                  //第一層底下 從高度 0 開始
                {
                    int rDirty = (int)Random.Range(v2Dirty.x, v2Dirty.y);                   //隨機泥土

                    if (y >= posY- rDirty)                                                 //第一層下方先顯示泥土在顯示石頭
                    {
                        Vector3 posDirty = new Vector3(posX, y * heightTerrain, posZ);
                        Instantiate(objTerrains[1], posDirty, Quaternion.identity, traTerrainGroup);
                    }
                    else
                    {
                        Vector3 posStone = new Vector3(posX, y * heightTerrain, posZ);
                        Instantiate(objTerrains[2], posStone, Quaternion.identity, traTerrainGroup);
                    }
                }
            }

        }
        #endregion
    }
    private void GeneratePlayer()
    {
        Vector3 pos = new Vector3(x / 2, height + 3, z / 2);
        Instantiate(traPlayer, pos, Quaternion.identity);
    }    
       

    #endregion
}
