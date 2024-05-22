using UnityEngine;

namespace CodeBase.Service
{
    public abstract class InputService : IInputService
    {
        protected const string Vertical = "Vertical";
        protected const string MouseY = "MouseY";
        
        public abstract Vector2 Axis { get; }

        protected static Vector2 SimpleInputAxis() => 
            new Vector2(0, SimpleInput.GetAxis(MouseY));
    }
}