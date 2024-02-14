using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CookingTable : BaseTable
{
    private float cookingProgress;
    private void Update()
    {
        if (HasKitchenObject())
        {
            cookingProgress += Time.deltaTime;
        }
    }

    [SerializeField] private CookingRecipeSO[] cookingRecipeSOArray;
    
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectScripticalObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cookingProgress = 0;
                    CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectScripticalObject());

                }
            }

            
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
    private CookingRecipeSO GetCookingRecipeSOWithInput(KitchenObjectScripticalObject inputKitchenObjectScripticalObject)
    {
        foreach(CookingRecipeSO cookingRecipeSO in cookingRecipeSOArray)
        {
            if (cookingRecipeSO.input == inputKitchenObjectScripticalObject)
            {
                return cookingRecipeSO;
            }
        }
        return null;
    }
    private KitchenObjectScripticalObject GetOutputForInput(KitchenObjectScripticalObject input)
    {
        CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(input);
        if (cookingRecipeSO !=null)
        {
            return cookingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private bool HasRecipieWithInput(KitchenObjectScripticalObject input)
    {
        CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(input);
        return cookingRecipeSO != null;
    }
}
