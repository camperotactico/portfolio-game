using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformPivot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Rigidbody2D pivotRigidbody2D;


    private PlatformPivotParametes platformPivotParametes;
    private InputAction inputAction;
    private bool isPivotButtonPressed;

    private Vector2 nextPosition;

    private void Start()
    {
        nextPosition = pivotRigidbody2D.position;
    }

    public void SetParameters(PlatformPivotParametes newPlatformPivotParametes)
    {
        platformPivotParametes = newPlatformPivotParametes;
    }

    public void SetInputAction(InputAction newInputAction)
    {
        inputAction = newInputAction;
        inputAction.performed += OnActionPerformed;
        inputAction.canceled += OnActionCancelled;
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        isPivotButtonPressed = true;
    }

    private void OnActionCancelled(InputAction.CallbackContext context)
    {
        isPivotButtonPressed = false;
    }


    public void UpdatePosition(float fixedDeltaTime)
    {
        if (isPivotButtonPressed)
        {
            float distanceToLowestPoint = pivotRigidbody2D.position.y - platformPivotParametes.LowestHeight;
            if (distanceToLowestPoint > 0.0f)
            {
                float heightDelta = fixedDeltaTime * platformPivotParametes.DescendingSpeed;
                heightDelta = Mathf.Min(heightDelta, distanceToLowestPoint);
                nextPosition = pivotRigidbody2D.position + heightDelta * Vector2.down;
                pivotRigidbody2D.MovePosition(nextPosition);
            }
        }
        else
        {
            float distanceToHighestPoint = platformPivotParametes.HighestHeight - pivotRigidbody2D.position.y;
            if (distanceToHighestPoint > 0.0f)
            {
                float heightDelta = fixedDeltaTime * platformPivotParametes.AscendingSpeed;
                heightDelta = Mathf.Min(heightDelta, distanceToHighestPoint);
                nextPosition = pivotRigidbody2D.position + heightDelta * Vector2.up;
                pivotRigidbody2D.MovePosition(nextPosition);
            }
        }
    }

    public Vector2 NextPosition
    {
        get
        {
            return nextPosition;
        }
    }
}

