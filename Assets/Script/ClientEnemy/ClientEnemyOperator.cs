//-------------------------------------------------------------
// データのやり取りを管理するクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;

// サーバーとの通信を行う
public class ClientEnemyOperator : MonoBehaviour {

    private int id;
    public int Id { get { return id; } set { id = value; } }
    EnemyMasterData data = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // ポジションなどの更新
    void UpdateDatas()
    {
        data = GameManager.Instance.GetEnemyData(Id);
        DataSet();
    }

    // ポジションなどの更新
    void DataSet()
    {
        this.gameObject.transform.position = data.Position;                     // ポジション
        this.gameObject.transform.rotation = Quaternion.Euler(data.Rotation);   // 角度
    }
}
