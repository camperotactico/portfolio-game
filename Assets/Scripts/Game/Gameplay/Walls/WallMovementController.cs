using System.Collections;
using UnityEngine;

public class WallMovementController: MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D topHalf;
    [SerializeField]
    private Rigidbody2D bottomHalf;

    private float currentVerticalPosition;
    private float currentGoalSize;
    private bool hasPositionChanged = false;

    private Coroutine updateHalvesPositionCoroutine;
    private Coroutine handleVerticalPositionCoroutine;
    private Coroutine handleGoalSizeCoroutine;


    private void Awake()
    {
        currentVerticalPosition = 0f;
        currentGoalSize = 0f;
        hasPositionChanged = true;
    }

    public void StartMovement(ICommandProvider<IWallMovementCommand> verticalPositionCommandProvider, ICommandProvider<IWallMovementCommand> goalSizeCommandProvider )
    {
        TryStopCoroutines();

        handleGoalSizeCoroutine = StartCoroutine(HandleWallMovementCommandProvider(goalSizeCommandProvider));
        handleVerticalPositionCoroutine = StartCoroutine(HandleWallMovementCommandProvider(verticalPositionCommandProvider));
        updateHalvesPositionCoroutine = StartCoroutine(UpdateHalvesPositions());
    }

    public void StopMovement()
    {
        TryStopCoroutines();
    }

    private void TryStopCoroutines()
    {
        if (updateHalvesPositionCoroutine != null)
        {
            StopCoroutine(updateHalvesPositionCoroutine);
            updateHalvesPositionCoroutine = null;
        }

        if (handleGoalSizeCoroutine != null)
        {
            StopCoroutine(handleGoalSizeCoroutine);
            handleGoalSizeCoroutine = null;
        }

        if (handleVerticalPositionCoroutine != null)
        {
            StopCoroutine(handleVerticalPositionCoroutine);
            handleVerticalPositionCoroutine = null;
        }
    }

    private IEnumerator HandleWallMovementCommandProvider(ICommandProvider<IWallMovementCommand> wallMovementCommandProvider)
    {
        while (wallMovementCommandProvider.TryGetNext(out IWallMovementCommand wallMovementCommand))
        {
            yield return wallMovementCommand.Execute(this);
        }
        yield return null;
    }


    public IEnumerator SetVerticalPosition(float newVerticalPosition, float duration = 0f)
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

    private IEnumerator UpdateHalvesPositions()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!hasPositionChanged)
            {
                continue;
            }
            Vector2 tophalfPosition = transform.TransformPoint(0f, currentVerticalPosition + (0.5f * currentGoalSize), 0f);
            Vector2 bottomHalfPosition = transform.TransformPoint(0f, currentVerticalPosition - (0.5f * currentGoalSize), 0f);
            topHalf.MovePosition(tophalfPosition);
            bottomHalf.MovePosition(bottomHalfPosition);
            hasPositionChanged = false;
        }

    }
}
