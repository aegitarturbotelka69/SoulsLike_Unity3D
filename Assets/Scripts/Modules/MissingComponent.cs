using UnityEngine;

namespace SLGame.Modules
{
    public class ComponentInspector
    {
        public void ComponentAttendCheck<T>(GameObject gameObjectWithComponent, T componentToCheck)
        {
            if (componentToCheck == null)
            {
                componentToCheck = gameObjectWithComponent.GetComponent<T>();

                if (componentToCheck == null)
                {
                    Debug.LogError(gameObjectWithComponent.name.ToString() + "missing required component: " + typeof(T).ToString());
                }
            }
        }
    }
}