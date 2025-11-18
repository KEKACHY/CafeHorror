using UnityEngine;
using System;

[RequireComponent(typeof(HoldBehaviour))]
[RequireComponent(typeof(AttachBehaviour))]
public class DynamicInteractable : MonoBehaviour
{
    protected IHoldable _holdable;
    protected IAttachable _attachable;

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

    public virtual void Drop()
    {
        _holdable.Drop();
    }
}
