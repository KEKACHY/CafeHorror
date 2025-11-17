using UnityEngine;

public class CoffeeMachine :  StaticInteractable
{
    private CoffeeCup _coffeeCup;
    [SerializeField] Transform cupPlace;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<DynamicInteractable>() is DynamicInteractable interactable)
        {
            if (interactable.TryGetComponent(out CoffeeCup cup))
            {
                if(_coffeeCup == null && !cup.IsHeld)
                {
                    _coffeeCup = cup;
                    cup.Pickup(cupPlace);
                }
                else if(_coffeeCup != null && cup.IsHeld)
                {
                    _coffeeCup = null;
                }   
            }
        }
    }

    public override void Interact(Transform holdPoint, out DynamicInteractable result)
    {
        result = null;
    }
}
