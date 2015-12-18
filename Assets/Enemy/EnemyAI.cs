using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
    EnemyData Enemy = null;

    bool DodSpawn = false;

    public bool NowStateMatcingc(EnemyData.EnamyState _statNnum)
    {
        return (Enemy.State == _statNnum);
    }


    void Start()
    {
        Enemy = gameObject.GetComponent<EnemyData>();
    }

    //エネミーデータのStateを変更します。
    void StateChanger()
    {
        if (Enemy.IsActive() && !DodSpawn) 
        {
            Enemy.StateChange(EnemyData.EnamyState.SPAWN);
            DodSpawn = true;
        }
        else if (!Enemy.IsActive() && DodSpawn)
        {
            Enemy.StateChange(EnemyData.EnamyState.DEAD);
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
