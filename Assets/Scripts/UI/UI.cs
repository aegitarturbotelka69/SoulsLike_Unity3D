using System;
using System.Collections;
using System.Collections.Generic;
using SLGame.Input;
using UnityEngine;

namespace SLGame.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI : MonoBehaviour
    {
        public static event Action<bool> OnMenuOpened;

        [Header("References:")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("In game:")]
        [SerializeField] private bool _showUI = false;

        private void Awake()
        {
            _canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
        }

        private void Update()
        {
            if (VirtualInputManager.Instance.AdditionalMenu)
            {
                _showUI = !_showUI;

                OnMenuOpened?.Invoke(_showUI);

                _canvasGroup.alpha = (_showUI == true) ? 1f : 0f;
                _canvasGroup.interactable = _showUI;
            }
        }
    }
}