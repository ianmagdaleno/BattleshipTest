using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;

    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode previousNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;

        CheckCollisions();
    }
    private void CheckCollisions()
    {
        Vector3 worldPosition = grid.GetWorldPosition(x, y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, .3f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Collider"))
            {
                isWalkable = false; 
                break; 
            }
        }
    }

    public void CalculateFCost()
    {
       fCost = gCost + hCost;
    }
}
