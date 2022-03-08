using UnityEngine;
using UnityEditor;

using SLGame.Enemy;

namespace SLGame.Editor
{
    [CustomEditor(typeof(ForwardFOV))]
    public class Editor_EnemyForwardFOV : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            ForwardFOV enemyFOV = (ForwardFOV)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(enemyFOV.transform.position, Vector3.up, Vector3.forward, 360, enemyFOV.Radius);

            Vector3 leftSideViewAngle = DirectionFromAngle(enemyFOV.transform.eulerAngles.y, -enemyFOV.Angle / 2);
            Vector3 rightSideViewAngle = DirectionFromAngle(enemyFOV.transform.eulerAngles.y, enemyFOV.Angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + leftSideViewAngle * enemyFOV.Radius);
            Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + rightSideViewAngle * enemyFOV.Radius);

            if (enemyFOV.CanSeePlayer)
            {
                Handles.color = Color.green;
                Handles.DrawLine(enemyFOV.transform.position, Player.Instance.transform.position);
            }
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}