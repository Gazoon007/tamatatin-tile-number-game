using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        spriteRenderer.enabled = false;
    }

    void OnMouseEnter()
    {
        spriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        spriteRenderer.enabled = false;
    }
}
