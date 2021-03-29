using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalInteraction : MonoBehaviour
{
    private GameObject _object;
    private Animator _animator;
    public GameObject linkedPortal;
    public GameObject layer;
    private bool _isCompleted = false;
    public bool isOpen;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_object)
            return;
        int playerNb = _object.GetComponent<CharacterController2D>().playerNumber;
        if ((playerNb == 1 && Input.GetKeyDown(KeyCode.F)) ||
            (playerNb == 2 && Input.GetKeyDown(KeyCode.KeypadPlus)))
        {
            if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<CapsuleCollider2D>()) && isOpen)
            {
                _isCompleted = true;
                if (_isCompleted && linkedPortal.GetComponent<PortalInteraction>().IsCompleted())
                    SceneManager.LoadScene("MainMenu");
                else
                {
                    layer.SetActive(true);
                    _object.GetComponent<CharacterController2D>().SetState(PlayerState.FROZEN);
                }

            }
        }
    }

    public void SetPortalState(bool state)
    {
        _animator.SetBool("isOpen", state);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _object = other.gameObject;
        }
    }

    public bool IsCompleted()
    {
        return _isCompleted;
    }
}