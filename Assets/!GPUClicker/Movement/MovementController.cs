using NaughtyAttributes;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Required]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;

    private float _targetSpeed;

    private Vector2 _inputAxis = new Vector2();

    #region ==== Unity API ====

    private void FixedUpdate()
    {
        if (!PlayerInputManager.Instance.IsEnabled(InputActionType.Movement))
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        GetInput();
        ProcessMove();
    }

    #endregion ================


    private void GetInput()
    {
        _inputAxis.x = Input.GetAxisRaw("Horizontal");
        _inputAxis.y = Input.GetAxisRaw("Vertical");

        if (_inputAxis.sqrMagnitude > 1f)
            _inputAxis.Normalize();

        _targetSpeed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _speed;

    }

    private void ProcessMove() => _rb.velocity = _inputAxis * _targetSpeed;
}
