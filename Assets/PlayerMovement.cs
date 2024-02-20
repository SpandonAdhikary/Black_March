using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;

    private bool isMoving = false;

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();

                if (tileInfo != null && !tileInfo.IsBlocked())
                {
                    StartCoroutine(MoveToTile(tileInfo.transform.position));
                }
            }
        }
    }

    System.Collections.IEnumerator MoveToTile(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        isMoving = false;
    }
    public Vector2Int GetCurrentGridPosition()
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x, 10f, transform.position.z), Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                return tileInfo.gridPosition;
            }
        }

        return Vector2Int.zero; // Return an invalid position if not found
    }
}
