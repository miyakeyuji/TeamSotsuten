//-------------------------------------------------------------
// サーバーから送られたデータを処理するクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CliantEnemySpawner : MonoBehaviour {
    // 生成したオブジェクトの親に設定するゲームオブジェクト
    [SerializeField]
    GameObject parentObject = null;

    [SerializeField]
    GameObject enemy = null;

    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        // 子オブジェクトを全部取得する
        foreach (Transform child in transform)
        {
            enemies.Add(child.gameObject);
        }
    }

    /// <summary>
    /// エネミーを生成する
    /// </summary>
    /// <param name="_id">設定するID(配列のindex)</param>
    void SpawnEnemy(int _id)
    {
        var data = GameManager.Instance.GetEnemyData(_id);
        var createdEnemy = (GameObject)Instantiate(enemy, data.Position, Quaternion.Euler(data.Rotation));
        createdEnemy.GetComponent<ClientEnemyOperator>().ID = _id;
        if(parentObject != null) createdEnemy.transform.parent = parentObject.transform;   
    }

    /// <summary>
    /// 発生
    /// </summary>
    public void Spawn(int num)
    {
        if (enemies.Count < num)
        {
            Debug.Log("要素外です");
            return;
        }

        var hash = new Hashtable();
        {
            hash.Add("scale", new Vector3(1f, 1f, 1f)); // 設定するサイズ
            hash.Add("time", 1f);                       // 1秒で行う
            hash.Add("easetype", "easeOutQuad");        // イージングタイプを設定
            hash.Add("onstart", "ChangeActive");        // 初めにメソッドを呼ぶ
        }
        iTween.ScaleTo(enemies[num], hash);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead(int num)
    {
        if (enemies.Count < num)
        {
            Debug.Log("要素外です");
            return;
        }

        var hash = new Hashtable();
        {
            hash.Add("scale", new Vector3(0f, 0f, 0f)); // 設定するサイズ
            hash.Add("time", 1f);                       // 1秒で行う
            hash.Add("easetype", "easeOutQuad");        // イージングタイプを設定
            hash.Add("oncomplete", "ChangeActive");     // 最後にメソッドを呼ぶ
        }
        iTween.ScaleTo(enemies[num], hash);
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
