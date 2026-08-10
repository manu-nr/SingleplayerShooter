using UnityEngine;

namespace AimLab
{
    public class AimLabDamageable : MonoBehaviour
    {
        private AimLabDamageableManager _manager;

        private float _speed;
        private float _size;
        private float _leftLimit;
        private float _rightLimit;

        private int _direction = 1;

        private bool _canMove;

        public void Initialize(AimLabDamageableManager manager)
        {
            _manager = manager;
        }

        private void Update()
        {
            if (!_canMove)
                return;

            transform.position += Vector3.right * (_speed * _direction * Time.deltaTime);

            if (transform.position.x >= _rightLimit)
                _direction = -1;

            if (transform.position.x <= _leftLimit)
                _direction = 1;
        }

        public void SetCanMove(bool on)
        {
            _canMove = on;
        }
        public void SetDamageableSize(float size)
        {
            _size = size;
        }

        public void SetDamageableSpeed(float speed)
        {
            _speed = speed;
        }
    }
}