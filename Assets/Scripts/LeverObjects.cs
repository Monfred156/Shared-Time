using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObjects : MonoBehaviour
{
    private GameObject _object;
    private Animator _animator;
    public bool isNormal;
    public GameObject toHide;
    public GameObject toHideAlternate;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_object)
            return;
        if ((_object.GetComponent<CharacterController2D>().playerNumber == 1 && Input.GetKeyDown(KeyCode.F)) ||
            (_object.GetComponent<CharacterController2D>().playerNumber == 2 &&
             Input.GetKeyDown(KeyCode.KeypadPlus)))
        {
            if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<CapsuleCollider2D>()))
            {
                _animator.SetBool("On", !_animator.GetBool("On"));
                if (isNormal)
                {
                    if (toHide)
                        toHide.SetActive(!toHide.activeSelf);
                }
                else
                {
                    if (toHideAlternate)
                        toHideAlternate.SetActive(!toHideAlternate.activeSelf);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _object = other.gameObject;
        }
    }
}