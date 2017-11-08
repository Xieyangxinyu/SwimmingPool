using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 100f;
        public float YSensitivity = 100f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -180F;
        public float MaximumX = 180F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;

		private Quaternion previousX;
		private Quaternion previousY;
		private bool change = false;
        private Quaternion m_CameraTargetRot;
		private Quaternion m_CharacterTargetRot;
        private bool m_cursorIsLocked = true;
		private int prex = 0;
		private int prey = 0;
		private int asx = 0;
		private int asy = 0;

        public void Init(Transform character, Transform camera)
        {
			m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }


		public void LookRotation(Transform character, Transform camera)
        {
			float yRot = CrossPlatformInputManager.GetAxis("Mouse X");
			float xRot = CrossPlatformInputManager.GetAxis("Mouse Y");

			if (xRot * asx >0)
				xRot = 0f;
			else
				asx = 0;

			if (yRot * asy > 0)
				yRot = 0f;
			else
				asy = 0;

			if (xRot < 0.1 && xRot > -0.1)
				xRot = 0f;
			if (yRot < 0.1 && yRot > -0.1)
				yRot = 0f;

			float mini = 0.2f;

			if (!change) {
				//previousX = Quaternion.Euler (0f, 0f, 0f);
				previousY = Quaternion.Euler (0f, 0f, 0f);
				change = true;
			}

			//m_CameraTargetRot *= previousX;
			m_CharacterTargetRot *= previousY;

			if (xRot > 0 && prex < 1) {
				asx = 1; prex++;
			}
			if (xRot < 0 && prex > -1) {
				asx = -1; prex--;
			}
			if (yRot > 0 && prey < 1) {
				asy = 1; prey++;
			}
			if (yRot < 0 && prey > -1) {
				asy = -1; prey--;
			}
			//if(yRot != 0)
			 //previousX = Quaternion.Euler (0f, prey * mini, 0f);
			if(xRot != 0)
				previousY = Quaternion.Euler (0f - prex * mini, prey * mini, 0f);

			//if (clampVerticalRotation) 
			//	m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                //camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                //   smoothTime * Time.deltaTime);
				character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
					smoothTime * Time.deltaTime);
            }
            else
			{
                //camera.localRotation = m_CameraTargetRot;
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
