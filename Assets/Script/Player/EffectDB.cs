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
        /// エフェクトのサイズ
        /// </summary>
        public Vector3 scale;
        
        /// <summary>
        /// ダメージ量
        /// </summary>
        public int damage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type">攻撃タイプ</param>
        /// <param name="_speed">速度</param>
        /// <param name="_scale">サイズ</param>
        /// <param name="_damage">ダメージ</param>
        public EffectData(
            MotionManager.MotionSkillType _type , 
            float _speed,
            Vector3 _scale,
            int _damage
            )
        {
            skillType = _type;
            speed = _speed;
            scale = _scale;
            damage = _damage;
        }
    }

    /// <summary>
    /// 各エフェクトの細かいデータ一覧
    /// 順番は攻撃タイプの列挙型に準拠
    /// 列挙側では0番目がNONEのため協力技の分として仮定
    /// </summary>
    private float[] speedArray = new float[] 
    {
        0.5f,1.5f,1.0f, 0.2f, 0.3f, 0.4f
    };

    /// <summary>
    /// エフェクトのサイズ配列
    /// </summary>
    private Vector3[] scaleArray = new Vector3[] 
    {
        new Vector3(20.0f,20.0f,20.0f),
        new Vector3(10.0f,10.0f,10.0f),
        new Vector3(10.0f,10.0f,10.0f)
    };

    /// <summary>
    /// ダメージ配列
    /// </summary>
    private int[] damageArray = new int[] { 50, 10 , 15 };

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
                speedArray[(int)motionType],
                scaleArray[(int)motionType],
                damageArray[(int)motionType]);
            dataList.Add(data);
        }
	}
}
