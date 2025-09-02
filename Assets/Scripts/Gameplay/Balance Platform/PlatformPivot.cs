using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformPivot : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField]
    private Rigidbody2D pivotRigidbody2D;
    [SerializeField]
    private SpriteRenderer pivotPointSpriteRenderer;
    [SerializeField]
    private SpriteRenderer pivotRailSpriteRenderer;


    private InputAction inputAction;
    private bool isPivotButtonPressed;


    private Vector2 nextPosition;
    private PlatformPivotParametes platformPivotParameters;
    private float globalLowestHeight;
    private float globalHighestHeight;

    private void Start()
    {
        nextPosition = pivotRigidbody2D.position;
    }

    public void SetParameters(PlatformPivotParametes newPlatformPivotParametes, Vector3 directionFromCenter)
    {
        platformPivotParameters = newPlatformPivotParametes;
        RepositionLimits(directionFromCenter);
    }

    private void RepositionLimits(Vector3 directionFromCenter)
    {
        transform.localPosition = platformPivotParameters.DistanceFromCenter * directionFromCenter;
        globalLowestHeight = transform.TransformPoint(platformPivotParameters.Range * Vector3.down).y;
        globalHighestHeight = transform.TransformPoint(Vector3.zero).y;

        float pivotPointDiameter = pivotPointSpriteRenderer.size.y;
        float range = platformPivotParameters.Range;

        Vector2 railSize = pivotRailSpriteRenderer.size;
        railSize.y = range + pivotPointDiameter;
        pivotRailSpriteRenderer.size = railSize;

        Vector3 railLocalPosition = pivotRailSpriteRenderer.transform.localPosition;
        railLocalPosition.y = -0.5f * range;
        pivotRailSpriteRenderer.transform.localPosition = railLocalPosition;
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
            float distanceToLowestPoint = pivotRigidbody2D.position.y - globalLowestHeight;
            if (distanceToLowestPoint > 0.0f)
            {
                float heightDelta = fixedDeltaTime * platformPivotParameters.DescendingSpeed;
                heightDelta = Mathf.Min(heightDelta, distanceToLowestPoint);
                nextPosition = pivotRigidbody2D.position + heightDelta * Vector2.down;
                pivotRigidbody2D.MovePosition(nextPosition);
            }
        }
        else
        {
            float distanceToHighestPoint = globalHighestHeight - pivotRigidbody2D.position.y;
            if (distanceToHighestPoint > 0.0f)
            {
                float heightDelta = fixedDeltaTime * platformPivotParameters.AscendingSpeed;
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

