using UnityEngine;
using System.Collections;

public class EnemyData: MonoBehaviour 
{

    [SerializeField]
    int id;      //ID

    [SerializeField]
    int life;      //体力

    [SerializeField]
    Vector3 Position;    // 座標

    [SerializeField]
    Vector3 Rotation;    //方向


    public int Life
    {
        get { return life; }
        set { if (life == -1) Life = value; }
    }

    public int Id
    {
        get { return id; }
        set { if (life == -1) id = value; }
    }


    enum STATE
    {
        NONE,
        STAY,
        ACTIVE,
        DEAD,
    }

    [SerializeField]
    STATE State = STATE.NONE;

    public void SetMyData()
    {
        GameManager.Instance.SendEnemyHP(0, life);
        GameManager.Instance.SendEnemyPosition(0, Position);
        GameManager.Instance.SendEnemyRotation(0, Rotation);
        GameManager.Instance.SendEnemyIsActive(id, true);
    }
}
