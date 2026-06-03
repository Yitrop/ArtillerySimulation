using UnityEngine;
using System.Collections.Generic;

public class MarkerManager : MonoBehaviour
{
    public static MarkerManager Instance;

    public GameObject markerPrefab;
    public int maxMarkers = 5;

    public Material normalMaterial;      // обычный материал
    public Material highlightMaterial;   // материал подсветки

    private List<GameObject> markers = new List<GameObject>();
    private GameObject lastHighlighted = null;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            ClearMarkers();
    }

    public void CreateMarker(Vector3 position, float distance)
    {
        GameObject marker = Instantiate(markerPrefab, position, Quaternion.identity);

        // обновляем текст
        var text = marker.GetComponentInChildren<TMPro.TextMeshPro>();
        if (text != null)
            text.text = distance.ToString("F1") + " м";

        markers.Add(marker);

        // удаляем старые
        if (markers.Count > maxMarkers)
        {
            Destroy(markers[0]);
            markers.RemoveAt(0);
        }

        HighlightLastMarker(marker);
    }

    private void HighlightLastMarker(GameObject newMarker)
    {
        // снимаем подсветку со старого
        if (lastHighlighted != null)
        {
            var oldDot = lastHighlighted.transform.Find("MarkerDot");
            if (oldDot != null)
            {
                var renderer = oldDot.GetComponent<MeshRenderer>();
                if (renderer != null)
                    renderer.material = normalMaterial;
            }
        }

        // включаем подсветку на новом
        var newDot = newMarker.transform.Find("MarkerDot");
        if (newDot != null)
        {
            var renderer = newDot.GetComponent<MeshRenderer>();
            if (renderer != null)
                renderer.material = highlightMaterial;
        }

        lastHighlighted = newMarker;
    }

    public void ClearMarkers()
    {
        foreach (var m in markers)
            Destroy(m);

        markers.Clear();
        lastHighlighted = null;
    }
}
