///
/// 高木へ、コメント書きましょう
///
/// code by TKG and ogata 

using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
    EnemyData Enemy = null;

    // 湧くことが出来ます。
    bool CanSpawn = true;


    /// <summary>
    /// 現在のステートを返します
    /// </summary>
    /// <returns></returns>
    public EnemyData.EnamyState GetNowState()
    {
        return Enemy.State;
    }

    void Start()
    {
        Enemy = gameObject.GetComponent<EnemyData>();
    }

    //エネミーデータのStateを変更します。
    void StateChanger()
    {
        if (Enemy.IsActive() && CanSpawn) 
        {
            Enemy.StateChange(EnemyData.EnamyState.SPAWN);
            CanSpawn = false;
        }
        else if (!Enemy.IsActive() && !CanSpawn)
        {
            Enemy.StateChange(EnemyData.EnamyState.DEAD);
        }
        else if(Enemy.IsActive() && Enemy.IsHit())
        {//攻撃がヒットした場合
            Enemy.StateChange(EnemyData.EnamyState.HIT);
        }
        else if(Enemy.IsActive())
        {

        }
        else 
        {
            Enemy.StateChange(EnemyData.EnamyState.NONE); 
        }
        
    }


    void Update()
    {
        //Stateを変更します
        StateChanger();

        //HPの更新
        Enemy.LifeUpDate(); 

    }
}
