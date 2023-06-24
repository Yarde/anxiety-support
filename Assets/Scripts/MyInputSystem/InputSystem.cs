using UnityEngine;

namespace Yarde.MyInputSystem
{
    public class InputSystem
    {
        private readonly Joystick _joystick;

        private float X => Mathf.Clamp(_joystick.Horizontal + Input.GetAxis("Horizontal"), -1, 1);
        private float Y => Mathf.Clamp(_joystick.Vertical + Input.GetAxis("Vertical"), -1, 1);
        public Vector3 InputValue => new(X, 0, Y);
        private float TargetAngle => Mathf.Atan2(X, Y) * Mathf.Rad2Deg;
        public bool IsMoving => InputValue.magnitude > 0;

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