///
/// 高木へ、コメント書きましょう
///
/// code by TKG and ogata 


using UnityEngine;
using System.Collections;

public class ClientEnamyController : MonoBehaviour
{
    EnemyAI EnamyAI = null;

    [SerializeField]
    GameObject EnamyGraph = null;

    void Start()
    {
        if (ConnectionManager.IsWacth) Destroy(this);

        EnamyGraph.GetComponent<SpriteRenderer>().enabled = false;
        EnamyAI = gameObject.GetComponent<EnemyAI>();

    }


    void Update()
    {
        
        //現在のステートを確認し、処理
        switch(EnamyAI.GetNowState())
        {
            default:
                break;
            case EnemyData.EnamyState.SPAWN:    // 出現
                EnamyGraph.GetComponent<ClientEnemyOperator>().Spawn();
                break;
        }

    }
}
