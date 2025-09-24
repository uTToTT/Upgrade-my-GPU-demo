using UnityEngine;

public class FireExtinguisherUI : MonoBehaviour
{
    [SerializeField] private KeySpriteMap _keySpriteMap;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private Transform _tipParent;
    [SerializeField] private SpriteRenderer _keyTipPrefab;

    private KeySequenceUI _sequenceUI;

    public KeySequenceUI Init()
    {
        _sequenceUI = new KeySequenceUI();

        var lSR = Instantiate(_keyTipPrefab, _tipParent);
        var rSR = Instantiate(_keyTipPrefab, _tipParent);

        _sequenceUI.AddTip(KeyCode.Mouse0, lSR);
        _sequenceUI.AddTip(KeyCode.Mouse1, rSR);

        UpdatePosition();

        return _sequenceUI;
    }

    private void UpdatePosition()
    {
        foreach (var kv in _sequenceUI.KeyRendererPair)
        {
            switch (kv.Key)
            {
                case KeyCode.Mouse0:
                    kv.Value.transform.position = _tipParent.position + Vector3.left * _distance;
                    break;

                case KeyCode.Mouse1:
                    kv.Value.transform.position = _tipParent.position + Vector3.right * _distance;
                    break;

                default:
                    throw new System.ArgumentException(nameof(kv));
            }
        }
    }
}
