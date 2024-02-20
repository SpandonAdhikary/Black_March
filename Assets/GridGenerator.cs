using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public ObstacleData obstacleData;

    void Start()
    {
        GenerateGrid();
        SetObstacles();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                TileInfo tileInfo = cube.GetComponent<TileInfo>();
                tileInfo.gridPosition = new Vector2Int(x, z);
            }
        }
    }

    void SetObstacles()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                TileInfo tileInfo = GetTileInfoAtPosition(new Vector2Int(x, y));

                if (tileInfo != null && obstacleData.obstacleGrid[x, y])
                {
                    tileInfo.SetBlocked(true);
                }
            }
        }
    }

    TileInfo GetTileInfoAtPosition(Vector2Int position)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(position.x, 10f, position.y), Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.collider.GetComponent<TileInfo>();
        }

        return null;
    }
}
