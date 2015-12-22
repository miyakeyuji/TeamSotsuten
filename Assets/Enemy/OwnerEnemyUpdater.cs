using UnityEngine;
using System.Collections;

public class OwnerEnemyUpdater: MonoBehaviour 
{
    EnemyAI AI = null;



    void Start()
    {
        if (ConnectionManager.IsWacth) Destroy(this);
        AI = gameObject.GetComponent<EnemyAI>();
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
