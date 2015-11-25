using UnityEngine;
using System.Collections;

public class ClientEnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject clientEnemy;

	// Use this for initialization
	void Start () {
        ObjectPool.Instance.SetObject(clientEnemy, 5);   
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
