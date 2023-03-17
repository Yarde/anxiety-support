using Input;
using UnityEngine;

namespace Yarde.Input
{
    public class InputSystem
    {
        private readonly Joystick _joystick;

        private float X => _joystick.Horizontal;
        private float Y => _joystick.Vertical;
        public Vector3 Direction => new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        public Quaternion LookRotation => Quaternion.Euler(0f, Mathf.Atan2(X, Y) * Mathf.Rad2Deg, 0f);
        public bool IsMoving => Direction.magnitude > 0;

        public InputSystem(Joystick joystick)
        {
            _joystick = joystick;
        }
    }
}