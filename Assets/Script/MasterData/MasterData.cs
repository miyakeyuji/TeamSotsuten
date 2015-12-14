//-------------------------------------------------------------
//  マスターデータ クラス
// 
//  code by m_yamada
//  and ogata
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;


/// <summary>
/// プレイヤーマスターデータ(一人一人)
/// </summary>
public class PlayerMasterData 
{
    public Vector3 Position; //座標

    public JobDB.JobType Job;   //職業
}


/// <summary>
/// エネミーマスターデータ(１体１体)
/// </summary>
public class EnemyMasterData
{
    public int HP;      //体力

    public Vector3 Position;    // 座標

    public Vector3 Rotation;    //方向

    public bool IsActive; // 有効化フラグ

    public EnemyData.EnamyState State;  // 状態

    public MotionManager.MotionSkillType HitAttackType; // どの攻撃が当たったか
}


/// <summary>
/// プレイヤーの攻撃用データ
/// </summary>
public class PlayerAttackMasterData
{
    //有効フラグ
    public bool IsActive;

    //Hit時にtrueになる変数
    public bool IsHit;

    //攻撃のモーション種類
    public MotionManager.MotionSkillType AttackType;

    //攻撃のエフェクト種類
    //public int EffectType;

    // 攻撃対象
    public int EnemyTargetIndex;
    
}



/// <summary>
/// エネミー攻撃のマスターデータ
/// </summary>
public class EnemyAttackMasterData
{
    public bool IsActive; //有効フラグ

    //攻撃の種類の列挙型
    public enum EnemyAttackEffectType
    {
        NONE,
        ATTACK1,
    };

    // 攻撃対象
    public int PlayerTargetIndex;

}