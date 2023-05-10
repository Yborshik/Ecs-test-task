using UnityEngine;

namespace Data
{
    public class DoorData : MonoBehaviour
    {
        [SerializeField] private Transform _buttonTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _buttonSize;
        [SerializeField] private float _openDuration;

        public Transform ButtonTransform => _buttonTransform;
        public Animator Animator => _animator;
        public float ButtonSize => _buttonSize;
        public float OpenDuration => _openDuration;
    }
}
