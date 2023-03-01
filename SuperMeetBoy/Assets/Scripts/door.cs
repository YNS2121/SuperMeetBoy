using UnityEngine;

public class door : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
     
    public void Open()
    {
        _animator.SetTrigger("Open");
    }
}
