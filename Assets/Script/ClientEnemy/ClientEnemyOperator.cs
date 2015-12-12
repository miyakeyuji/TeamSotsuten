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
    [SerializeField]
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
    /*void OnTriggerEnter(Collider other)
    {
        if (!data.IsActive || data.HP <= 0)
        {
            //GameManager.Instance.SendEnemyDeath(ID);
        }
    }//*/

    /// <summary>
    /// 発生
    /// </summary>
    public void Spawn()
    {
        var hash = new Hashtable();
        {
            hash.Add("scale", new Vector3(1f, 1f, 1f)); // 設定するサイズ
            hash.Add("time", 1f);                       // 1秒で行う
            hash.Add("easetype", "easeOutQuad");        // イージングタイプを設定
            hash.Add("onstart", "ChangeActive");        // 初めにメソッドを呼ぶ
        }
        iTween.ScaleTo (this.gameObject, hash);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        var hash = new Hashtable();
        {
            hash.Add("scale", new Vector3(0f, 0f, 0f)); // 設定するサイズ
            hash.Add("time", 1f);                       // 1秒で行う
            hash.Add("easetype", "easeOutQuad");        // イージングタイプを設定
            hash.Add("oncomplete", "ChangeActive");     // 最後にメソッドを呼ぶ
        }
        iTween.ScaleTo(this.gameObject, hash);
    }

    /// <summary>
    /// アクティブ状態を変更する
    /// </summary>
    void ChangeActive()
    {
        if (!this.gameObject.activeInHierarchy)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
