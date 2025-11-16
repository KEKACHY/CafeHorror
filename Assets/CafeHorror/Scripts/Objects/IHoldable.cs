using UnityEngine;

public interface IHoldable
{
    void Pickup(Transform holdPoint);
    void Drop();
    bool IsHeld { get; }
}