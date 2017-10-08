using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Kinetica
{
    //---------------------------------------------
    public class ServerManager : NetworkBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        public static ServerManager singleton = null;
        //---------------------------------------------

        //---------------------------------------------
        private NetworkInstanceId m_instructor;
        private List<NetworkInstanceId> m_students =
            new List<NetworkInstanceId>();
        private int m_numStudents = 0;
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
        public void AddInstructor(NetworkInstanceId instructorId)
        //---------------------------------------------
        {
            Debug.Log("ServerManager.AddInstructor");

            m_instructor = instructorId;

            GameObject instructor = NetworkServer.FindLocalObject(
                instructorId);

            GameObject instructorSpawn = LocalGameManager
                .singleton.InstructorSpawnPoint();
            instructor.transform.position = instructorSpawn.transform.position;
            instructor.transform.rotation = instructorSpawn.transform.rotation;
            instructor.transform.localScale =
                instructorSpawn.transform.localScale;
        }
        //---------------------------------------------

        //---------------------------------------------
        public void AddStudent(NetworkInstanceId studentId)
        //---------------------------------------------
        {
            Debug.Log("ServerManager.AddStudent");

            m_students.Add(studentId);

            GameObject student = NetworkServer.FindLocalObject(
                studentId);

            int spawnLocationIndex = m_numStudents++;
            GameObject[] studentSpawnPoints = LocalGameManager
                .singleton.StudentSpawnPoints();
            
            if (studentSpawnPoints.Length <= spawnLocationIndex)
            {
                Debug.Log("More than "
                    + spawnLocationIndex
                    + " not yet implemented!");

                spawnLocationIndex = studentSpawnPoints.Length - 1;
            }

            GameObject studentSpawn = 
                studentSpawnPoints[spawnLocationIndex];

            student.GetComponent<NetworkedPlayerManager>().
                RpcInitializeTransform(
                    studentSpawn.transform.position,
                    studentSpawn.transform.rotation,
                    studentSpawn.transform.localScale);
        }
        //---------------------------------------------

        //---------------------------------------------
        public void ActivateGhost()
        //---------------------------------------------
        {
            Debug.Log("ServerManager.ActivateGhost");
            foreach (NetworkInstanceId studentId in m_students)
            {
                GameObject studentGO =
                    NetworkServer.FindLocalObject(studentId);
                NetworkedPlayerManager studentManager =
                    studentGO.GetComponent<NetworkedPlayerManager>();
                studentManager.RpcActivateGhost(m_instructor);
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        public void DeactivateGhost()
        //---------------------------------------------
        {
            Debug.Log("ServerManager.DeactivateGhost");
            foreach (NetworkInstanceId studentId in m_students)
            {
                GameObject studentGO =
                    NetworkServer.FindLocalObject(studentId);
                NetworkedPlayerManager studentManager =
                    studentGO.GetComponent<NetworkedPlayerManager>();
                studentManager.RpcDeactivateGhost(m_instructor);
            }
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}