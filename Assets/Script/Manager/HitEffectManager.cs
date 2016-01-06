// ---------------------------------------------------
//  ヒットエフェクトを管理するスクリプト
// 
//  code by m_yamada
// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitEffectManager : Singleton<HitEffectManager> {

    [SerializeField]
    GameObject weakHitEffect = null;         //< 弱いヒットエフェクト

    [SerializeField]
    GameObject strengthHitEffect = null;    //< 強いヒットエフェクト

    List<ParticleSystem> weakHitEffectList = new List<ParticleSystem>();
    List<ParticleSystem> strengthHitEffectList = new List<ParticleSystem>();
    int weakPlayIndex = 0;
    int strengthPlayIndex = 0;
    
    [SerializeField]
    int createNum = 5;

    PhotonView view = null;


    void Start()
    {
        var id = ConnectionManager.ID == 0 ? 0 : 1;
        view.RPC("SyncCreateEffect", ConnectionManager.GetSmartPhonePlayer(id));
    }

    [PunRPC]
    void SyncCreateEffect(PhotonMessageInfo info)
    {

        for (int i = 0; i < createNum; i++)
        {
            var obj = Instantiate(weakHitEffect);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            weakHitEffectList.Add(obj.GetComponent<ParticleSystem>());
        }

        for (int i = 0; i < createNum; i++)
        {
            var obj = Instantiate(strengthHitEffect);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            strengthHitEffectList.Add(obj.GetComponent<ParticleSystem>());
        }
    }

    /// <summary>
    /// 弱い方のヒットエフェクトを再生する
    /// </summary>
    /// <param name="position"></param>
    public void WeakHitEffectPlay(Vector3 position)
    {
        weakHitEffectList[weakPlayIndex].gameObject.SetActive(true);
        weakHitEffectList[weakPlayIndex].transform.position = position;
        weakHitEffectList[weakPlayIndex].Play();
        weakPlayIndex++;

        weakPlayIndex = weakHitEffectList.Count <= weakPlayIndex ? 0 : weakPlayIndex;
    }

    /// <summary>
    /// 強い方のヒットエフェクトを再生する
    /// </summary>
    /// <param name="position"></param>
    public void StrengthHitEffectPlay(Vector3 position)
    {
        strengthHitEffectList[strengthPlayIndex].gameObject.SetActive(true);
        strengthHitEffectList[strengthPlayIndex].transform.position = position;
        strengthHitEffectList[strengthPlayIndex].Play();
        strengthPlayIndex++;

        strengthPlayIndex = strengthHitEffectList.Count <= strengthPlayIndex ? 0 : strengthPlayIndex;
    }

    void Update()
    {
        Stop(strengthHitEffectList);
        Stop(weakHitEffectList);
    }

    void Stop(List<ParticleSystem> hitEffectList)
    {
        for (int i = 0; i < hitEffectList.Count; i++)
        {
            if (!hitEffectList[i].gameObject.activeInHierarchy) continue;

            if (!hitEffectList[i].isPlaying)
            {
                hitEffectList[i].gameObject.SetActive(false);
            }
        }
    }

}
