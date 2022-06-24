using UnityEngine;

namespace Project.EnemyLogic
{
    public class EnemyRagdoll : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _rigidbodies;

        private void Reset()
        {   
            _rigidbodies = GetComponentsInChildren<Rigidbody>();

            Freeze();
        }

        public void Activate()
        {
            _animator.enabled = false;
            
            foreach (Rigidbody rigidbody in _rigidbodies) 
                rigidbody.isKinematic = false;
        }

        public void Freeze()
        {
            foreach (Rigidbody rigidbody in _rigidbodies) 
                rigidbody.isKinematic = true;
        }
        
    }
}