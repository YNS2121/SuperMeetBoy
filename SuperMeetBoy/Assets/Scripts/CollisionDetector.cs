using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{       
        [SerializeField]
        private string _colliderScript;
        [SerializeField]
        private UnityEvent _collisonEntered;
        [SerializeField]
        private UnityEvent _collisionExit;

    private void OnCollisionExit2D(Collision2D col)
    {
        
        if (col.gameObject.GetComponent(_colliderScript))
        {
            _collisionExit?.Invoke();

        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent(_colliderScript))
        {
            _collisonEntered?.Invoke();

        }
    }
}
