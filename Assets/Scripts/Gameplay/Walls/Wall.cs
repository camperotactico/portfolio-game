using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D topHalf;
    [SerializeField]
    private Rigidbody2D bottomHalf;

    // TODO: Create a WallVerticalPositionProvider
    // TODO: Create a WallGoalSizeProvider
    private WallMovementController movementController;


    private void Awake()
    {
        movementController = new WallMovementController(transform, topHalf, bottomHalf);
    }

    private void Start()
    {
        // TODO: Replace this with two different routines, one for the vertical position that uses the WallVerticalPositionProvider and another for the goal size that uses WallGoalSizeProvider
        StartCoroutine(PingPong());
    }

    IEnumerator PingPong()
    {
        float targetVerticalPosition = 5f;
        yield return movementController.SetGoalSize(8f, 2f);
        while (true)
        {
            yield return movementController.MoveTo(targetVerticalPosition, 5f);
            targetVerticalPosition *= -1;
        }
    }

    private void FixedUpdate()
    {
        movementController.UpdateWallPosition();
    }
}
