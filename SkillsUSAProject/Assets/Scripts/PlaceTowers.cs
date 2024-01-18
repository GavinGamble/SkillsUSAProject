using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowers : MonoBehaviour
{
    [SerializeField] private Camera PlayersCamera;
    private GameObject TowerToPlace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TowerToPlace != null)
        {
            Ray camray = PlayersCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(camray, out RaycastHit hitInfo, 100f))
            {
                TowerToPlace.transform.position = hitInfo.point;
            }
            if(Input.GetMouseButtonDown(0))
            {
                TowerToPlace = null;
            }
        }
    }

    public void PlaceTower(GameObject Tower)
    {
        TowerToPlace = Instantiate(Tower, Vector3.zero, Quaternion.identity);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
