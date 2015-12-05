using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnamySpawner : MonoBehaviour 
{
    enum SpawnerState
    {
        Standby,
        Update,

    }
    SpawnerState State = SpawnerState.Standby;

    [SerializeField]
    List<EnamyData> EnemyList = new List<EnamyData>();

    bool isOwner = false;
    int enemyMaxId;


	// Use this for initialization
	void Start () 
    {
       if (ConnectionManager.IsOwner)
       {
           enemyMaxId = GameManager.Instance.MaxPlayerNum;
           isOwner = true;


           State = SpawnerState.Update;
       }
	}
	
    void Spawn()
    {
        // サバ―にエネミーを登録をさせます
        foreach (var _Enemies in EnemyList)
        {
            _Enemies.SetMyData();
            Debug.Log(_Enemies+"が登録されました");
        }

    }

	// Update is called once per frame
	void Update () 
    {
        if (isOwner)
        {
            if (State == SpawnerState.Update)
            {
                Spawn();

                State = SpawnerState.Standby;
            }

        }	    
	}
}
