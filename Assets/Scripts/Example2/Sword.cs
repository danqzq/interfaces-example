using UnityEngine;

public class Sword : MonoBehaviour, IUpgradable
{
    [field: SerializeField] public int Damage { get; private set; } = 1;
    
    [SerializeField] private Sprite _swordIcon;
    [SerializeField] private PlayerAttack _playerAttack;
    
    private Animator _animator;
    private Collider2D _collider;
    
    private static readonly int AttackTrigger = Animator.StringToHash("attack");

    public UpgradableInfo GetInfo() => new UpgradableInfo
    { 
        name = "Sword",
        description = "Increases the player's sword damage",
        icon = _swordIcon
    };
    
    public void OnUpgrade()
    {
        Damage++;
    }
    
    // This method is called by Animation Events
    public void ToggleCollider(int colliderEnabled)
    {
        _collider.enabled = colliderEnabled == 1;
    }

    private void Attack()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerAttack.OnAttack(other, Damage);
    }
}