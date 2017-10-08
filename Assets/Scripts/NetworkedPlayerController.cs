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
        [System.Serializable]
        public class AvatarTracker
        //---------------------------------------------
        {
            public GameObject[] sourceObjects;
            public GameObject[] avatarObjects;
        }
        //---------------------------------------------

        //---------------------------------------------
        //[SerializeField] protected GameObject m_playerTrackedHead;
        [SerializeField] protected TrackedController m_leftController;
        [SerializeField] protected TrackedController m_rightController;
        //[SerializeField] protected GameObject m_avatarHead;
        //[SerializeField] protected GameObject m_avatarLeftHand;
        //[SerializeField] protected GameObject m_avatarRightHand;
        [Header("Avatar to source transform")]
        [Tooltip("Objects are coupled by array index")]
        [SerializeField] protected AvatarTracker[] m_trackedAvatarObjects;

        private Vector3 m_avatarHandOffset = new Vector3(
            0f, 0f, -.06f);
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
            //if (m_playerTrackedHead != null
            //    && m_avatarHead != null)
            //{
            //    Transform targetTransform = m_playerTrackedHead.transform;
            //    m_avatarHead.transform.rotation =
            //        targetTransform.rotation;
            //    m_avatarHead.transform.position = targetTransform.position;
            //    //m_avatarHead.transform.localScale = targetTransform.localScale;
            //}


            //if (m_leftController != null
            //    && m_avatarLeftHand != null)
            //{
            //    Transform targetTransform = m_leftController.controllerGO.transform;
            //    m_avatarLeftHand.transform.rotation =
            //        targetTransform.rotation;
            //    m_avatarLeftHand.transform.position = targetTransform.position;
            //    //m_avatarLeftHand.transform.localScale = targetTransform.localScale;
            //}

            //if (m_rightController != null
            //    && m_avatarRightHand != null)
            //{
            //    Transform targetTransform = m_rightController.controllerGO.transform;
            //    m_avatarRightHand.transform.rotation =
            //        targetTransform.rotation;
            //    m_avatarRightHand.transform.position = targetTransform.position;
            //    //m_avatarRightHand.transform.localScale = targetTransform.localScale;
            //}

            for (int i = 0; i < m_trackedAvatarObjects.Length; i++)
            {
                AvatarTracker trackedObjs = m_trackedAvatarObjects[i];

                if (trackedObjs == null)
                    continue;

                for (int j = 0; j < trackedObjs.avatarObjects.Length; j++)
                {
                    if (j >= trackedObjs.sourceObjects.Length)
                        break;

                    if (trackedObjs.avatarObjects[j])
                    {
                        Transform targetTransform = trackedObjs.sourceObjects[j].transform;
                        if (trackedObjs.sourceObjects[j] != null)
                        {
                            trackedObjs.avatarObjects[j].transform.localRotation =
                                targetTransform.localRotation;
                            trackedObjs.avatarObjects[j].transform.localPosition =
                                new Vector3(
                                    targetTransform.localPosition.x + m_avatarHandOffset.x,
                                    targetTransform.localPosition.y + m_avatarHandOffset.y,
                                    targetTransform.localPosition.z + m_avatarHandOffset.z);
                        }
                    }
                }
            }
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}