using UnityEngine;

public class StaticInteractable : MonoBehaviour
{
    [SerializeField] GameObject instansDynamicObject = null;

    public DynamicInteractable Interact(Transform holdPoint)
    {
        if(instansDynamicObject != null)
        {
            DynamicInteractable dynamicInteractable = Instantiate(instansDynamicObject, holdPoint).GetComponent<DynamicInteractable>();
            dynamicInteractable.Pickup(holdPoint);
            return dynamicInteractable;
        }
        return null;
    }

    private void OnMouseDown()
    {
        
    }
}