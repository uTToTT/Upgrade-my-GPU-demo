using UnityEngine;
using UTToTTGames.Initialization;

public class FireExtinguisherManager : MonoBehaviour, IInitializable
{
    [SerializeField] private GPU _gpu;
    [Space]
    [SerializeField] private FireExtinguisher _fireExtinguisher;
    [SerializeField] private FireExtinguisherUI _ui;

    private KeySequenceUI _keySequenceUI;

    public bool Init() // גûחûגאועסÿ קונוח bootstrap ג awake
    {
        _keySequenceUI = _ui.Init();
        _fireExtinguisher.Init(_keySequenceUI, _gpu);

        return true;
    }
}
