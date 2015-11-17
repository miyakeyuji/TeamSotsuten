//-------------------------------------------------------------
//  シーケンスのマネージャークラス
//  各オブジェクトの処理をここに記述する。
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceManager : Singleton<SequenceManager> 
{
    [SerializeField]
    List<ISequenceBehaviour> behaviorList = new List<ISequenceBehaviour>();

    public override void Awake() 
    {
        base.Awake();

        for (int i = 0; i < behaviorList.Count; i++)
        {
            behaviorList[i].Init();
        }
    }

    public override void Start() 
    {
        base.Start();

	}

    public override void Update()
    {
        base.Update();

    }
}
