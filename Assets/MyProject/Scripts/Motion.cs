using System;
using UnityEngine;
using UnityEngine.Windows;

public class Motion : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;

    public void Move(Vector3 motion)
    {
        if (motion.sqrMagnitude > 0.05f)
        {
            Movement(motion);
            UpdateAnimatorSpeed(_controller.velocity.magnitude);
        }
        else
        {
            UpdateAnimatorSpeed(0);
        }
    }

    private void Movement(Vector3 motion)
    {
        var direction = _camera.transform.TransformDirection(motion);
        direction.y = 0;
        direction.Normalize();

        transform.forward = direction;
        direction += Physics.gravity;

        _controller.Move(direction * _speed * Time.deltaTime);
    }

    private void UpdateAnimatorSpeed(float speed) => _animator.SetFloat("Speed", speed);
}
