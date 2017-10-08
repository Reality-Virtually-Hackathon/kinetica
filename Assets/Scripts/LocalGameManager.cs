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

        //---------------------------------------------
        [Header("Network spawn points")]
        [SerializeField] private GameObject m_instructorSpawnPoint;
        [SerializeField] private GameObject[] m_studentSpawnPoints;
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
    }
    //---------------------------------------------

}
