using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTable : BaseTable
{


    

    
    public override void Interact(Player player)
    {
       if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
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

}
