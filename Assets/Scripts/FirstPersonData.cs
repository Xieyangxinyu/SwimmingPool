using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonData : MonoBehaviour {

	//m_Camera is the camera inside FirstPersonCharacter
	//m_CharacterController is the CharacterController(in Physics) in FPSController
	private Camera m_Camera;

	// Use this for initialization
	private void Start () {
		//Initial Set-up
		m_Camera = Camera.main;
	}
	
	// Get Camera position and Rotation as you like it
	void Update () {
		
	}
}
