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
        if (EnamyAI.NowStateMatcingc(EnemyData.EnamyState.SPAWN))
        {
            EnamyGraph.GetComponent<ClientEnemyOperator>().Spawn();
        }

    }
}
