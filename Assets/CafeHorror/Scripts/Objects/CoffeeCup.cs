using System;
using UnityEngine;

public class CoffeeCup : DynamicInteractable
{
    public event Action<bool> OnCoffeeIsFullChanged;
    private bool _coffeeIsFull = false;
    [SerializeField] private AttachPoint attachPoint;
    public SkinnedMeshRenderer Coffee;
    private void Start()
    {
        if (attachPoint != null)
            OnCoffeeIsFullChanged += attachPoint.OnCupFullStateChanged;
    }

    public bool CoffeeIsFull
    {
        get => _coffeeIsFull;
        set
        {
            if (_coffeeIsFull == value) return;
            _coffeeIsFull = value;
            OnCoffeeIsFullChanged?.Invoke(_coffeeIsFull);
        }
    }
}