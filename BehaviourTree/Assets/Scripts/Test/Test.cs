using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Test : MonoBehaviour {

	INode bt;

	// Use this for initialization
	void Start () {
		bt = new Builder()
				.Sequence("seq")
					.Do("act1", t =>{
						return Status.Success;
					})
					.Do("act2", t => {
						return Status.Success;
					})
				.End()
			.Build();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(bt.Tick(Time.deltaTime));
	}
}
