using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    public class InstructorGhostManager : MonoBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        [SerializeField] private GameObject m_ghostLeftHand;
        [SerializeField] private GameObject m_ghostRightHand;

        private GameObject m_instructorLeftHand = null;
        private GameObject m_instructorRightHand = null;

        private bool m_followInstructor = false;
        //---------------------------------------------


        //---------------------------------------------
        private void Update()
        //---------------------------------------------
        {
            if (!m_followInstructor)
                return;

            if (m_instructorLeftHand != null)
            {
                m_ghostLeftHand.transform.localPosition = 
                    m_instructorLeftHand.transform.localPosition;
            }

            if (m_instructorRightHand != null)
            {
                m_ghostRightHand.transform.localPosition =
                    m_instructorRightHand.transform.localPosition;
            }
        }
        //---------------------------------------------


        //---------------------------------------------
        public void EnableGhostFollow(GameObject instructor)
        //---------------------------------------------
        {
            Debug.Log("EnableGhostFollow");

            m_followInstructor = true;

            InstructorController instrContr = instructor
                .GetComponent<InstructorController>();
            m_instructorLeftHand = instrContr.LeftHand();
            m_instructorRightHand = instrContr.RightHand();

            m_ghostLeftHand.SetActive(true);
            m_ghostRightHand.SetActive(true);
        }
        //---------------------------------------------

        //---------------------------------------------
        public void DisableGhostFollow()
        //---------------------------------------------
        {
            Debug.Log("DisableGhostFollow");

            m_followInstructor = false;

            m_instructorRightHand = null;
            m_instructorLeftHand = null;

            m_ghostLeftHand.SetActive(false);
            m_ghostRightHand.SetActive(false);
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}