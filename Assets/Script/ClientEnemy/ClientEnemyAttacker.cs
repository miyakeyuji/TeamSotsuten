﻿using UnityEngine;
using System.Collections;

public class ClientEnemyAttacker : MonoBehaviour {

    [SerializeField]
    private GameObject attackObj = null;

	// Use this for initialization
	void Start () {
        ObjectPool.Instance.SetObject(attackObj, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
