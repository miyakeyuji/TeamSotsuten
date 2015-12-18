using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour 
{
    enum SpawnerState
    {
        None,
        Standby,
        Start,
        Update,

    }
    SpawnerState State = SpawnerState.Standby;

    [SerializeField]
    List<GameObject> EnemyList = new List<GameObject>();


    int enemyMaxId;


	// Use this for initialization
	void Start () 
    {
       if (ConnectionManager.IsOwner)
       {
           State = SpawnerState.Start;
       }
       else State = SpawnerState.None;
	}

    void Spawn()
    {
        //EnemyList[0].GetComponent<EnemyData>().SetMyDate();
        // サバ―にエネミーを登録,初期化を行う
        foreach (var _Enemies in EnemyList)
        {
            _Enemies.GetComponent<EnemyData>().SetMyDate();
        }

        Debug.Log("登録終了");
    }



	// Update is called once per frame
	void Update () 
    {
        if (State == SpawnerState.None) return;

        if (State == SpawnerState.Start)
        {
            Spawn();

            State = SpawnerState.Standby;
        }


	}
}
