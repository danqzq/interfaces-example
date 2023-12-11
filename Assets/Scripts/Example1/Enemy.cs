using UnityEngine;

public class Enemy : MonoBehaviour
{
    // DEFINE THE TAKE DAMAGE METHOD HERE:
    
    
    
    // ----------------------------------------
    
    [field: SerializeField] public int Damage { get; private set; } = 25;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _attackSprite;
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sightRadius = 1.5f;
    [SerializeField] private float _spriteFloatHeight = 0.125f;
    
    [SerializeField] private Transform[] _patrolPoints;
    
    [SerializeField] private GameObject _upgradePickupPrefab;
    
    private int _health = 2;
    
    private int _currentPatrolPointIndex;
    private Transform _playerTransform;

    private void ChasePlayer()
    {
        var t = transform;
        var position = t.position;
        var direction = (_playerTransform.position - position).normalized;
        var velocity = direction * _speed;

        position += velocity * Time.deltaTime;
        t.position = position;

        if (direction.x != 0)
            transform.localScale = new Vector3(direction.x < 0 ? 1 : -1, 1, 1);
    }

    private void Wander()
    {
        var t = transform;
        var currentPatrolPoint = _patrolPoints[_currentPatrolPointIndex];

        var position = t.position;
        var direction = (currentPatrolPoint.position - position).normalized;
        var velocity = direction * _speed;
        
        position += velocity * Time.deltaTime;
        t.position = position;

        if (direction.x != 0)
            transform.localScale = new Vector3(direction.x < 0 ? 1 : -1, 1, 1);
        
        if (Vector3.Distance(t.position, currentPatrolPoint.position) < 0.1f)
        {
            _currentPatrolPointIndex = (_currentPatrolPointIndex + 1) % _patrolPoints.Length;
        }
    }
    
    private void Awake()
    {
        _currentPatrolPointIndex = 0;
    }
    
    private void Update()
    {
        _spriteRenderer.transform.localPosition = Vector3.up * (Mathf.Sin(Time.time * 5f) * _spriteFloatHeight);
        
        var col = Physics2D.OverlapCircle(transform.position, _sightRadius, LayerMask.GetMask("Player"));
        if (col != null)
        {
            _spriteRenderer.sprite = _attackSprite;
            _playerTransform = col.transform;
        }
        else
        {
            _spriteRenderer.sprite = _normalSprite;
            _playerTransform = null;
        }
        
        if (_playerTransform != null)
        {
            ChasePlayer();
            return;
        }
        
        Wander();
    }
    
    private void OnDeath()
    {
        Destroy(gameObject);
        if (_upgradePickupPrefab == null)
            return;
        Instantiate(_upgradePickupPrefab, transform.position, Quaternion.identity);
    }

    private void Highlight(Color color) => _spriteRenderer.color = color;
    
    private void ResetColor() => _spriteRenderer.color = Color.white;
}