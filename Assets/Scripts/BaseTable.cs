using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTable : MonoBehaviour, IkitchenObjectParent
{
    [SerializeField] private Transform topTawla;
    protected KitchenObject kitchenObject;


    public virtual void Interact(Player player)
    {
        Debug.Log("Base counter interact");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("Base counter interactAlternate");
    }



    public Transform GetKitchenObjectFollowTransform()
    {
        return topTawla;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
