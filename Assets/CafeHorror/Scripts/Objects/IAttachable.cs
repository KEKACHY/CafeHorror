using UnityEngine;

public interface IAttachable
{
    void TryAttach(Transform attachPoint);
    void Detach();
    bool IsAttached { get; }
}