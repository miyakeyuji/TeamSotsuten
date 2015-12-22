///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// エフェクトのデータベース
/// 
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EffectDB : MonoBehaviour {

    /// <summary>
    /// エフェクトのデータベース用構造体
    /// </summary>
    public struct EffectData
    {
        /// <summary>
        /// 攻撃タイプ
        /// </summary>
        public MotionManager.MotionSkillType skillType;

        /// <summary>
        /// 攻撃の速度
        /// </summary>
        public float speed;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="_type">攻撃タイプ</param>
        /// <param name="_speed">速度</param>
        public EffectData(
            MotionManager.MotionSkillType _type , 
            float _speed
            )
        {
            skillType = _type;
            speed = _speed;
        }
    }

    /// <summary>
    /// 各エフェクトの細かいデータ一覧
    /// 順番は攻撃タイプの列挙型に準拠
    /// 列挙側では0番目がNONEのため協力技の分として仮定
    /// </summary>
    private float[] speedArray = new float[] {0.0f,5.1f, 5.2f, 5.3f, 5.4f };

    /// <summary>
    /// ゲーム中に参照するエフェクトデータのリスト
    /// </summary>
    public List<EffectData> dataList = new List<EffectData>() { };

	// Use this for initialization
	void Start ()
    {
        ///<summary>
        /// データをリストへ入れる 
        ///</summary>
        foreach (MotionManager.MotionSkillType motionType in Enum.GetValues(typeof(MotionManager.MotionSkillType)))
        {
            EffectData data = new EffectData(
                motionType,
                speedArray[(int)motionType]);
            dataList.Add(data);
        }
	}
}
