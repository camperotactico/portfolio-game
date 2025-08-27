using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D topHalf;
    [SerializeField]
    private Rigidbody2D bottomHalf;


    private float currentVerticalPosition;
    private float currentGoalSize;

    private bool hasPositionChanged = false;
    private Coroutine moveVerticalPositionCoroutine;
    private Coroutine setGoalSizeCoroutine;


    private void Awake()
    {
        currentVerticalPosition = 0f;
        currentGoalSize = 0f;
    }

    private void Start()
    {
        UpdateWallPosition();
        MoveTo(2f, 0.5f);
        SetGoalSize(6f, 3f);
    }

    private void FixedUpdate()
    {
        if (!hasPositionChanged)
        {
            return;
        }
        UpdateWallPosition();
        hasPositionChanged = false;
    }

    private void MoveTo(float newVerticalPosition, float duration = 0f)
    {
        if (moveVerticalPositionCoroutine != null)
        {
            StopCoroutine(moveVerticalPositionCoroutine);
        }

        moveVerticalPositionCoroutine = StartCoroutine(HandleVerticalPosition(newVerticalPosition,duration));
    }

    private IEnumerator HandleVerticalPosition(float newVerticalPosition, float duration = 0f)
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

        moveVerticalPositionCoroutine = null;
        yield return null;

    }

    private void SetGoalSize(float newGoalSize, float duration = 0f)
    {
        if (setGoalSizeCoroutine != null)
        {
            StopCoroutine(setGoalSizeCoroutine);
        }

        setGoalSizeCoroutine = StartCoroutine(HandleGoalSize(newGoalSize, duration));
    }

    private IEnumerator HandleGoalSize(float newGoalSize, float duration = 0f)
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

        setGoalSizeCoroutine = null;
        yield return null;

    }

    private void UpdateWallPosition()
    {
        Vector2 tophalfPosition = transform.TransformPoint(0f, currentVerticalPosition + (0.5f* currentGoalSize), 0f);
        Vector2 bottomHalfPosition = transform.TransformPoint(0f, currentVerticalPosition - (0.5f* currentGoalSize), 0f);
        topHalf.MovePosition(tophalfPosition);
        bottomHalf.MovePosition(bottomHalfPosition);
    }
}
