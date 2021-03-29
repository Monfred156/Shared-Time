using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
    public GameObject uICanvasPlayer1;
    public GameObject uICanvasPlayer2;
    public string textToDisplay;
    
    private GameObject _player;
    private bool _isHover;

    private TextMeshProUGUI _textPlayer1;
    private TextMeshProUGUI _textPlayer2;

    private void Awake()
    {
        _textPlayer1 = uICanvasPlayer1.GetComponent<TextMeshProUGUI>();
        _textPlayer2 = uICanvasPlayer2.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Vector3 transPos = transform.position;
        transPos.y += 0.8f;
        if (uICanvasPlayer1.activeSelf && _isHover && _player && _player.GetComponent<CharacterController2D>().GetPlayerNumber() == 1)
        {
            Vector3 pos = _player.GetComponentInChildren<Camera>().WorldToScreenPoint(transPos);
            uICanvasPlayer1.transform.position = pos;
        }
        else if (uICanvasPlayer2.activeSelf && _isHover && _player && _player.GetComponent<CharacterController2D>().GetPlayerNumber() == 2)
        {
            Vector3 pos = _player.GetComponentInChildren<Camera>().WorldToScreenPoint(transPos);
            uICanvasPlayer2.transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        _player = other.gameObject;
        _isHover = true;
        if (_player.GetComponent<CharacterController2D>().GetPlayerNumber() == 1)
        {
            _textPlayer1.text = textToDisplay;
            uICanvasPlayer1.SetActive(true);
        }
        else if (_player.GetComponent<CharacterController2D>().GetPlayerNumber() == 2)
        {
            _textPlayer2.text = textToDisplay;
            uICanvasPlayer2.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (_player.GetComponent<CharacterController2D>().GetPlayerNumber() == 1)
            uICanvasPlayer1.SetActive(false);
        else
            uICanvasPlayer2.SetActive(false);
        _isHover = false;
    }
}