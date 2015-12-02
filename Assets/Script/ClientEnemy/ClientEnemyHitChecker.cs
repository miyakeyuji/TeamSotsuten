//-------------------------------------------------------------
// 衝突のチェックを行うクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;

// 衝突判定を行うクラス
public class ClientEnemyHitChecker : MonoBehaviour {
    ClientEnemyOperator　enemyOperator = null;

    void Start()
    {
        enemyOperator = this.gameObject.GetComponent<ClientEnemyOperator>();
    }

    // 衝突判定
    void OnTriggerEnter(Collider other)
    {
        int ID = 0;

        var data = GameManager.Instance.GetEnemyData(ID);

        if(data.HP <= 0)
        {
            //GameManager.Instance.SendEnemyDeath(ID);
        }
    }
}
