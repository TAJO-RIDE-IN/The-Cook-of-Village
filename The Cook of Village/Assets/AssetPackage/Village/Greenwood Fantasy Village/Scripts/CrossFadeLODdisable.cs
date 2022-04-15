
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeLODdisable : MonoBehaviour {
	#if UNITY_ANDROID
	// Use this for initialization
	void Start () {
			DisableCrossFade();


	}
	void DisableCrossFade(){
		gameObject.GetComponent<LODGroup> ().fadeMode = LODFadeMode.None;
	}
	#endif
}

