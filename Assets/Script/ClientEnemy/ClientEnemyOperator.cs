// code by Gai Takakura
using UnityEngine;
using System.Collections;

// サーバーとの通信を行う
public class ClientEnemyOperator : MonoBehaviour {

    public int Id { get; set; }
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
    }
}
