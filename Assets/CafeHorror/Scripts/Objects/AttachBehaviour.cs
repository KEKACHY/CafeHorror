using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttachBehaviour : MonoBehaviour, IAttachable
{
    private Rigidbody _rb;

    public bool IsAttached { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void TryAttach(Transform attachPoint)
    {
        if (attachPoint == null || IsAttached)
            return;

        AttachInternal(attachPoint);
    }

    private void AttachInternal(Transform parent)
    {
        IsAttached = true;

        _rb.useGravity = false;
        _rb.isKinematic = true;

        transform.SetParent(parent, worldPositionStays: false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Collider lidCol = GetComponent<Collider>();
        if (lidCol != null)
            lidCol.enabled = false;
    }

    public void Detach()
    {
        if (!IsAttached)
            return;

        IsAttached = false;

        _rb.isKinematic = false;
        _rb.useGravity = true;

        transform.SetParent(null);
    }
}