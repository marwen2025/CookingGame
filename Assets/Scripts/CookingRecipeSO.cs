using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CookingRecipeSO : ScriptableObject
{
    public KitchenObjectScripticalObject input;
    public KitchenObjectScripticalObject output;
    public float CookingProgressMax;
}
