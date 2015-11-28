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
    public int ID;

}


/// <summary>
/// エネミーマスターデータ(１体１体)
/// </summary>
public class EnemyMasterData
{
    //public bool IsHit; // ヒットフラグ

    public int ID;      //各エネミー判定用のID

    public int HP;      //体力

    public Vector3 Position;    // 座標

    public Vector3 Rotation;    //方向

    public bool IsLife; // 生存フラグ

    
}



/// <summary>
/// 攻撃のマスターデータ
/// </summary>
public class AttackMasterData
{
    public int ID;  //認識用ID

    public bool IsLife; //生存（有効）フラグ
}