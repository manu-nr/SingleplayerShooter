using UnityEngine;
using UnityEngine.Pool;


namespace AimLab
{
    public class AimLabDamageableManager : MonoBehaviour
    {
        [Header("Damageable")]
        [SerializeField] private AimLabDamageable _damageablePrefab;

        [Header("Spawn Area")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _horizontalRange = 5f;

        [Header("Pool")]
        [SerializeField] private int _defaultPoolSize = 10;
        [SerializeField] private int _maxPoolSize = 30;

        private ObjectPool<AimLabDamageable> _damageablePool;
        private AimLabDamageable _currentDamageable;

        private float _currentSpeed;
        private float _currentSize;

        private void Awake()
        {
            _damageablePool = new ObjectPool<AimLabDamageable>(
                CreateDamageable,
                OnGetDamageable,
                OnReleaseDamageable,
                OnDestroyDamageable,
                true,
                _defaultPoolSize,
                _maxPoolSize
            );
        }

        private void Start()
        {
            AimLabManager.OnGameModeSelected += OnGameModeSelected;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                SpawnDamageable();
        }

        private void OnDestroy()
        {
            AimLabManager.OnGameModeSelected -= OnGameModeSelected;
        }

        private void OnGameModeSelected(bool started, AimDifficulty difficulty)
        {
            if (!started)
                return;

            GameModeScriptableObject mode = GetCurrentModeData(difficulty);

            if (mode == null)
                return;

            _currentSpeed = mode.damageableSpeed;
            _currentSize = mode.damageableSize;

            SpawnDamageable();
        }

        private void SpawnDamageable()
        {
            // Return old object to pool
            if (_currentDamageable != null)
            {
                _damageablePool.Release(_currentDamageable);
                _currentDamageable = null;
            }

            // Get an object from pool
            _currentDamageable = _damageablePool.Get();

            // Configure it
            float randomX = Random.Range(-_horizontalRange, _horizontalRange);

            Vector3 position = _spawnPoint.position;
            position.x += randomX;

            _currentDamageable.transform.position = position;

            _currentDamageable.SetDamageableSize(_currentSize);
            _currentDamageable.SetDamageableSpeed(_currentSpeed);
            _currentDamageable.SetCanMove(true);
        }

        private AimLabDamageable CreateDamageable()
        {
            AimLabDamageable damageable = Instantiate(_damageablePrefab);
            damageable.gameObject.SetActive(false);

            return damageable;
        }

        private void OnGetDamageable(AimLabDamageable damageable)
        {
            damageable.gameObject.SetActive(true);
        }

        private void OnReleaseDamageable(AimLabDamageable damageable)
        {
            damageable.SetCanMove(false);
            damageable.gameObject.SetActive(false);
        }

        private void OnDestroyDamageable(AimLabDamageable damageable)
        {
            Destroy(damageable.gameObject);
        }

        public void ReleaseDamageable(AimLabDamageable damageable)
        {
            _damageablePool.Release(damageable);
        }

        private GameModeScriptableObject GetCurrentModeData(AimDifficulty difficulty)
        {
            return difficulty switch
            {
                AimDifficulty.Easy => _easyMode,
                AimDifficulty.Medium => _mediumMode,
                AimDifficulty.Hard => _hardMode,
                _ => null
            };
        }

        [Header("Mode Scriptable Objects")]
        [SerializeField] private GameModeScriptableObject _easyMode;
        [SerializeField] private GameModeScriptableObject _mediumMode;
        [SerializeField] private GameModeScriptableObject _hardMode;
    }
}