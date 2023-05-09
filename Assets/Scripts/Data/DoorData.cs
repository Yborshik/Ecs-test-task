using UnityEngine;

public class DoorData : MonoBehaviour
{
    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _buttonSize;

    public Transform ButtonTransform => _buttonTransform;
    public Animator Animator => _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
