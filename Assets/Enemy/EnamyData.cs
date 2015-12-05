using UnityEngine;
using System.Collections;

public class EnamyData: MonoBehaviour 
{
    [SerializeField]
    int id;      //ID

    [SerializeField]
    int HP;      //体力

    [SerializeField]
    Vector3 Position;    // 座標

    [SerializeField]
    Vector3 Rotation;    //方向

    public void SetMyData()
    {
        GameManager.Instance.SendEnemyHP(0, HP);
        GameManager.Instance.SendEnemyPosition(0, Position);
        GameManager.Instance.SendEnemyRotation(0, Rotation);
        GameManager.Instance.SendEnemyIsLife(id, true);
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
