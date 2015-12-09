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
    /// 生成する攻撃
    /// </summary>
    [SerializeField]
    GameObject prefav = null;
    GameObject createdAttack = null;

    /// <summary>
    /// エネミーのID
    /// </summary>
    private int id = -1;
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            if (id == -1) id = value;
        }
    }
    EnemyMasterData data = null;

    void Update()
    {
        // 弾瀬性
        if(createdAttack == null)
        {
            //createdAttack = (GameObject)Instantiate(prefav, this.transform.position, Quaternion.identity);
            //createdAttack.GetComponent<ClientEnemyAttack>().ID = ...;
        }
    }

    void FixedUpdate()
    {
        UpdateDatas();
        DataSet();
    }

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
        if (!data.IsActive || data.HP <= 0)
        {
            //GameManager.Instance.SendEnemyDeath(ID);
        }
    }
}
