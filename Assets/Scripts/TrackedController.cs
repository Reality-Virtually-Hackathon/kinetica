using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinetica
{
    //---------------------------------------------
    [System.Serializable]
    public class TrackedController
    //---------------------------------------------
    {
        public GameObject controllerGO;
        public SteamVR_TrackedObject trackedObj;
        public SteamVR_Controller.Device controlDevice;
    }
    //---------------------------------------------
}