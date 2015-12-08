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

    /// <summary>
    /// 計算時間
    /// </summary>
    [SerializeField]
    float calcTime = 1.0f;

    [SerializeField]
    MotionWatchData[] motionData = new MotionWatchData[2];

    [SerializeField]
    AttackSkillCreator attackSkill = null;

    [SerializeField]
    JobRotater jobRotater = null;

    bool isStart = false;
    int clearIndex = 0;
    float stopValue = 0;

    public override void Awake()
    {
        base.Awake();

        MotionSkill = MotionSkillType.NONE;

    }

    public override void Start()
    {
        base.Start();

        if (!ConnectionManager.IsSmartPhone)
        {
            enabled = false;
        }
    }

    /// <summary>
    /// 加速度量を取得
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    bool IsMotion(MotionWatchData data,int index)
    {
        float x = data.isCalcX ? data.acc[index].x : WatchManager.Instance.Acc.x;
        float y = data.isCalcY ? data.acc[index].y : WatchManager.Instance.Acc.y;
        float z = data.isCalcZ ? data.acc[index].z : WatchManager.Instance.Acc.z;

        bool isRangeX = Mathf.Abs(x - WatchManager.Instance.Acc.x) > accRange;
        bool isRangeY = Mathf.Abs(y - WatchManager.Instance.Acc.y) > accRange;
        bool isRangeZ = Mathf.Abs(z - WatchManager.Instance.Acc.z) > accRange;

        if (data.isCalcX && isRangeX) return false;
        if (data.isCalcY && isRangeY) return false;
        if (data.isCalcZ && isRangeZ) return false;

        return true;
    }


    /// <summary>
    /// モーションを開始する
    /// </summary>
    /// <param name="data"></param>
    void StartMotion()
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            if (!IsMotion(motionData[i], 0)) continue;

            calcMotionSkill = motionData[i].skillType;
            isStart = true;
            clearIndex = 1;
            stopValue = calcTime;

            Debugger.Log(">> StartMotion");
            Debugger.Log(calcMotionSkill.ToString());

            return;
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
            if (!IsMotion(motionData[i], clearIndex)) continue;

            clearIndex++;

            if (motionData[i].acc.Length <= clearIndex)
            {
                MotionSkill = motionData[i].skillType;
                calcMotionSkill = MotionSkillType.NONE;
                isStart = false;

                Debugger.Log("【モーション成功】");
                Debugger.Log(MotionSkill.ToString());

                return;
            }
        }
    }


    /// <summary>
    /// 時間内にモーション成功しなかったら、
    /// 計算を中止する
    /// </summary>
    /// <returns></returns>
    void StopCalc()
    {
        stopValue -= Time.deltaTime;
        if (stopValue <= 0)
        {
            MotionSkill = MotionSkillType.NONE;
            calcMotionSkill = MotionSkillType.NONE;
            isStart = false;

            Debugger.Log(">> StopCalc  失敗！！。");
        }
    }


    /// <summary>
    /// モーションが成功した時に呼ばれる。
    /// </summary>
    void OnComplated()
    {
        if (isStart) return;
        if (MotionSkill == MotionSkillType.NONE) return;

        Debugger.Log(">> OnComplated");
        Debugger.Log(MotionSkill.ToString());

        //attackSkill.OnMotionComplated();
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
            StopCalc();
            CalcMotion();
            OnComplated();
        }
    }
}
