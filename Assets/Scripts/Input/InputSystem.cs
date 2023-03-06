using Input;
using UnityEngine;

namespace Yarde.Input
{
    public class InputSystem
    {
        private readonly Joystick _joystick;

        public InputSystem(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector3 GetMovementNormalized()
        {
            return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        }
    }
}