using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : BaseTable,IkitchenObjectParent
{

    public static Player Instance { get; private set; }


    public event EventHandler<OnSelectedTableChangedEventArgs> OnSelectedTableChanged;

    public class OnSelectedTableChangedEventArgs : EventArgs
    {
        public BaseTable selectedTable;
    }
    [SerializeField] private float moveSpd = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask tableLayerMask;
    [SerializeField] private Transform playerHand;
    private BaseTable selectedTable;
    private bool isWalking;
    

    private Vector3 lastInteractDir;
    // Start is called before the first frame update
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnIneractAlternateAction += GameInput_OnIneractAlternateAction;
    }

    private void GameInput_OnIneractAlternateAction(object sender, EventArgs e)
    {
        if (selectedTable != null)
        {
            selectedTable.InteractAlternate(this);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there is more than one Player");
        }
        Instance = this;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {   if (selectedTable != null)
        {
            selectedTable.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
        const float interactDistance = 2f;
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero )
        {
            lastInteractDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, tableLayerMask))
        {
          if( raycastHit.transform.TryGetComponent(out BaseTable baseCounter))
            {
            if(baseCounter != selectedTable)
                {
                    SetSelectedTable(baseCounter);
                }

            }
            else
            {
                SetSelectedTable(null);
            }
        }
        else
        {
            SetSelectedTable(null);
        }
    }
    
    //Handle player movement
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        float moveDistance = moveSpd * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;


        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirx, moveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            }
            else
            {
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirz, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirz;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpd =15f;
        transform.forward = Vector3.Slerp(transform.forward, -moveDir, Time.deltaTime * rotateSpd);
    }
    private void SetSelectedTable(BaseTable selectedTable)
    {   this.selectedTable = selectedTable;

        OnSelectedTableChanged?.Invoke(this, new OnSelectedTableChangedEventArgs
        {
            selectedTable = selectedTable
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return playerHand;
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
