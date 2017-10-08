using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    public class PlayerInformationManager : MonoBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        private const float UPDATE_RATE = 1f;

        [SerializeField] private GameObject m_trackedLeftHand;
        [SerializeField] private GameObject m_trackedRightHand;
        [SerializeField] private GameObject m_leftTarget;
        [SerializeField] private GameObject m_rightTarget;

        private DataManager m_dataManager;
        private string m_username = "";

        private float m_timeToUpdate = 0f;
        private bool m_recording = false;

        private List<float> m_currentDataLeft;
        private List<float> m_currentDataRight;
        //---------------------------------------------


        //---------------------------------------------
        private void Start()
        //---------------------------------------------
        {
            m_username = LocalGameManager.singleton.localUsername;

            m_dataManager = FindObjectOfType<DataManager>();

            if (m_dataManager != null)
            {
                m_dataManager.SetUser(m_username);
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        private void Update()
        //---------------------------------------------
        {
            if (0f > m_timeToUpdate)
            {
                m_timeToUpdate -= Time.deltaTime;
                return;
            }

            if (LocalGameManager.singleton != null)
            {
                if (LocalGameManager.singleton.TrackingMovement())
                {
                    if (!m_recording)
                    {
                        StartRecording();                        
                    }
                    DetermineLeftAccuracy();
                    DetermineRightAccuracy();
                }
                else
                {
                    if (m_recording)
                    {
                        EndRecording();
                    }
                }
            }
        }
        //---------------------------------------------


        //---------------------------------------------
        private void StartRecording()
        //---------------------------------------------
        {
            m_recording = true;
            m_currentDataLeft = new List<float>();
            m_currentDataRight = new List<float>();
        }
        //---------------------------------------------

        //---------------------------------------------
        private void EndRecording()
        //---------------------------------------------
        {
            m_recording = false;
            if (m_dataManager != null)
            {
                m_dataManager.ExportPerformanceData(
                    m_currentDataLeft, 
                    m_currentDataRight);
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        private void DetermineLeftAccuracy()
        //---------------------------------------------
        {
            float accuracy = CalculateAccuracy(
                m_leftTarget.transform.position, 
                m_trackedLeftHand.transform.position);

            m_currentDataLeft.Add(accuracy);
        }
        //---------------------------------------------

        //---------------------------------------------
        private void DetermineRightAccuracy()
        //---------------------------------------------
        {
            float accuracy = CalculateAccuracy(
                m_rightTarget.transform.position,
                m_trackedRightHand.transform.position);

            m_currentDataRight.Add(accuracy);
        }
        //---------------------------------------------

        //---------------------------------------------
        private float CalculateAccuracy(
            Vector3 targetPosition, Vector3 trackedPosition)
        //---------------------------------------------
        {
            return Vector3.Distance(targetPosition, trackedPosition);
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}
