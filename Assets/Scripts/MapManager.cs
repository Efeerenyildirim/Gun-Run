using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] panels;

    private Vector3 firstPanelPosition = new Vector3(0f, 0f, 17f);
    int randomPanel;
    //17,42,67, 117

    void Start()
    {
        randomPanel = Random.Range(0, 8);
        Instantiate(panels[randomPanel], firstPanelPosition, Quaternion.Euler(Vector3.zero));
        firstPanelPosition.z += 25f;
        randomPanel = Random.Range(0, 8);
        Instantiate(panels[randomPanel], firstPanelPosition, Quaternion.Euler(Vector3.zero));
        firstPanelPosition.z += 25f;
        randomPanel = Random.Range(0, 8);
        Instantiate(panels[randomPanel], firstPanelPosition, Quaternion.Euler(Vector3.zero));
        firstPanelPosition.z += 50f;
        Instantiate(panels[8], firstPanelPosition, Quaternion.Euler(Vector3.zero));
    }
}
