using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectScripticalObject input;
    public KitchenObjectScripticalObject output;
    public int cuttingProgressMax;
}
