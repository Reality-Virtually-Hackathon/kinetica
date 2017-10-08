using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    public class InstructorController : NetworkedPlayerController
    //---------------------------------------------
    {
        //---------------------------------------------
        private bool m_leftTriggerDown = false;
        private bool m_rightTriggerDown = false;
        //--------------------------------------------- 


        //---------------------------------------------
        protected override void Update()
        //---------------------------------------------
        {
            base.Update();

            if (m_leftTriggerDown)
            {
                // holding left trigger
                //Debug.Log("Left trigger held!");
            }

            if (m_rightTriggerDown)
            {
                // holding right trigger
                //Debug.Log("Right trigger held!");
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        protected override void LeftInputs(ref SteamVR_Controller.Device device)
        //---------------------------------------------
        {
            //Debug.Log("InstructorController.LeftInputs");
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                //Debug.Log("Left trigger pressed");
                if (!m_leftTriggerDown)
                {
                    // Start trace
                    if (null != ServerManager.singleton)
                    {
                        ServerManager.singleton.ActivateGhost();
                    }
                    m_leftTriggerDown = true;
                }
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (m_leftTriggerDown)
                {
                    //Debug.Log("Left trigger released");
                    // end trace
                    if (null != ServerManager.singleton)
                    {
                        ServerManager.singleton.DeactivateGhost();
                    }
                    m_leftTriggerDown = false;
                }
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        protected override void RightInputs(ref SteamVR_Controller.Device device)
        //---------------------------------------------
        {
            //Debug.Log("InstructorController.RightInputs");
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (!m_rightTriggerDown)
                {
                    Debug.Log("Right trigger pressed");
                    // start trace
                    if (null != ServerManager.singleton)
                    {
                        Debug.Log("Singleton found!");
                        ServerManager.singleton.ActivateGhost();
                    }
                    m_rightTriggerDown = true;
                }
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (m_rightTriggerDown)
                {
                    Debug.Log("Right trigger released");
                    // end trace
                    if (null != ServerManager.singleton)
                    {
                        ServerManager.singleton.DeactivateGhost();
                    }
                    m_rightTriggerDown = false;
                }
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        public GameObject LeftHand()
        //---------------------------------------------
        {
            return m_leftController.controllerGO;
        }
        //---------------------------------------------

        //---------------------------------------------
        public GameObject RightHand()
        //---------------------------------------------
        {
            return m_rightController.controllerGO;
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}