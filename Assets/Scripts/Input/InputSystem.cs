using UnityEngine;

namespace Yarde.Input
{
    public class InputSystem
    {
        private readonly Joystick _joystick;

        private float X => _joystick.Horizontal;
        private float Y => _joystick.Vertical;
        public Vector3 Direction => new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        private float TargetAngle => Mathf.Atan2(X, Y) * Mathf.Rad2Deg;
        public bool IsMoving => Direction.magnitude > 0;

        private float _turnSmoothVelocity;

        public InputSystem(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Quaternion LookRotationSmoothed(float currentAngle, float smoothTime)
        {
            var smoothedAngle = Mathf.SmoothDampAngle(currentAngle, TargetAngle,
                ref _turnSmoothVelocity, smoothTime);
            return Quaternion.Euler(0f, smoothedAngle, 0f);
        }
    }
}