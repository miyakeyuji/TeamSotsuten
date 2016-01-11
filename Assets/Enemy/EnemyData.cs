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
    }

    public int Id
    {
        get { return id; }
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

    [SerializeField]
    Sprite sprite = null;

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
        GameManager.Instance.SendEnemyHP(id, life);
        GameManager.Instance.SendEnemyPosition(id, transform.position);
        GameManager.Instance.SendEnemyRotation(id, transform.rotation.eulerAngles);
        GameManager.Instance.SendEnemyIsActive(id, true);

        EnemyManager.Instance.SetEnemySprite(sprite);
    }


    public void UpdateData()
    {
        life = GameManager.Instance.GetEnemyData(id).HP;
    }

}
