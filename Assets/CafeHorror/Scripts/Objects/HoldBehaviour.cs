using UnityEngine;

public class HoldBehaviour : MonoBehaviour, IHoldable
{
    [SerializeField] private float _moveSpeed = 12f;

    private Rigidbody _rb;
    private Transform _holdPoint;

    public bool IsHeld { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Pickup(Transform holdPoint)
    {
        _holdPoint = holdPoint;
        IsHeld = true;

        _rb.useGravity = false;
        _rb.isKinematic = true;

        transform.SetParent(null);
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;  
        IsHeld = false;
        _holdPoint = null;
    }

    private void FixedUpdate()
    {
        if (!IsHeld || _holdPoint == null)
            return;

        Vector3 targetPos = _holdPoint.position;
        Vector3 delta = targetPos - transform.position;

        _rb.MovePosition(transform.position + delta * _moveSpeed * Time.fixedDeltaTime);
    }
}