using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class SmoothOrthographicCameraFollow : MonoBehaviour
    {
        [SerializeField] private bool lockX;
        [SerializeField] private bool lockY;

        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private Vector3 offset;

        private Vector3 velocity = Vector3.zero;
        private Transform target;

        void Update()
        {
            MoveCamera();
        }

        public void SetTarget(Transform cameraTarget)
        {
            target = cameraTarget;
            TeleportToTarget();
        }

        public void ResetCameraConstraints()
        {
            lockX = false;
            lockY = false;
        }

        public void UnlockX()
        {
            lockX = false;
        }

        public void LockX()
        {
            lockX = true;
        }

        public void TeleportToTarget()
        {
            transform.position = target.position + offset;
        }

        private void MoveCamera()
        {
            Vector3 goalPos = target.position + offset;
            if (lockX && (transform.position.x == target.position.x)) goalPos.x = transform.position.x;
            if (lockY) goalPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        }
    }
}