using UnityEngine;
using System;

public class CoffeeCap : DynamicInteractable
{
    private Transform _pendingAttachPoint;
    public override void Drop()
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
        if (!_holdable.IsHeld)
            return;

        if (other.TryGetComponent(out AttachPoint attachPoint)
            && _pendingAttachPoint == attachPoint.transform)
        {
            _pendingAttachPoint = null;
        }
    }
}
