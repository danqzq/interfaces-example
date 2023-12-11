using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    
    private PlayerHealth _playerHealth;
    private PlayerArmor _playerArmor;
    
    private PlayerMovement _playerMovement;
    
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerArmor = GetComponent<PlayerArmor>();
        
        _playerMovement = GetComponent<PlayerMovement>();
        
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Upgrade"))
        {
            _upgradeManager.ToggleUpgradeMenu(true);
            Destroy(other.gameObject);
            return;
        }
        
        if (!other.collider.TryGetComponent(out Enemy enemy)) 
            return;
        
        var enemyDamage = enemy.Damage;
        _playerMovement.Knockback(enemy.transform.position);
        
        if (_playerArmor.Armor > 0)
        {
            _playerArmor.Armor -= enemyDamage;
        }
        else
        {
            _playerHealth.Health -= enemyDamage;
        }

        if (_playerHealth.Health > 0) return;
        OnDeath();
    }

    private void OnDeath()
    {
        _collider2D.enabled = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddTorque(-100f);
    }
}