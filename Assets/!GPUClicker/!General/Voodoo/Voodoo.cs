using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Voodoo : MonoBehaviour
{
    private const float DEFAULT_FACTOR = 1.0f;

    private VoodooConfig _config;
    private CircleCollider2D _trigger;

    private float _currFactor;
    private float _timer;

    private readonly List<IFactorProvider> _targets = new();

    #region ==== Init ====

    public void InitConfig(VoodooConfig voodooConfig)
    {
        _config = Instantiate(voodooConfig);

        _trigger.radius = _config.Radius;
    }

    #endregion ===========

    #region ==== Unity API ====

    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;

        if (_timer <= 0 && _currFactor != 1) 
            ResetFactor();
    }

    private void OnTriggerEnter2D(Collider2D collision) => AddTarget(collision);
    private void OnTriggerExit2D(Collider2D collision) => RemoveTarget(collision);

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (_trigger == null)
            _trigger = GetComponent<CircleCollider2D>();

        _trigger.isTrigger = true;
#endif
    }

    #endregion ================

    public void CancelVoodoo()
    {
        foreach (var item in _targets)
        {
            RemoveBuff(item);
        }

        ResetFactor();
    }

    public void CastVoodoo()
    {
        foreach (var item in _targets)
        {
            AddBuff(item);
        }

        _currFactor += _config.FactorByClick;
    }

    private void ResetFactor()
    {
        _currFactor = 1;
    }

    private void AddBuff(IFactorProvider factorProvider) =>
        SetVoodooAmount(factorProvider, _currFactor, _config.Duration);

    private void RemoveBuff(IFactorProvider factorProvider) =>
        factorProvider.FactorController.RemoveModifier(FactorController.FACTOR_VOODOO_ID);

    private void SetVoodooAmount(IFactorProvider factorProvider, float factor, float duration)
    {
        factorProvider.FactorController.AddTemporaryModifier
            (FactorController.FACTOR_VOODOO_ID, factor, duration);

        _timer = duration;
    }

    private void AddTarget(Collider2D collision)
    {
        if (collision.TryGetComponent(out IFactorProvider target))
        {
            if (_targets.Contains(target))
                throw new System.ArgumentException(nameof(target));

            _targets.Add(target);
        }
    }

    private void RemoveTarget(Collider2D collision)
    {
        if (collision.TryGetComponent(out IFactorProvider target))
        {
            if (!_targets.Contains(target))
                throw new System.ArgumentException(nameof(target));

            _targets.Remove(target);
        }
    }
}
