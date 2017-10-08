using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    public class LocalGameManager : MonoBehaviour
    //---------------------------------------------
    {
        public static LocalGameManager singleton = null;

        [Header("Debug settings")]
        public string localUsername = "Test";

        //---------------------------------------------
        [Header("Network spawn points")]
        [SerializeField] private GameObject m_instructorSpawnPoint;
        [SerializeField] private GameObject[] m_studentSpawnPoints;

        private bool m_trackMovement = false;
        //---------------------------------------------


        //---------------------------------------------
        private void Awake()
        //---------------------------------------------
        {
            if (singleton != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                singleton = this;
            }
        }
        //---------------------------------------------


        //---------------------------------------------
        public GameObject InstructorSpawnPoint()
        //---------------------------------------------
        {
            return m_instructorSpawnPoint;
        }
        //---------------------------------------------

        //---------------------------------------------
        public GameObject[] StudentSpawnPoints()
        //---------------------------------------------
        {
            return m_studentSpawnPoints;
        }
        //---------------------------------------------

        //---------------------------------------------
        public void EnableMovementTracking(bool enabled)
        //---------------------------------------------
        {
            m_trackMovement = enabled;
        }
        //---------------------------------------------

        //---------------------------------------------
        public bool TrackingMovement()
        //---------------------------------------------
        {
            return m_trackMovement;
        }
        //---------------------------------------------
    }
    //---------------------------------------------

}
