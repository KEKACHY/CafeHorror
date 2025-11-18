using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private LayerMask interactableLayer;

    private DynamicInteractable _current;
    private Camera _camera;
    [SerializeField] private Image interactImage;
    [SerializeField] private float defaultSize = 8f;
    [SerializeField] private float hoverSize = 12f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        UpdateInteractUI();
        UpdateInteract();
    }

    private void UpdateInteract()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_current == null)
                TryInteract();  
            else
                Throw();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if (_current != null)
                Drop();
        }
    }

    private void UpdateInteractUI()
    {
        interactImage.rectTransform.sizeDelta = new Vector2(defaultSize, defaultSize);
        interactImage.color = new Color(
            interactImage.color.r,
            interactImage.color.g,
            interactImage.color.b,
                0.2f);
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
        {
            interactImage.rectTransform.sizeDelta = new Vector2(hoverSize, hoverSize);
            interactImage.color = new Color(
                interactImage.color.r,
                interactImage.color.g,
                interactImage.color.b,
                    0.5f);
        }
    }

    private void TryInteract()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent(out DynamicInteractable dynamicObj) && !dynamicObj.IsHeld)
            {
                _current = dynamicObj;
                _current.Pickup(holdPoint);
            }
            else if (hit.collider.TryGetComponent(out StaticInteractable staticObj))
            {
                staticObj.Interact(holdPoint, out _current);
            }
        }
    }

    private void Throw()
    {
        Vector3 throwDir = _camera.transform.forward;
        _current.Throw(throwDir, throwForce);
        _current = null;
    }

    private void Drop()
    {
        _current.Drop();
        _current = null;
    }

}