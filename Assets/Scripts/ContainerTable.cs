using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerTable : BaseTable
{
    [SerializeField] private KitchenObjectScripticalObject kitchenObjectScripticalObject;
    public event EventHandler OnPlayerGrabObject;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {   if (!player.HasKitchenObject()) {
                KitchenObject.spawnKitchenObject(kitchenObjectScripticalObject, player);
                OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
            }
            


        }
        

    }
    
}
