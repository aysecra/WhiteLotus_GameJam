using System;
using System.Collections;
using System.Collections.Generic;
using Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        [SerializeField] private Transform player;
        [SerializeField] private Image lineMap;
        [SerializeField] private RectTransform pointer;
        [SerializeField] private CameraManager CM;
        [SerializeField] private ButterfliesController BC;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject startTap;
        [SerializeField] private GameObject finishLine;
        [SerializeField] private GameObject retry;
        [SerializeField] private GameObject nextLevel;
        private float maxPos;
        private float _curPos;
        private void Awake() {
            instance = this;
        }

        private void Start()
        {
            maxPos = finishLine.transform.position.z - 4f;
        }

        private void Update()
        {
            lineMap.fillAmount = CalcFill();
            scoreText.text = BC.butterflyList.Count.ToString();

            if (PlyrController.IsRunning)
            {
                startTap.SetActive(false);
            }
        }
        
        private float CalcFill()
        {
            _curPos = (100 / maxPos) * player.position.z / 100;
            if (_curPos <=1)
            {
                MovePointer(_curPos);
            }
            else if (_curPos >= 1)
            {
                CM.FinishCamChange();
            }

            return _curPos;
        }

        private void MovePointer(float pos)
        {
            pointer.transform.localPosition = new Vector3(pos * 700 - 350, 0, 0);
        }

        public void StartGame(UIElements button)
        {
            button.Close();
            StartCoroutine(CameraChange());
        }
        public void Retry()
        {
            retry.SetActive(true);
        }
        public void NextLevel()
        {
            nextLevel.SetActive(true);
        }

        private IEnumerator CameraChange()
        {
            CM.CamChange();
            yield return new WaitForSeconds(2);
            CameraManager.isCamChanged = true;
        }
    }

