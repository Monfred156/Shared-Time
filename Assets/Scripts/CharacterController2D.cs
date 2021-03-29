using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum PlayerState
{
    FROZEN,
    FREE
}

public class CharacterController2D : MonoBehaviour
{
    public int playerNumber = 0;

    private float _speed = 1.5f;
    private float _savespeed;
    private const float RunSpeed = 3.5f;

    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private LayerMask groundLayer;

    private float _jumpForce = 5f;
    private bool _isGrounded = false;
    private bool _isClimbing;

    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private PlayerState _state = PlayerState.FREE;

    private void Awake()
    {
        _savespeed = _speed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_state == PlayerState.FROZEN)
            return;
        float horizontal = 0;
        if (playerNumber == 1)
        {
            horizontal = Input.GetAxis("Horizontal");

            transform.Translate(horizontal * _speed * Time.deltaTime, 0f, 0f);
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetBool("Run", true);
                _speed = RunSpeed;
            }
            else
            {
                _animator.SetBool("Run", false);
                _speed = _savespeed;
            }

            if (_isClimbing)
            {
                _rigidbody2D.velocity = Vector3.zero;
                _rigidbody2D.gravityScale = 0f;
                if (Input.GetKey(KeyCode.Z))
                {
                    _animator.speed = 1;
                    var vertical = Input.GetAxis("Vertical");

                    _animator.SetBool("Climbing", true);
                    transform.Translate(0f, vertical * _speed * Time.deltaTime, 0f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    _animator.speed = 1;
                    var vertical = Input.GetAxis("Vertical");

                    _animator.SetBool("Climbing", true);
                    transform.Translate(0f, vertical * _speed * Time.deltaTime, 0f);
                }
                else
                {
                    _animator.speed = 0;
                }
            }
            else
            {
                _rigidbody2D.gravityScale = 1f;
                _animator.speed = 1;
            }
        }
        else if (playerNumber == 2)
        {
            horizontal = Input.GetAxis("Horizontal2");
            transform.Translate(horizontal * _speed * Time.deltaTime, 0f, 0f);
            if (Input.GetKeyDown(KeyCode.Keypad0) && _isGrounded)
            {
                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.RightControl))
            {
                _animator.SetBool("Run", true);
                _speed = RunSpeed;
            }
            else
            {
                _animator.SetBool("Run", false);
                _speed = _savespeed;
            }

            if (_isClimbing)
            {
                _rigidbody2D.velocity = Vector3.zero;
                _rigidbody2D.gravityScale = 0f;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    _animator.speed = 1;
                    var vertical = Input.GetAxis("Vertical2");

                    _animator.SetBool("Climbing", true);
                    transform.Translate(0f, vertical * _speed * Time.deltaTime, 0f);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    _animator.speed = 1;
                    var vertical = Input.GetAxis("Vertical2");

                    _animator.SetBool("Climbing", true);
                    transform.Translate(0f, vertical * _speed * Time.deltaTime, 0f);
                }
                else
                {
                    _animator.speed = 0;
                }
            }
            else
            {
                _rigidbody2D.gravityScale = 1f;
                _animator.speed = 1;
            }
        }

        // bloque la position maximale en X Y (variables à mettre en dur ou en param public)
        // transform.position = new Vector2(
        // Mathf.Clamp(transform.position.x, -10f, 10f),
        // Mathf.Clamp(transform.position.y, -10f, 10f));
        GroundCheck();
        PlayerAnimatorHandler(horizontal);
    }

    private void PlayerAnimatorHandler(float horizontal)
    {
        if (_isGrounded && playerNumber == 1 && Input.GetKeyDown("space"))
            _animator.SetBool("Jump", true);
        else if (_isGrounded && playerNumber == 2 && Input.GetKeyDown(KeyCode.UpArrow))
            _animator.SetBool("Jump", true);

        if (_isGrounded)
            _animator.SetBool("Jump", false);
        else if (!_isClimbing)
            _animator.SetBool("Jump", true);

        if (horizontal != 0)
        {
            if (_isGrounded && !_isClimbing)
                _animator.SetBool("Walk", true);
            if (horizontal > 0)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }
        else
            _animator.SetBool("Walk", false);

        if (!_isClimbing)
            _animator.SetBool("Climbing", false);
    }

    private void GroundCheck()
    {
        _isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.1f, groundLayer);
        if (colliders.Length > 0)
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Vine"))
        {
            if (playerNumber == 1 && Input.GetKey(KeyCode.Z))
            {
                _isClimbing = true;
            }
            else if (playerNumber == 2 && Input.GetKey(KeyCode.UpArrow))
            {
                _isClimbing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Vine"))
        {
            _isClimbing = false;
        }
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void SetState(PlayerState state)
    {
        _state = state;
    }
}