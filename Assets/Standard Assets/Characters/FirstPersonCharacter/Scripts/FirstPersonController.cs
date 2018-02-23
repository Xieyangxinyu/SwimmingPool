using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.XR;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[RequireComponent(typeof (CharacterController))]
	public class FirstPersonController : MonoBehaviour
	{
		[SerializeField] private MouseLook m_MouseLook;

		//m_Camera is the camera inside FirstPersonCharacter
		//m_MoveDir is a vector<x,y,z> indicating the first person's (moving direction * moving speed)
		//m_CharacterController is the CharacterController(in Physics) in FPSController
		private Camera m_Camera;
		private Vector3 m_MoveDir = Vector3.zero;
		private CharacterController m_CharacterController;
		private float m_StepCycle;
		//Bx,By and Bz are boundary-control-variables
		public float Bx;
		public float By;
		public float Bz;

		//FirstPerson moving spead
		public float speed;

		//countText is the GUI element shows the number of bubbles caught
		//count GameObject records the number of bubbles caught in its Position x
		public Text countText;
		public int count;

		// Use this for initialization
		private void Start()
		{
			//Initial Set-up
			m_CharacterController = GetComponent<CharacterController>();
			m_Camera = Camera.main;
			m_MouseLook.Init(transform , m_Camera.transform);
			count = 0;
			SetCountText ();
		}


		// Update is called once per frame
		private void Update()
		{
			Vector3 moveDirection = m_Camera.transform.forward;
			RotateView();
		}

		private void FixedUpdate()
		{
			//moveDirection records the forward direct of the m_Camera
			//presentPosition records the current position of the m_Camera and is used to check if the avatar is out of the boundary
			Vector3 moveDirection = m_Camera.transform.forward;
			Vector3 presentPosition = m_Camera.transform.position;

			//Boundaries of limited swimming spaces
			if (presentPosition.y > By || presentPosition.y < 1 || presentPosition.x > Bx || presentPosition.x < 0 || presentPosition.z > Bz || presentPosition.z < 0) {
				countText.text = "Boundary Warning";
				if (presentPosition.y > By && moveDirection.y > 0)
					moveDirection.y = 0f; 
				if (presentPosition.y < 1 && moveDirection.y < 0)
					moveDirection.y = 0f;
				if (presentPosition.x > Bx && moveDirection.x > 0) 
					moveDirection.x = 0f;
				if (presentPosition.x < 0 && moveDirection.x < 0)
					moveDirection.x = 0f;
				if (presentPosition.z > Bz && moveDirection.z > 0)
					moveDirection.z = 0f;
				if (presentPosition.z < 0 && moveDirection.z < 0)
					moveDirection.z = 0f;
			}else {
				SetCountText ();
			}

			//Derection setup
			m_MoveDir.x = moveDirection.x*speed;
			m_MoveDir.z = moveDirection.z*speed;
			m_MoveDir.y = moveDirection.y*speed;

			//update Character Position
			m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

			//update Camera Position
			UpdateCameraPosition(speed);

			//Don't mind it if you are working on DataCollection or MapGeneration
			m_MouseLook.UpdateCursorLock();
		}


		private void UpdateCameraPosition(float speed)
		{
			Vector3 newCameraPosition;
			newCameraPosition = m_Camera.transform.localPosition;
			m_Camera.transform.localPosition = newCameraPosition;
		}

		//Rotate View by reading inputs from VR headset or Trackpad
		private void RotateView()
		{
			m_MouseLook.LookRotation (transform, m_Camera.transform);
		}

		//Update countText GUI element
		private void SetCountText ()
		{
			countText.text = "Count: " + count.ToString ();
		}
	}
}
