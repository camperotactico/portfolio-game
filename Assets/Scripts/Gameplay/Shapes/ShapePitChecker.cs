using System;
using UnityEngine;

public class ShapesPitChecker : MonoBehaviour
{
    public Action Fell;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != Layers.ShapesPit)
        {
            return;
        }
        Fell?.Invoke();
    }
}

