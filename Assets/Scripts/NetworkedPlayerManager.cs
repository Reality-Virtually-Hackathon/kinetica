using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

namespace Kinetica
{
    //---------------------------------------------
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool>
    //---------------------------------------------
    {
    }
    //---------------------------------------------


    //---------------------------------------------
    public class NetworkedPlayerManager : NetworkBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        [SerializeField] private ToggleEvent m_toggleLocal;
        [SerializeField] private GameObject m_instructorAvator;
        [SerializeField] private GameObject m_studentAvatar;
        [SerializeField] private InstructorGhostManager m_instructorGhost;

        [SyncVar] private bool m_isInstructor = false;

        //private static List<NetworkInstanceId> m_connectedPlayers =
        //    new List<NetworkInstanceId>();
        //---------------------------------------------


        //---------------------------------------------
        private void Start()
        //---------------------------------------------
        {
            if (isServer)
            {
                m_instructorGhost.gameObject.SetActive(false);

                if (isLocalPlayer)
                {
                    m_toggleLocal.Invoke(true);

                    ServerManager.singleton.AddInstructor(
                        this.GetComponent<NetworkIdentity>().netId);

                    m_isInstructor = true;
                    m_instructorAvator.SetActive(false);
                    m_studentAvatar.SetActive(false);

                    this.GetComponent<StudentController>().enabled = false;
                }
                else
                {
                    m_studentAvatar.SetActive(true);
                }
            }
            else
            {
                if (isLocalPlayer)
                {
                    m_toggleLocal.Invoke(true);

                    CmdAddStudent();

                    m_isInstructor = false;
                    m_instructorAvator.SetActive(false);
                    m_studentAvatar.SetActive(false);
                    m_instructorGhost.gameObject.SetActive(true);

                    this.GetComponent<InstructorController>().enabled = false;
                }
                else
                {
                    if (m_isInstructor)
                    {
                        m_instructorAvator.SetActive(true);
                    }
                    else
                    {
                        m_studentAvatar.SetActive(true);
                    }

                    m_instructorGhost.gameObject.SetActive(false);
                }
            }

            //m_connectedPlayers.Add(
            //    this.GetComponent<NetworkIdentity>().netId);
        }
        //---------------------------------------------

        //---------------------------------------------
        [Command]
        private void CmdAddStudent()
        //---------------------------------------------
        {
            ServerManager.singleton.AddStudent(
              this.GetComponent<NetworkIdentity>().netId);
        }
        //---------------------------------------------


        //---------------------------------------------
        [ClientRpc]
        public void RpcInitializeTransform(
            Vector3 startPos, Quaternion startRot, Vector3 startScale)
        //---------------------------------------------
        {
            if (!isLocalPlayer)
                return;

            this.transform.position = startPos;
            this.transform.rotation = startRot;
            this.transform.localScale = startScale;
        }
        //---------------------------------------------

        //---------------------------------------------
        [ClientRpc]
        public void RpcActivateGhost(NetworkInstanceId instructorId)
        //---------------------------------------------
        {
            Debug.Log("NetworkedPlayerManager.RpcActivateGhost");

            if (!isLocalPlayer)
                return;

            GameObject instructorGO = ClientScene.FindLocalObject(instructorId);

            m_instructorGhost.EnableGhostFollow(instructorGO);
        }
        //---------------------------------------------

        [ClientRpc]
        public void RpcDeactivateGhost(NetworkInstanceId instructorId)
        {
            Debug.Log("NetworkedPlayerManager.RpcDeactivateGhost");

            if (!isLocalPlayer)
                return;

            GameObject instructorGO = ClientScene.FindLocalObject(instructorId);

            m_instructorGhost.DisableGhostFollow();
        }
    }
    //---------------------------------------------
}