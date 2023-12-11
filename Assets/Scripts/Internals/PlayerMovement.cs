using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _falterDuration = 0.375f;
    
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private Transform _jumpCheck;
    [SerializeField] private LayerMask _groundLayer;
    
    private Rigidbody2D _rigidbody2D;

    private float _falterTimer;
    
    public void Knockback(Vector3 source)
    {
        _falterTimer = _falterDuration;
        _rigidbody2D.AddForce(Vector3.right * (transform.position.x < source.x ? -1 : 1) * _jumpForce +
                              (IsGrounded() ? Vector3.up * _jumpForce : Vector3.zero), ForceMode2D.Impulse);
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(_jumpCheck.position, _groundCheckRadius, _groundLayer);

    private void HandleMovement()
    {
        if (_falterTimer > 0)
        {
            _falterTimer -= Time.deltaTime;
            return;
        }
        
        var horizontal = Input.GetAxisRaw("Horizontal");
        
        var velocity = new Vector2(horizontal * _speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = velocity;
        
        if (horizontal != 0) 
            transform.localScale = new Vector3(horizontal > 0 ? 1 : -1, 1, 1);
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) 
            OnJump();
    }
    
    private void OnJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    
    private void ListenForRestart()
    {
        if (!Input.GetKeyDown(KeyCode.R)) 
            return;
        
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        HandleMovement();
        ListenForRestart();
    }
}
