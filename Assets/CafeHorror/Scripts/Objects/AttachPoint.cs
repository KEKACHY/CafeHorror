using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    [SerializeField] private GameObject ToolTip;
    private Collider _collider;

    public void OnCupFullStateChanged(bool isFull)
    {
        if (isFull)
        {
            EnablePoint();
        }
        else
        {
            DisablePoint();
        }
    }

    public void HideAttachPoint()
    {
       DisablePoint();
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        ToolTip.SetActive(false);
    }
    private void EnablePoint()
    {
        _collider.enabled = true;
        ToolTip.SetActive(true);
    }

    private void DisablePoint()
    {
        _collider.enabled = false;
        ToolTip.SetActive(false);
    }
}