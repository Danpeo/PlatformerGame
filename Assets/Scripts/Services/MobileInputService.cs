using UnityEngine;

namespace Services
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleVectorAxis();
    }
}