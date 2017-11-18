using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[Serializable]
	public class MouseLook
	{
		public float XSensitivity = 2;
		public float YSensitivity = 2;
		public bool clampVerticalRotation = true;
		public float MinimumX = -90F;
		public float MaximumX = 90F;
		public bool smooth;
		public float smoothTime = 5f;
		public bool lockCursor = true;
		public int Cycle = 200;
		public float mini = 0.5f;

		private Quaternion previousX;
		private Quaternion previousY;
		private Quaternion m_CameraTargetRot;
		private Quaternion m_CharacterTargetRot;
		private bool m_cursorIsLocked = true;
		private int prey = 0;
		private int asy = 0;
		private int cnty = 0;

		public void Init(Transform character, Transform camera)
		{
			m_CharacterTargetRot = character.localRotation;
			m_CameraTargetRot = camera.localRotation;
			previousX = Quaternion.Euler (0f, 0f, 0f);
		}

		public void LookRotation(Transform character, Transform camera)
		{
			float yRot = CrossPlatformInputManager.GetAxis("Mouse X");
			float xRot = CrossPlatformInputManager.GetAxis("Mouse Y");

			m_CameraTargetRot *= Quaternion.Euler ((0f - xRot) * 2, 0f, 0f);

			//Rotation around axis y
			if (yRot * asy > 0)
				yRot = 0f;
			else
				asy = 0;

			if(yRot < 0.1 && yRot > -0.1)
				yRot = 0;

			if (cnty < Cycle) {
				m_CharacterTargetRot *= previousX;
				cnty++;
			}

			if (cnty == Cycle)
				prey = 0;
			if (yRot > 0 && prey < 1) {
				asy = 1; prey ++;
			}
			else if (yRot < 0 && prey > -1) {
				asy = -1; prey --;
			}
			if (yRot != 0) {
				previousX = Quaternion.Euler (0f, prey * mini, 0f);
				cnty = 0;
			}


			if (clampVerticalRotation) 
				m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

			if(smooth)
			{
				camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
					smoothTime * Time.deltaTime);
				character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
					smoothTime * Time.deltaTime);
			}
			else
			{
				camera.localRotation = m_CameraTargetRot;
				character.localRotation = m_CharacterTargetRot;
			}

			UpdateCursorLock();
		}

		public void SetCursorLock(bool value)
		{
			lockCursor = value;
			if(!lockCursor)
			{//we force unlock the cursor if the user disable the cursor locking helper
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		public void UpdateCursorLock()
		{
			//if the user set "lockCursor" we check & properly lock the cursos
			if (lockCursor)
				InternalLockUpdate();
		}

		private void InternalLockUpdate()
		{
			if(Input.GetKeyUp(KeyCode.Escape))
			{
				m_cursorIsLocked = false;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				m_cursorIsLocked = true;
			}

			if (m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else if (!m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

			angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

			q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}

	}
}
