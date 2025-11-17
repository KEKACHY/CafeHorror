using UnityEngine;
using System;

[RequireComponent(typeof(HoldBehaviour))]
[RequireComponent(typeof(AttachBehaviour))]
public class DynamicInteractable : MonoBehaviour
{
    private IHoldable _holdable;
    private IAttachable _attachable;

    private Transform _pendingAttachPoint;
    [HideInInspector] public bool IsHeld => CheckIfHeld();

    private void Awake()
    {
        _holdable = GetComponent<IHoldable>();
        _attachable = GetComponent<IAttachable>();
    }

    private bool CheckIfHeld()
    {
        if (_holdable == null)
            return false;

        return _holdable.IsHeld;
    }

    public void Pickup(Transform holdPoint)
    {
        _attachable.Detach();  
        _holdable.Pickup(holdPoint);
    }

    public void Throw(Vector3 direction, float force)
    {
        Drop(); 
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    public void Drop()
    {
        _holdable.Drop();
        _attachable.TryAttach(_pendingAttachPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_holdable.IsHeld)
            return;

        if (other.TryGetComponent(out AttachPoint attachPoint))
        {
            _pendingAttachPoint = attachPoint.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AttachPoint attachPoint)
            && _pendingAttachPoint == attachPoint.transform)
        {
            _pendingAttachPoint = null;
        }
    }
}
