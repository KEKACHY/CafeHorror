using UnityEngine;

public class StaticInteractable : MonoBehaviour
{
    [SerializeField] GameObject instansDynamicObject = null;

    public virtual void Interact(Transform holdPoint, out DynamicInteractable result)
    {
        result = null;
        if(instansDynamicObject != null)
        {
            DynamicInteractable dynamicInteractable = Instantiate(instansDynamicObject, holdPoint).GetComponent<DynamicInteractable>();
            dynamicInteractable.Pickup(holdPoint);
            result = dynamicInteractable;
        }
    }
}