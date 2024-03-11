using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public GameObject gridPrefab; // Assign in inspector
    public int width = 9;
    public int height = 7;
    public float spacing = 1.05f; // Adjust so there's no overlap

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Calculate position with appropriate spacing
                Vector3 position = new Vector3(x * spacing, 0, y * spacing);
                Instantiate(gridPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
