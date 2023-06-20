
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Rigidbody2D _rb;
    private MainInput _input;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private bool _isLeft;

    private void Awake()
    {
        _input = new MainInput();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        
        LevelManager.Instance.OnLevelStarted += OnLevelStartedHandler;
        LevelManager.Instance.OnLevelEnded += OnLevelEndedHandler;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
        LevelManager.Instance.OnLevelStarted -= OnLevelStartedHandler;
        LevelManager.Instance.OnLevelEnded -= OnLevelEndedHandler;
    }

    private void Update()
    {
        var inputX = _input.Player.Move.ReadValue<float>();

        if (inputX != 0)
        {
            _animator.SetBool("IsMoving", true);
            FlipSprite(inputX);
        }
        else
            _animator.SetBool("IsMoving", false);
        
        _rb.velocity = new Vector2(inputX * _speed, _rb.velocity.y);
    }

    private void OnLevelStartedHandler() => _input.Disable();
    private void OnLevelEndedHandler() => _input.Enable();

    private void FlipSprite(float movementDirection)
    {
        if (movementDirection > 0 && _isLeft || movementDirection < 0 && !_isLeft)
        {
            _sprite.flipX = !_sprite.flipX;
            _isLeft = !_isLeft;
        }
    }
}
