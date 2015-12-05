//-------------------------------------------------------------
// データのやり取りを管理するクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;

// サーバーとの通信を行う
public class ClientEnemyOperator : MonoBehaviour {
    /// <summary>
    /// エネミーのID
    /// </summary>
    private int id = 0;
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            if (id == 0) id = value;
        }
    }
    EnemyMasterData data = null;

    /// <summary>
    /// ポジションなどの更新
    /// </summary>
    void UpdateDatas()
    {
        data = GameManager.Instance.GetEnemyData(ID);
    }

    /// <summary>
    /// ポジションなどの更新
    /// </summary>
    void DataSet()
    {
        this.gameObject.transform.position = data.Position;                     // ポジション
        this.gameObject.transform.rotation = Quaternion.Euler(data.Rotation);   // 角度
    }


    /// <summary>
    /// 衝突判定
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (!data.IsLife || data.HP <= 0)
        {
            //GameManager.Instance.SendEnemyDeath(ID);
        }
    }
}
