using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLine : MonoBehaviour
{
    public int gridSize = 1;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public Color gridColor = Color.white;
    public float gridOffset = 5.5f;

    void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        // Draw vertical lines
        for (int x = 1; x <= gridWidth; x += gridSize)
        {
            Vector3 start = new Vector3(x - gridOffset, 0, 1 - gridOffset);
            Vector3 end = new Vector3(x - gridOffset, 0, gridHeight - gridOffset);
            Gizmos.DrawLine(start, end);
        }

        // Draw horizontal lines
        for (int z = 1; z <= gridHeight; z += gridSize)
        {
            Vector3 start = new Vector3(1 - gridOffset, 0, z - gridOffset);
            Vector3 end = new Vector3(gridWidth - gridOffset, 0, z - gridOffset);
            Gizmos.DrawLine(start, end);
        }
    }
}
