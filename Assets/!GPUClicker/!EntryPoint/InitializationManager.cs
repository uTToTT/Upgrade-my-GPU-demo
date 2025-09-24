namespace UTToTTGames.Initialization
{
    using NaughtyAttributes;
    using System;
    using UnityEngine;
    using UTToTTGames.Debug;

    public class InitializationManager : MonoBehaviour, IDebuggable
    {
        [Serializable]
        private class InitializationGroup
        {
            [SerializeField] private string _name;
            [SerializeField] private MonoBehaviour[] _targets;

            #region ==== Properties ====

            public string Name => _name;
            public MonoBehaviour[] Targets => _targets;

            #endregion =================
        }

        [HorizontalLine]
        [BoxGroup("Debug")]
        [SerializeField] private bool _debugging;

        [HorizontalLine]
        [BoxGroup("InitGroups")]
        [SerializeField] private InitializationGroup[] _awakeGroups;
        [BoxGroup("InitGroups")]
        [SerializeField] private InitializationGroup[] _startGroups;

        #region ==== Properties ====

        public bool Debugging => _debugging;

        #endregion =================

        #region ==== Unity API ====

        private void Start() => InitGroups(_startGroups);
        private void Awake() => InitGroups(_awakeGroups);

        private void OnValidate()
        {
            ValidateGroups(_awakeGroups);
            ValidateGroups(_startGroups);
        }

        #endregion ================

        #region ==== Init ====

        private void InitGroups(InitializationGroup[] groups)
        {
            foreach (var group in groups)
            {
                foreach (var target in group.Targets)
                {
                    if (target && target is IInitializable initTarget)
                    {
                        if (initTarget.Init())
                        {
                            DebugUtils.LogIfDebug
                                (this, $"[{group.Name}][{target.name}] Init success");
                        }
                        else
                        {
                            Debug.LogError($"[{group.Name}][{target.name}] Init failed");
                        }
                    }
                }
            }
        }

        #endregion ===========

        #region ==== Validation ====

        private void ValidateGroups(InitializationGroup[] groups)
        {
            foreach (var group in groups)
            {
                foreach (var target in group.Targets)
                {
                    if (target && target is not IInitializable)
                    {
                        Debug.LogError
                            ($"Target [{target.name}] in group {group.Name} must implement IInitializable.", target);
                    }
                }
            }
        }

        #endregion =================

        #region ==== Debug ====

        public void SetDebug(bool state) => _debugging = state;

        #endregion ============
    }
}
