using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.UI
{
    public class AdditionalMenu : MonoBehaviour
    {
        private void Awake()
        {
            UI.OnMenuOpened += OpenCloseAdditionalMenu;
        }

        private void OpenCloseAdditionalMenu(bool status)
        {
            //this.gameObject.SetActive(status);
        }
    }
}