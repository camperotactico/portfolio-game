using Codice.Client.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformPivot : MonoBehaviour
{
    public float descendingSpeed = 8.0f;
    public float ascendingSpeed = 12.0f;
    public float lowestHeight = -4.0f;
    public float highestHeight = 0.0f;

    [SerializeField]
    private Rigidbody2D pivotRigidbody2D;

    private InputAction inputAction;
    private bool isPivotButtonPressed;

    private Vector2 nextPosition;

    private void Start()
    {
        nextPosition = pivotRigidbody2D.position;
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
            float distanceToLowestPoint = pivotRigidbody2D.position.y - lowestHeight;
            if (distanceToLowestPoint > 0.0f)
            {
                float heightDelta = fixedDeltaTime * descendingSpeed;
                heightDelta = Mathf.Min(heightDelta, distanceToLowestPoint);
                nextPosition = pivotRigidbody2D.position + heightDelta * Vector2.down;
                pivotRigidbody2D.MovePosition(nextPosition);
            }
        }
        else
        {
            float distanceToHighestPoint = highestHeight - pivotRigidbody2D.position.y;
            if (distanceToHighestPoint > 0.0f)
            {
                float heightDelta = fixedDeltaTime * ascendingSpeed;
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

