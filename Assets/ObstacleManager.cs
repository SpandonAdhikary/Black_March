using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;
    public Transform gridParent;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                TileInfo tileInfo = GetTileInfoAtPosition(new Vector2Int(x, y));

                if (tileInfo != null && obstacleData.obstacleGrid[x, y])
                {
                    Vector3 position = new Vector3(x, 0.5f, y); // Adjust the y-coordinate as needed
                    Instantiate(obstaclePrefab, position, Quaternion.identity, gridParent);
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
