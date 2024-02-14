using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAnimator : MonoBehaviour
{   
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerTable containerTable;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerTable.OnPlayerGrabObject += ContainerTable_OnPlayerGrabObject;
    }

    private void ContainerTable_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
