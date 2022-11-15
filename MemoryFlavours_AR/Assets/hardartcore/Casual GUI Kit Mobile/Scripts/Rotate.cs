using UnityEngine;

namespace hardartcore.CasualGUI
{
    public class Rotate : MonoBehaviour
    {
        public float turnSpeed;

        void Update()
        {
            transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
        }
    }
}
