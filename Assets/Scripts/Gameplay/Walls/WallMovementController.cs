using System.Collections;
using UnityEngine;

public class WallMovementController
{
    private Transform transform;
    private Rigidbody2D topHalf;
    private Rigidbody2D bottomHalf;

    private float currentVerticalPosition;
    private float currentGoalSize;
    private bool hasPositionChanged = false;

    public WallMovementController(Transform newTransform, Rigidbody2D newTopHalf, Rigidbody2D newBottomHalf)
    {
        transform = newTransform;
        topHalf = newTopHalf;
        bottomHalf = newBottomHalf;
        currentVerticalPosition = 0f;
        currentGoalSize = 0f;
        hasPositionChanged = true;
    }

    public IEnumerator MoveTo(float newVerticalPosition, float duration = 0f)
    {
        float startVerticalPosition = currentVerticalPosition;
        float timer = 0f;

        while (timer < duration)
        {
            yield return new WaitForFixedUpdate();
            timer = Mathf.Min(timer +Time.fixedDeltaTime, duration);
            currentVerticalPosition = Mathf.Lerp(startVerticalPosition, newVerticalPosition, timer / duration);
            hasPositionChanged = true;

        }
        currentVerticalPosition =  newVerticalPosition;
        hasPositionChanged = true;

        yield return null;

    }

    public IEnumerator SetGoalSize(float newGoalSize, float duration = 0f)
    {
        float startGoalSize = currentGoalSize;
        float timer = 0f;

        while (timer < duration)
        {
            yield return new WaitForFixedUpdate();
            timer = Mathf.Min(timer + Time.fixedDeltaTime, duration);
            currentGoalSize = Mathf.Lerp(startGoalSize, newGoalSize, timer / duration);
            hasPositionChanged = true;

        }
        currentGoalSize = newGoalSize;
        hasPositionChanged = true;
        yield return null;

    }

    public void UpdateWallPosition()
    {
        if (!hasPositionChanged)
        {
            return;
        }

        Vector2 tophalfPosition = transform.TransformPoint(0f, currentVerticalPosition + (0.5f* currentGoalSize), 0f);
        Vector2 bottomHalfPosition = transform.TransformPoint(0f, currentVerticalPosition - (0.5f* currentGoalSize), 0f);
        topHalf.MovePosition(tophalfPosition);
        bottomHalf.MovePosition(bottomHalfPosition);
        hasPositionChanged = false;
    }
}
