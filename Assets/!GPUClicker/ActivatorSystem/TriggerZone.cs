using NaughtyAttributes;
using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TriggerZone : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public event Action Interact;

    [Header("UI Settings")]
    [SerializeField] private SpriteRenderer _tipSR;

    [Header("Sprites for Keys")]
    [SerializeField] private KeySpriteMap _keySpriteMap;

    [Header("Trigger Settings")]
    [SerializeField] private float _radius = 3;

    private CircleCollider2D _trigger;
    private bool _playerInside = false;

    private readonly KeyCode _entry = KeyCode.E;

    private void Start() => SetActiveActivator(false);

    private void Update()
    {
        if (!_playerInside) return;

        if (PlayerInputManager.Instance.IsActionTriggered(InputActionType.Interact))
        {
            Interact?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) => HandleTrigger(collision, true);
    private void OnTriggerExit2D(Collider2D collision) => HandleTrigger(collision, false);

    #region ==== Validate ==== 

    private void Validate()
    {
#if UNITY_EDITOR
        if (!_trigger)
            _trigger = GetComponent<CircleCollider2D>();

        _trigger.isTrigger = true;
        _trigger.radius = _radius;
#endif
    }

    #endregion ==============

    private void HandleTrigger(Collider2D collision, bool inside)
    {
        if (!collision.CompareTag(PLAYER_TAG))
            return;

        _playerInside = inside;
        SetActiveActivator(inside);
    }

    public void SetActiveActivator(bool state)
    {
        _tipSR.enabled = state;
        if (state) UpdateTip();
    }

    private void UpdateTip()
    {
        var sprite = _keySpriteMap.GetSprite(_entry);
        _tipSR.sprite = sprite;
    }
}
