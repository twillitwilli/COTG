using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuRaycast : MonoBehaviour
{
    [SerializeField] private GameObject _rayEffect;

    public LayerMask ignoreLayers;

    private float _range = 4;
    private ButtonHighlighted _selectedButton;

    public void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, _range, -ignoreLayers))
        {
            if (!_rayEffect.activeSelf) { _rayEffect.SetActive(true); }
            if (hit.collider.CompareTag("UITag") && hit.collider.GetComponent<ButtonHighlighted>())
            {
                if (_selectedButton != null && _selectedButton != hit.collider.GetComponent<ButtonHighlighted>())
                {
                    _selectedButton.menuRaycastHitting = null;
                    SelectNewButton(hit.collider.GetComponent<ButtonHighlighted>());
                    return;
                }
                else if (_selectedButton == null)
                {
                    SelectNewButton(hit.collider.GetComponent<ButtonHighlighted>());
                    return;
                }
            }
            return;
        }
        else
        {
            if (_rayEffect.activeSelf) { _rayEffect.SetActive(false); }
            if (_selectedButton != null)
            {
                _selectedButton.menuRaycastHitting = null;
                _selectedButton = null;
            }
        }
    }

    private void SelectNewButton(ButtonHighlighted newButton)
    {
        _selectedButton = newButton;
        _selectedButton.menuRaycastHitting = this;
    }

    public bool RayActive()
    {
        if (_rayEffect.activeSelf) { return true; }
        else return false;
    }

    public void ShootRaycast()
    {
        Debug.Log("shoot menu selector raycast");
        if (_selectedButton != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, _range, -ignoreLayers) && hit.collider.CompareTag("UITag"))
            {
                if (hit.collider.GetComponent<ButtonHighlighted>() != _selectedButton) { _selectedButton = hit.collider.GetComponent<ButtonHighlighted>(); }

                if (_selectedButton.GetComponent<Button>() == null) { Debug.Log("Button doesn't have a button component: " + _selectedButton); }
                else if (_selectedButton.GetComponent<Button>()) { _selectedButton.GetComponent<Button>().onClick.Invoke(); }
            }
        }
    }
}
