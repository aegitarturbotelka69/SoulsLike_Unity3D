using System.Collections;
using System.Collections.Generic;
using SLGame.Gameplay;
using UnityEngine;
using UnityEngine.AI;

namespace SLGame.Enemy
{

    public class EnemyOnEvent : MonoBehaviour
    {
        [SerializeField] private EnemyAI _enemyAI;
        private void Start()
        {
            this._enemyAI = this.transform.parent.gameObject.GetComponent<EnemyAI>();
        }

        // Update is called once per frame
        private void CallEndOfTransition()
        {
            _enemyAI.ManualEndTransaction();
        }
    }
}