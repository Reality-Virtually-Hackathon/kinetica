using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Kinetica
{
    //---------------------------------------------
    public class DataManager : MonoBehaviour
    //---------------------------------------------
    {
        //---------------------------------------------
        private const string SERVER =
            "https://kinetica-vr.appspot.com/";

        private string m_user = "";
        private bool m_coroutineInProgress = false;

        private List<float> m_performanceDataLeft;
        private List<float> m_performanceDataRight;

        private int m_exerciseNumber = 1;
        //---------------------------------------------

        //---------------------------------------------
        public void SetUser(string name)
        //---------------------------------------------
        {
            Debug.Log("DataManager.SetUser");
            m_user = name;

            if (m_user != "")
            {
                StartCoroutine(IE_PostData());
            }
        }
        //---------------------------------------------

        //---------------------------------------------
        public void ExportPerformanceData(
            List<float> leftData, List<float> rightData)
        //---------------------------------------------
        {
            m_performanceDataLeft = 
                new List<float>(leftData);
            m_performanceDataRight = 
                new List<float>(rightData);

            StartCoroutine(IE_UpdatePerformanceData());
        }
        //---------------------------------------------

        public void ExportHeartRate(float heartRate)
        {
            StartCoroutine(IE_UpdateHeartRate(heartRate));
        }

        //---------------------------------------------
        private IEnumerator IE_PostData()
        //---------------------------------------------
        {
            while (m_coroutineInProgress)
                yield return null;

            m_coroutineInProgress = true;

            string json = "{\"user_name\":\"" + m_user +"\"}";
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            using (UnityWebRequest www = new UnityWebRequest(SERVER + m_user, "POST"))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    //Debug.Log(www.downloadHandler.text);
                }
                else
                {
                    Debug.Log(www.responseCode);
                }
            }

            m_coroutineInProgress = false;
        }
        //---------------------------------------------

        //---------------------------------------------
        public IEnumerator IE_UpdatePerformanceData()
        //---------------------------------------------
        {
            while (m_coroutineInProgress)
                yield return null;

            m_coroutineInProgress = true;

            string json = "{\"Exercise " 
                + m_exerciseNumber 
                + "_Left\":\""
                + JsonUtility.ToJson(m_performanceDataLeft.ToArray()) + "\"}";
            using (UnityWebRequest www = UnityWebRequest.Put(SERVER + m_user, json))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    Debug.Log(www.downloadHandler.text);
                }
                else
                {
                    Debug.Log(www.responseCode);
                }
            }

            json = "{\"Exercise "
                + m_exerciseNumber
                + "_Right\":\""
                + JsonUtility.ToJson(m_performanceDataRight.ToArray()) + "\"}";
            using (UnityWebRequest www = UnityWebRequest.Put(SERVER + m_user, json))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    Debug.Log(www.downloadHandler.text);
                }
                else
                {
                    Debug.Log(www.responseCode);
                }
            }

            m_exerciseNumber++;

            m_coroutineInProgress = false;
        }
        //---------------------------------------------

        //---------------------------------------------
        public IEnumerator IE_UpdateHeartRate(float heartRate)
        //---------------------------------------------
        {
            while (m_coroutineInProgress)
                yield return null;

            m_coroutineInProgress = true;

            string json = "{\"heart_rate " 
                + Time.time 
                + "\":\"" 
                + heartRate 
                + "\"}";
            using (UnityWebRequest www = UnityWebRequest.Put(SERVER + m_user, json))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    Debug.Log(www.downloadHandler.text);
                }
                else
                {
                    Debug.Log(www.responseCode);
                }
            }

            m_coroutineInProgress = false;
        }
        //---------------------------------------------
    }
    //---------------------------------------------
}