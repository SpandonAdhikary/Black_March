using UnityEngine;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    public Text uiText; // Reference to your UI Text element

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();

            if (tileInfo != null)
            {
                // Access information from the TileInfo script and display on UI
                string tilePosition = "Tile Position: " + tileInfo.gridPosition;
                Debug.Log(tilePosition);

                // Update your UI with tile information
                if (uiText != null)
                {
                    uiText.text = tilePosition;
                }
            }
        }
    }
}
