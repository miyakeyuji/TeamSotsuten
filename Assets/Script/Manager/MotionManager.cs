//-------------------------------------------------------------
//  モーション管理クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class MotionManager : Singleton<MotionManager>
{
    /// <summary>
    /// モーションスキルタイプ
    /// </summary>
    public enum MotionSkillType
    {
        NONE,                   //< 未定義
        VERTICAL_UP_DOWN,       //< 上から下 縦振り
        VERTICAL_DOWN_UP,       //< 下から上 縦振り
        HORIZONTAL_RIGHT_LEFT,  //< 右から左 横振り
        HORIZONTAL_LEFT_RIGHT,  //< 左から右 横振り 
    };

    /// <summary>
    /// 今のモーションスキルタイプ
    /// </summary>
    public MotionSkillType MotionSkill { get; private set; }

    /// <summary>
    /// 計算するモーションの種類、
    /// </summary>
    MotionSkillType calcMotionSkill = MotionSkillType.NONE;

    [System.Serializable]
    public class MotionWatchData
    {
        public bool isCalcX = false;
        public bool isCalcY = false;
        public bool isCalcZ = false;
        public MotionSkillType skillType = MotionSkillType.NONE;
        public Vector3[] acc = new Vector3[2];
    }

    /// <summary>
    /// 加速度の誤差を許す範囲
    /// </summary>
    [SerializeField]
    float accRange = 0.1f;

    [SerializeField]
    MotionWatchData[] motionData = new MotionWatchData[2];

    [SerializeField]
    AttackSkillCreator attackSkill = null;

    [SerializeField]
    JobRotater jobRotater = null;

    bool isStart = false;
    int clearIndex = 0;

    public override void Awake()
    {
        base.Awake();

        MotionSkill = MotionSkillType.NONE;
    }

    public override void Start()
    {
        base.Start();


    }

    /// <summary>
    /// 加速度量を取得
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    Vector3 GetAccValue(MotionWatchData data,int index)
    {
        float x = data.isCalcX ? data.acc[index].x : WatchManager.Instance.Acc.x;
        float y = data.isCalcY ? data.acc[index].y : WatchManager.Instance.Acc.y;
        float z = data.isCalcZ ? data.acc[index].z : WatchManager.Instance.Acc.z;

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// モーションを開始する
    /// </summary>
    /// <param name="data"></param>
    void StartMotion()
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            float dist = Vector3.Distance(GetAccValue(motionData[i],0), WatchManager.Instance.Acc);
            if (dist >= accRange)
            {
                calcMotionSkill = motionData[i].skillType;
                isStart = true;
                clearIndex = 1;
            }
        }
    }

    /// <summary>
    /// モーションを分析
    /// </summary>
    /// <param name="data"></param>
    void CalcMotion()
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            if (calcMotionSkill != motionData[i].skillType) continue;

            float dist = Vector3.Distance(GetAccValue(motionData[i], clearIndex), WatchManager.Instance.Acc);
            if (dist <= accRange)
            {
                clearIndex++;
            }

            if (motionData[i].acc.Length <= clearIndex)
            {
                MotionSkill = motionData[i].skillType;
                calcMotionSkill = MotionSkillType.NONE;
                isStart = false;
            }
        }
    }


    /// <summary>
    /// モーションが成功した時に呼ばれる。
    /// </summary>
    void OnComplated()
    {
        if (isStart) return;
        if (MotionSkill == MotionSkillType.NONE) return;

        attackSkill.OnMotionComplated();
        jobRotater.OnMotionComplated();

    }

    public override void Update()
    {
        base.Update();

        if (!isStart)
        {
            StartMotion();
        }
        else
        {
            CalcMotion();

            OnComplated();
        }
    }
}
