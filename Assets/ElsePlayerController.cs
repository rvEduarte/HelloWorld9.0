using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ElsePlayerController : MonoBehaviour, pIPlayerController
{
    private ElsePlayerAnimator playerAnimator;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private ElseScriptableStats _stats;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private ElseFrameInput _frameInput;
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;

    private bool _isJumping = true;
    private bool _isFliping = true;

    #region Interface

    public Vector2 FrameInput => _frameInput.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    #endregion

    private float _time;

    private void Awake()
    {
        playerAnimator = FindObjectOfType<ElsePlayerAnimator>();  // Assuming there's only one animator
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        _time += Time.unscaledDeltaTime;
        GatherInput();
    }

    private void GatherInput()
    {
        if (!TriggerTutorial.disableMove)
        {
            _frameInput = new ElseFrameInput();  // Zero out input when movement is disabled
            return;
        }

        if (!TriggerTutorial.disableJump)
        {
            //_frameInput.JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C);
        }

        if (_stats.SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }

        //////////////////////////////////

    }

    private void FixedUpdate()
    {
        if (!TriggerTutorial.disableMove)
        {
            _rb.velocity = Vector2.zero;  // Stop all movement
            return;
        }

        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();

        ElseController();
    }
    // UI Button Input Methods
    public void OnLeftButtonDown()
    {
        //Debug.Log("LeftButtonDown");
        _frameInput.Move.x = -1f;
    }

    public void OnRightButtonDown()
    {
        
        _frameInput.Move.x = 1f;
    }
    public void OnJumpButtonDown()
    {
        //Debug.Log("LeftButtonDown");
        _frameInput.JumpDown = true;
        _frameInput.JumpHeld = true;
        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    public void OnJumpButtonUp()
    {
        //Debug.Log("LeftButtonUP");
        _frameInput.JumpHeld = false;
    }

    public void OnLeftButtonUp()
    {
        //Debug.Log("LeftButtonUP");
        if (_frameInput.Move.x < 0) _frameInput.Move.x = 0;
    }

    public void OnRightButtonUp()
    {
        if (_frameInput.Move.x > 0) _frameInput.Move.x = 0;
    }

    public void OnFlipButtonDown()
    {
        playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

        sprite.flipX = !sprite.flipX;  // Flip the sprite only when the player is detected for the first time
    }


    private void ApplyMovement()
    {
        if (!TriggerTutorial.disableMove)
        {
            _rb.velocity = Vector2.zero;  // Ensure the velocity is zero if movement is disabled
        }
        else
        {
            _rb.velocity = _frameVelocity;
        }
    }

    //------------------------------------------------------ELSE CONTROLLER-----------------------------------------------------------------------------
    private void ElseController()
    {
        //---------------------------------------------------------LAST ROW--------------------------------------------------------------------------------//

        if (sprite.flipX == true && Row3ThirdSlotScript.Row3Walk == true) // MOVE LEFT
        {
            //Debug.Log("move left");
            OnLeftButtonDown();
        }
        else if(sprite.flipX == false && Row3ThirdSlotScript.Row3Walk == true) // MOVE RIGHT
        {
            OnRightButtonDown();
        }
        else if(Row3ThirdSlotScript.Row3Jump == true)
        {
            if (!_isJumping) return;
            StartCoroutine(HandleJumpCoroutine());
        }
        else if(Row3ThirdSlotScript.Row3Flip == true)
        {
            if (!_isFliping) return;

            StartCoroutine(HandleFlipCoroutine());
        }
    }

    // Coroutine for holding jump button
    private IEnumerator HandleJumpCoroutine()
    {
        _isJumping = false;  // Set jumping status to true

        OnJumpButtonDown();  // Simulate pressing the jump button
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        OnJumpButtonUp();  // Simulate releasing the jump button

        _isJumping = true;  // Set jumping status to false after finishing the jump
    }

    // Coroutine for holding Flip button
    private IEnumerator HandleFlipCoroutine()
    {
        _isFliping = false;  // Set jumping status to true

        OnFlipButtonDown();  // Simulate pressing the jump button
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        _isFliping = true;  // Set jumping status to false after finishing the jump
    }




    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

        // Hit a Ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    #endregion


    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0)
        {
            _endedJumpEarly = true;
        }

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;

        // Apply initial jump power
        _frameVelocity.y = _stats.InitialJumpPower;
        Jumped?.Invoke();

        Debug.Log("Jump executed. Starting coroutine for jump hold.");

        // Start the coroutine to extend the jump if the button is held
        StartCoroutine(ApplyHeldJump());
    }

    private IEnumerator ApplyHeldJump()
    {
        float elapsedTime = 0f;

        Debug.Log($"JumpHeld: {_frameInput.JumpHeld}");

        // Continue increasing jump force while jump button is held and time hasn't exceeded max jump hold time
        while (_frameInput.JumpHeld && elapsedTime < _stats.MaxJumpHoldTime)
        {
            Debug.Log("Extending jump...");
            _frameVelocity.y += _stats.HeldJumpPower * Time.fixedDeltaTime;
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Jump extension finished.");
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedUnscaledDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedUnscaledDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedUnscaledDeltaTime);
        }
    }

    #endregion


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}

public struct ElseFrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
}

public interface pIPlayerController
{
    public event Action<bool, float> GroundedChanged;

    public event Action Jumped;
    public Vector2 FrameInput { get; }
}
