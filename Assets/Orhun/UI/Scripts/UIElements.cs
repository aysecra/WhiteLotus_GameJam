using System;
using UnityEngine;
using DG.Tweening;


    public class UIElements : MonoBehaviour, IUIElements
    {
        public void Appear()
        {
            transform.localScale = Vector3.zero;;
            transform.DOScale(Vector3.one, 1f);
        }

        public void Hover()
        {
            transform.DOScale(Vector3.one * 1.2f, 1);
        }

        public void Close()
        {
            transform.DOPunchScale(Vector3.one * .5f, 1f, 0, 0);
        }
        
        private void Awake()
        {
            Appear();
        }

        public void Clicked()
        {
            Close();
        }
    }
