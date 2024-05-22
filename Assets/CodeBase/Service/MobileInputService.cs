using UnityEngine;

namespace CodeBase.Service
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}