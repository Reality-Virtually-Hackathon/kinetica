using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    public class BiofeedbackManager : MonoBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        private const int HR_UPDATE_RATE = 1;
        private const int HR_DELTA = 2;
        private const int HR_LOW_BOUND = 50;
        private const int HR_HIGH_BOUND = 180;

        private int m_minHeartRate = 80;
        private int m_maxHeartRate = 120;
        private int m_averageHeartRate;
        private int m_currentHeartRate;
        private int m_heartRateTotal = 0;
        private int m_numSamples = 0;

        private float m_timeToUpdate = 0f;
        //---------------------------------------------

        //---------------------------------------------
        private void OnEnable()
        //---------------------------------------------
        {
            m_currentHeartRate = Random.Range(m_minHeartRate, m_maxHeartRate);
        }
        //---------------------------------------------

        //---------------------------------------------
        private void Update()
        //---------------------------------------------
        {
            if (0f < m_timeToUpdate)
                return;

            DetermineCurrentHeartRate();
            UpdateStatistics();

            m_timeToUpdate = HR_UPDATE_RATE;
        }
        //---------------------------------------------

        //---------------------------------------------
        private void DetermineCurrentHeartRate()
        //---------------------------------------------
        {
            m_currentHeartRate += Random.Range(-HR_DELTA, HR_DELTA);
            Mathf.Clamp(
                m_currentHeartRate,
                HR_LOW_BOUND,
                HR_HIGH_BOUND);
        }
        //---------------------------------------------

        //---------------------------------------------
        private void UpdateStatistics()
        //---------------------------------------------
        {
            if (m_currentHeartRate < m_minHeartRate)
                m_minHeartRate = m_currentHeartRate;
            else if (m_currentHeartRate > m_maxHeartRate)
                m_maxHeartRate = m_currentHeartRate;

            m_heartRateTotal += m_currentHeartRate;
            m_averageHeartRate =
                (m_heartRateTotal / ++m_numSamples);
        }
        //---------------------------------------------


        //---------------------------------------------
        public int CurrentHeartRate()
        //---------------------------------------------
        {
            return m_currentHeartRate;
        }
        //---------------------------------------------

        //---------------------------------------------
        public int MinHeartRate()
        //---------------------------------------------
        {
            return m_minHeartRate;
        }
        //---------------------------------------------

        //---------------------------------------------
        public int MaxHeartRate()
        //---------------------------------------------
        {
            return m_maxHeartRate;
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}