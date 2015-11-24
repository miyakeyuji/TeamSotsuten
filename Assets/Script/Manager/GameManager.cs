//-------------------------------------------------------------
//  ゲーム管理クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GameManager : Singleton<CharacterSelectManager>
{


    public override void Awake()
    {
        base.Awake();

    }

    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        base.Update();

    }

    public EnemyMasterData EnemyMansterData { get; private set; }
    public PlayerMasterData PlayerMansterData { get; private set; }

    /// <summary>
    /// 敵がヒットしたら、この関数をよんでください。
    /// </summary>
    /// <param name="isHit"></param>
    public void EnemyHit(bool isHit)
    {
        EnemyMansterData.IsHit = isHit;
    }
}
