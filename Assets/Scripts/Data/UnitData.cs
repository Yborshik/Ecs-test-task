using UnityEngine;

namespace Data
{
    public class UnitData : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public Animator Animator => _animator;
    }
}