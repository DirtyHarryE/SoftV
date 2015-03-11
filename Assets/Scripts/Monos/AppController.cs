﻿using UnityEngine;
using System.Collections;

public class AppController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameController.Instance.Init();
	}
	
	// Update is called once per frame
    void Update()
    {
        GameController.Instance.Update();
	}
}
