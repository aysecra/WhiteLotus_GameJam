using System;
using System.Collections;
using Cinemachine;
using Main;
using UnityEngine;


    public class CameraManager : MonoBehaviour
    {
        public CinemachineVirtualCamera vm1;
        public CinemachineVirtualCamera vm2;
        public CinemachineVirtualCamera vm3;
        public static bool isCamChanged = false;

        public void CamChange()
        {
            vm1.Priority = 0;
            vm2.Priority = 1;
            vm3.Priority = 0;
        }
        public void FinishCamChange()
        {
            vm1.Priority = 0;
            vm2.Priority = 0;
            vm3.Priority = 1;
        }
    }

