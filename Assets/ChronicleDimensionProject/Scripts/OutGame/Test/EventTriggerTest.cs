using UnityEngine;

namespace ChronicleDimensionProject.Scripts.OutGame.Test
{
    public class EventTriggerTest : MonoBehaviour {


        void Update () {
            if (Input.GetKeyDown ("q"))
            {
                EventManager.TriggerEvent ("test");
            }

            if (Input.GetKeyDown ("o"))
            {
                EventManager.TriggerEvent ("Spawn");
            }

            if (Input.GetKeyDown ("p"))
            {
                EventManager.TriggerEvent ("Destroy");
            }

            if (Input.GetKeyDown ("x"))
            {
                EventManager.TriggerEvent ("Junk");
            }
        }
    }
}