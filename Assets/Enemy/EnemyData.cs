///
/// 高木へ、コメント書きましょう
///
/// code by TKG and ogata 


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
        private set { if (life == -1) Life = value; }
    }

    public int Id
    {
        get { return id; }
        private set { if (id == -1) id = value; }
    }

    public enum EnamyState
    {
        NONE,
        SPAWN,
        STAY,
        ACTIVE,
        HIT,
        DEAD,
    }

    [SerializeField]
    EnamyState state = EnamyState.NONE;
    public EnamyState State{  get { return state; }}

    public void StateChange(EnamyState _statNnum) { state = _statNnum; }

    public bool IsActive() { return GameManager.Instance.GetEnemyData(id).IsActive; }

    public bool IsHit() { return GameManager.Instance.GetEnemyData(id).IsHit; }

    /// <summary>
    /// ヒットフラグを解除する。
    /// </summary>
    public void HitRelease()
    {
        GameManager.Instance.GetEnemyData(id).IsHit = false;
    }

    //GameManager用のエネミーデータ
    public void SetMyDate()
    {
        GameManager.Instance.SendEnemyHP(0, life);
        GameManager.Instance.SendEnemyPosition(0, Position);
        GameManager.Instance.SendEnemyRotation(0, Rotation);
        GameManager.Instance.SendEnemyIsActive(id, true);

    }


    /// <summary>
    /// GameManager側で保持しているHPに更新
    /// </summary>
    public void LifeUpDate()
    {
        Life = GameManager.Instance.GetEnemyData(id).HP;
    }

}
