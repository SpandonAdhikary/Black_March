using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public float moveSpeed = 3f;

    private bool isMoving = false;
    private Vector2Int targetPosition;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        MoveToRandomAdjacentTile();
    }

    void Update()
    {
        if (!isMoving)
        {
            MoveToRandomAdjacentTile();
        }
    }

    void MoveToRandomAdjacentTile()
    {
        Vector2Int playerPosition = playerMovement.GetCurrentGridPosition();
        Vector2Int[] adjacentTiles = GetAdjacentTiles(playerPosition);

        targetPosition = adjacentTiles[Random.Range(0, adjacentTiles.Length)];
        StartCoroutine(MoveToTile(targetPosition));
    }

    Vector2Int[] GetAdjacentTiles(Vector2Int position)
    {
        Vector2Int[] adjacentTiles = new Vector2Int[4];

        adjacentTiles[0] = new Vector2Int(position.x + 1, position.y);
        adjacentTiles[1] = new Vector2Int(position.x - 1, position.y);
        adjacentTiles[2] = new Vector2Int(position.x, position.y + 1);
        adjacentTiles[3] = new Vector2Int(position.x, position.y - 1);

        return adjacentTiles;
    }

    System.Collections.IEnumerator MoveToTile(Vector2Int targetPosition)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetWorldPosition = new Vector3(targetPosition.x, 0.5f, targetPosition.y); // Adjust the y-coordinate as needed
        float journeyLength = Vector3.Distance(startPosition, targetWorldPosition);
        float startTime = Time.time;

        while (transform.position != targetWorldPosition)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetWorldPosition, fractionOfJourney);
            yield return null;
        }

        isMoving = false;
    }

    public void MoveTo(Vector2Int targetPosition)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToTile(targetPosition));
        }
    }
}
