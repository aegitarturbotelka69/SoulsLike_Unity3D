using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    public enum States
    {
        Idle,
        Move,
        Roll,
        Run,
        StopRun,
        Falling,
        HardLand,
        Patrolling,
        Chasing,
        LightAttack,
        HeavyAttack,
        DodgeBackJump,
        RestoringPower
    }
}
