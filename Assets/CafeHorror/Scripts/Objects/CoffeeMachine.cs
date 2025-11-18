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
                if(_coffeeCup == null && !cup.IsHeld && !cup.CoffeeIsFull)
                {
                    _coffeeCup = cup;
                    cup.Pickup(cupPlace);
                } 
            }
        }
    }

    public override void Interact(Transform holdPoint, out DynamicInteractable result)
    {
        result = null;
        if(_coffeeCup != null && !_coffeeCup.CoffeeIsFull)
        {
            _coffeeCup.CoffeeIsFull = true;
            _coffeeCup.Drop();
            _coffeeCup = null;
        }
    }
}
