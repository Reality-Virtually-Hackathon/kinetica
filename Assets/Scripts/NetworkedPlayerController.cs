using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    public class NetworkedPlayerController : MonoBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        [SerializeField] protected GameObject m_playerTrackedHead;
        [SerializeField] protected TrackedController m_leftController;
        [SerializeField] protected TrackedController m_rightController;
        [SerializeField] protected GameObject m_avatarHead;
        [SerializeField] protected GameObject m_avatarLeftHand;
        [SerializeField] protected GameObject m_avatarRightHand;
        //---------------------------------------------


        //---------------------------------------------
        protected virtual void Update()
        //---------------------------------------------
        {
            UpdateAvatarTransforms();

            SteamVR_Controller.Device leftDevice;
            SteamVR_Controller.Device rightDevice;

            if (m_leftController != null
                && (int)m_leftController.trackedObj.index > -1)
            {
                leftDevice = SteamVR_Controller.Input((int)m_leftController.trackedObj.index);

                LeftInputs(ref leftDevice);
            }

            if (m_rightController != null
                && (int)m_rightController.trackedObj.index > -1)
            {
                rightDevice = SteamVR_Controller.Input((int)m_rightController.trackedObj.index);

                RightInputs(ref rightDevice);
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        protected virtual void LeftInputs(ref SteamVR_Controller.Device device)
        //---------------------------------------------
        {
        }
        //---------------------------------------------

        //---------------------------------------------
        protected virtual void RightInputs(ref SteamVR_Controller.Device device)
        //---------------------------------------------
        {
        }
        //---------------------------------------------


        //---------------------------------------------
        protected void UpdateAvatarTransforms()
        //---------------------------------------------
        {
            //Debug.Log("NetworkedPlayerController.UpdateAvatarTransforms");
            if (m_playerTrackedHead != null
                && m_avatarHead != null)
            {
                Transform targetTransform = m_playerTrackedHead.transform;
                m_avatarHead.transform.rotation =
                    targetTransform.rotation;
                m_avatarHead.transform.position = targetTransform.position;
                //m_avatarHead.transform.localScale = targetTransform.localScale;
            }


            if (m_leftController != null
                && m_avatarLeftHand != null)
            {
                Transform targetTransform = m_leftController.controllerGO.transform;
                m_avatarLeftHand.transform.rotation =
                    targetTransform.rotation;
                m_avatarLeftHand.transform.position = targetTransform.position;
                //m_avatarLeftHand.transform.localScale = targetTransform.localScale;
            }

            if (m_rightController != null
                && m_avatarRightHand != null)
            {
                Transform targetTransform = m_rightController.controllerGO.transform;
                m_avatarRightHand.transform.rotation =
                    targetTransform.rotation;
                m_avatarRightHand.transform.position = targetTransform.position;
                //m_avatarRightHand.transform.localScale = targetTransform.localScale;
            }
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}