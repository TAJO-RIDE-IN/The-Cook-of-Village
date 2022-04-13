using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour {
	public Transform rotationObject;
	public Vector3 rotateDirection = Vector3.forward;
	public float rotationSpeed = 1;
	public bool pinpong = false;
	public float oscillationAngle = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {
		if (rotationObject) {
			if (!pinpong) {
				rotationObject.transform.Rotate (rotateDirection.normalized * rotationSpeed * Time.deltaTime);
			} else {
				rotationObject.transform.localEulerAngles = rotateDirection * Mathf.PingPong (Time.time * rotationSpeed, oscillationAngle);
			}
		} else {
			return;
		}
	}
}
