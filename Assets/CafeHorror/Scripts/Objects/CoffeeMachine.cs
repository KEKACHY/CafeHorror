using System.Collections;
using UnityEngine;

public class CoffeeMachine :  StaticInteractable
{
    private CoffeeCup _coffeeCup;
    private bool _isFilling = false;
    [SerializeField] private Transform cupPlace;
    [SerializeField] private GameObject coffee;
    [SerializeField] [Range(1f, 20f)] private float timeDuration = 5f;
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
        if(_coffeeCup != null && !_coffeeCup.CoffeeIsFull && !_isFilling)
        {
            StartCoroutine(FillCupRoutine());
        }
    }
    
    private IEnumerator FillCupRoutine()
    {
        _isFilling = true;
        float timer = 0f;
        coffee.SetActive(true);
        _coffeeCup.Coffee.gameObject.SetActive(true);

        while (timer < timeDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Lerp(0f, 100f, timer / timeDuration);
            _coffeeCup.Coffee.SetBlendShapeWeight(_coffeeCup.Coffee.sharedMesh.GetBlendShapeIndex("FillCoffee"), t);

            yield return null;
        }
        _coffeeCup.Coffee.SetBlendShapeWeight(_coffeeCup.Coffee.sharedMesh.GetBlendShapeIndex("FillCoffee"), 100f);
        _coffeeCup.CoffeeIsFull = true;
        _coffeeCup.Drop();
        _coffeeCup = null;
        _isFilling = false;
        coffee.SetActive(false);
    }
}
