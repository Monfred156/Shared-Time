using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaBehaviour : MonoBehaviour
{
    private GameObject _object;
    public GameObject _self;
    public GameObject portal;

    void Update()
    {
        if (!_object)
            return;
        if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<CapsuleCollider2D>()))
        {
            portal.GetComponent<PortalInteraction>().isOpen = true;
            portal.GetComponent<PortalInteraction>().SetPortalState(true);
            _self.SetActive(false);
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