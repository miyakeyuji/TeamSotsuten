﻿using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

    public ParticleSystem particle = null;

	// Use this for initialization
	//void Start () {}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            ParticleEmit();
        }
        if (Input.GetMouseButton(1))
        {
            ParticlePause();
        }
    }

    
    /// <summary>
    /// パーティクルを生成する
    /// </summary>
    void ParticleEmit()
    {
        // 時間を初期状態に設定
        particle.time = 0f;

        // 再生中かチェック
        if(particle.isStopped || particle.isPaused)
        {
            particle.Play();
        }
    }

    /// <summary>
    /// パーティクルをプレイ状態にする
    /// </summary>
    void ParticlePlay()
    {
        particle.Play();
    }

    /// <summary>
    /// パーティクルを停止状態にする
    /// </summary>
    void ParticleStop()
    {
        particle.Stop();
    }

    /// <summary>
    /// パーティクルを一時停止する
    /// </summary>
    void ParticlePause()
    {
        particle.Pause();
    }
}
