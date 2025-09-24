using UnityEngine;

public class VoodooUI : MonoBehaviour
{
    [SerializeField] private KeySpriteMap _keySpriteMap;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private Transform _tipParent;
    [SerializeField] private SpriteRenderer _keyTipPrefab;

    private KeySequenceUI _sequenceUI;

    public KeySequenceUI Init()
    {
        _sequenceUI = new KeySequenceUI();

        var wSR = Instantiate(_keyTipPrefab, _tipParent);
        var aSR = Instantiate(_keyTipPrefab, _tipParent);
        var sSR = Instantiate(_keyTipPrefab, _tipParent);
        var dSR = Instantiate(_keyTipPrefab, _tipParent);

        _sequenceUI.AddTip(KeyCode.W, wSR);
        _sequenceUI.AddTip(KeyCode.A, aSR);
        _sequenceUI.AddTip(KeyCode.S, sSR);
        _sequenceUI.AddTip(KeyCode.D, dSR);

        UpdatePosition();

        return _sequenceUI;
    }

    private void UpdatePosition()
    {
        foreach (var kv in _sequenceUI.KeyRendererPair)
        {
            switch (kv.Key)
            {
                case KeyCode.W:
                    kv.Value.transform.position = _tipParent.position + Vector3.up * _distance;
                    break;

                case KeyCode.A:
                    kv.Value.transform.position = _tipParent.position + Vector3.left * _distance;
                    break;

                case KeyCode.S:
                    kv.Value.transform.position = _tipParent.position + Vector3.down * _distance;
                    break;

                case KeyCode.D:
                    kv.Value.transform.position = _tipParent.position + Vector3.right * _distance;
                    break;

                default:
                    throw new System.ArgumentException(nameof(kv));
            }
        }
    }
}
