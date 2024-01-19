using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaceTowers : MonoBehaviour
{
    [SerializeField] private LayerMask CheckPlacement;
    [SerializeField] private LayerMask PlacementCollider;
    [SerializeField] private Camera PlayersCamera;
    private GameObject TowerToPlace;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TowerToPlace != null)
        {
            Ray camray = PlayersCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;

            if (Physics.Raycast(camray, out HitInfo, 100f, PlacementCollider))
            {
                TowerToPlace.transform.position = HitInfo.point;
            }
            if (Input.GetMouseButtonDown(0) && HitInfo.collider.gameObject != null)
            {
                if (!HitInfo.collider.gameObject.CompareTag("CantPlace"))
                {
                    
                    if(!HitInfo.collider.gameObject.CompareTag("Tower"))
                    {
                        BoxCollider TowersCollider = TowerToPlace.gameObject.GetComponent<BoxCollider>();
                        TowersCollider.isTrigger = true;
                        Vector3 BoxCenter = TowerToPlace.gameObject.transform.position + TowersCollider.center;
                        Vector3 HalfExtent = TowersCollider.size / 2;
                        if (!Physics.CheckBox(BoxCenter, HalfExtent, Quaternion.identity, CheckPlacement, QueryTriggerInteraction.Ignore))
                        {
                            TowersCollider.isTrigger = false;
                            TowerToPlace = null;
                        }
                    }
                    
                }


            }
        }
    }

    public void PlaceTower(GameObject Tower)
    {
        TowerToPlace = Instantiate(Tower, Vector3.zero, Quaternion.Euler(0, 0, 0));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {

    }
}
