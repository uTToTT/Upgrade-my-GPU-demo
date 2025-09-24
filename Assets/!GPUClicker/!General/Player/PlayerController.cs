using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;

    public MovementController MovementController => _movementController;
}
