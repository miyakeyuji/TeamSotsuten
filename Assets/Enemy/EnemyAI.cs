using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{

    EnemyData data = null;

    bool isOwner = false;

	void Start () 
    {
        if (ConnectionManager.IsOwner)
        {
            isOwner = true;
        }

        data.GetComponent<EnemyData>();
        
    }


    //サーバーのエネミーに死んだことをお知らせする
    void IsDead()
    {
        GameManager.Instance.SendEnemyIsActive(data.Id, false);
    }

	// Update is called once per frame
	void Update () 
   {
        if (!isOwner) return;

       //死ぬ判定
       if (data.Life <= 0) IsDead();
        //他の判定追記

	}

}
