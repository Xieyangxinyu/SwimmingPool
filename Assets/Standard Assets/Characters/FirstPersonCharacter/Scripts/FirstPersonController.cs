using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[RequireComponent(typeof (CharacterController))]
	public class FirstPersonController : MonoBehaviour
	{
		[SerializeField] private MouseLook m_MouseLook;

		private Camera m_Camera;
		private Vector3 m_MoveDir = Vector3.zero;
		private CharacterController m_CharacterController;
		private float m_StepCycle;
		public float Bx;
		public float By;
		public float Bz;
		public float speed;
		public ParticleSystem hitEffect;

		public Text countText;
		private int count;

		// Use this for initialization
		private void Start()
		{
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

			m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

			UpdateCameraPosition(speed);

			m_MouseLook.UpdateCursorLock();
		}


		private void UpdateCameraPosition(float speed)
		{
			Vector3 newCameraPosition;
			newCameraPosition = m_Camera.transform.localPosition;
			m_Camera.transform.localPosition = newCameraPosition;
		}

		private void RotateView()
		{
			m_MouseLook.LookRotation (transform, m_Camera.transform);
		}


		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			//Getting a bubble
			if (hit.collider.tag == "Pick up")
			{
				count++;
				SetCountText ();
				Instantiate( hitEffect, hit.gameObject.transform.position,
					hitEffect.transform.rotation);
				hit.gameObject.SetActive(false);
			}
		}

		private void SetCountText ()
		{
			countText.text = "Count: " + count.ToString ();
		}
	}
}
