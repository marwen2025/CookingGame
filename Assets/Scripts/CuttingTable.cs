using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : BaseTable
{
    private int cuttingProgress;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectScripticalObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectScripticalObject());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs()
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
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
    private bool HasRecipieWithInput(KitchenObjectScripticalObject inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;

    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipieWithInput(GetKitchenObject().GetKitchenObjectScripticalObject()))
        {

            cuttingProgress++;
            OnCut?.Invoke(this,EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectScripticalObject());
            
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs()
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)

            {

                KitchenObjectScripticalObject outputkitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectScripticalObject());
                GetKitchenObject().DestroySelf();
                KitchenObject.spawnKitchenObject(outputkitchenObjectSO, this);
                
            }


        }
    }
    private KitchenObjectScripticalObject GetOutputForInput(KitchenObjectScripticalObject inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectScripticalObject inputKitchenObjectScripticalObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectScripticalObject)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
