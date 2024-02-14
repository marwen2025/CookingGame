using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScripticalObject kitchenObjectScripticalObject;

    private IkitchenObjectParent kitchenObjectParent;

    public KitchenObjectScripticalObject GetKitchenObjectScripticalObject()
    {
        return kitchenObjectScripticalObject;
    }
    public void SetKitchenObjectParent(IkitchenObjectParent kitchenObjectParent)
    {   
        
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("This kitchenObjectParent contain object");
        }

        kitchenObjectParent.SetKitchenObject(this);


        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IkitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject() ;
        Destroy(gameObject);
    }
    public static KitchenObject spawnKitchenObject(KitchenObjectScripticalObject kitchenObjectScripticalObject,IkitchenObjectParent ikitchenObjectParent)
    {
        Transform kitchenObjectScripticalObjectTransform = Instantiate(kitchenObjectScripticalObject.prefab);
        KitchenObject kitchenObject=kitchenObjectScripticalObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(ikitchenObjectParent);

        return kitchenObject;
    }
}
