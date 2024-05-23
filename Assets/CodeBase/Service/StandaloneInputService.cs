using UnityEngine;

namespace CodeBase.Service
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();
                if (axis == Vector2.zero)
                {
                    axis = UnityAxis();
                }
                return axis;
            }
        }

        private static Vector2 UnityAxis()
        {
            return new Vector2(0, 0);
            //return new Vector2(0, SimpleInput.GetAxis(Vertical));
        }
    }
}