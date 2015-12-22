using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EffectDB : MonoBehaviour {

    public struct EffectData
    {
        public MotionManager.MotionSkillType skillType;
        public float speed;

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
        foreach (MotionManager.MotionSkillType motionType in Enum.GetValues(typeof(MotionManager.MotionSkillType)))
        {
            EffectData data = new EffectData(
                motionType,
                speedArray[(int)motionType]);
            dataList.Add(data);
        }
	}
}
