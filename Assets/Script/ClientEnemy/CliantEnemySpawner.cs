//-------------------------------------------------------------
// サーバーから送られたデータを処理するクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class CliantEnemySpawner : MonoBehaviour 
{

    // 生成したオブジェクトの親に設定するゲームオブジェクト
    [SerializeField]
    GameObject parentObject = null;

    [SerializeField]
    GameObject enemy = null;

    // Update is called once per frame
    void Update ()
    {
	    
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
}
