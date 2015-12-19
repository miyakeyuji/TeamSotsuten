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

    bool isLive = false;

    //void Update(){}

    /// <summary>
    /// 発生
    /// </summary>
    public void Spawn()
    {
        isLive = true;

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
        isLive = false;

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
    /// 衝突時の処理
    /// </summary>
    public void Hit()
    {

    }

    /// <summary>
    /// アクティブ状態を変更する
    /// </summary>
    void ChangeActive()
    {
        this.gameObject.SetActive(isLive);
    }
}
