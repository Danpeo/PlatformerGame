using UnityEngine;

namespace Services
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleVectorAxis();
                if (axis == Vector2.zero)
                {
                    axis = UnityAxis();
                }

                return axis;
            }
        }

        private static Vector2 UnityAxis() => new(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}