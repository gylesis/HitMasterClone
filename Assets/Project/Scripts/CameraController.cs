using DG.Tweening;
using Project.PlayerLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private float _moveLerpSpeed;
        [SerializeField] private float _rotationLerpSpeed;

        public bool HasToFollow;

        private PlayerFacade _playerFacade;

        [Inject]
        private void Init(PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
        }

        public void RotateTowards(Quaternion rotation)
        {
            var yAngle = _cameraParent.rotation.eulerAngles.y;

            DOVirtual.Float(yAngle, rotation.eulerAngles.y, 1, (value =>
            {
                Vector3 eulerAngles = _cameraParent.rotation.eulerAngles;
                eulerAngles.y = value;
                _cameraParent.rotation = Quaternion.Euler(eulerAngles);
            }));
        }
        
        private void Update()
        {
            if (HasToFollow == false) return;

            Transform player = _playerFacade.Transform;

            _cameraParent.position = Vector3.Lerp(_cameraParent.position, player.transform.position,
                Time.deltaTime * _moveLerpSpeed);

            _cameraParent.rotation = Quaternion.Lerp(_cameraParent.rotation, player.rotation,
                Time.deltaTime * _rotationLerpSpeed);
        }
    }
}