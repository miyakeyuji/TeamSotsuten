//-------------------------------------------------------------
// 衝突のチェックを行うクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;

// 衝突判定を行うクラス
public class ClientEnemyHitChecker : MonoBehaviour {
    ClientEnemyOperator enemyOperator = null;

    void Start()
    {
        enemyOperator = GetComponent<ClientEnemyOperator>();
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        var data = GameManager.Instance.GetEnemyData(enemyOperator.ID);

        if(data.HP <= 0)
        {
            //GameManager.Instance.SendEnemyDeath(ID);
        }
    }
}
