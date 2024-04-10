using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    [Header("Build Objects")]
    public List<GameObject> floorGameObject = new List<GameObject>();
    public List<GameObject> wallGameObject = new List<GameObject>();
    [Header("Destroy setting")]
    [SerializeField] public bool isDestroy = false;
    [SerializeField] private Transform LastHitDestroyTranform;
    private List<Material> materials = new List<Material>();


    [Header("Build Settings")]
    public SelectBuildType selectBuildTyte;
    public LayerMask connectorLayer; // The layer that the player can build on
    [Header("Ghost Settings")]
    public Material ghostMaterialValid;   // The material used to draw the ghost
    public Material ghostMaterialInvalid; // The material used  to draw the invalid ghost
    public float connectOverlabRadius = 1f;
    public float maxGroundAngle = 45f;
    [Header("Internal State")]
    public bool isBuilding = false;
    public int currentBuildingIndex;
    public GameObject ghostBuildGameObject;
    public bool isGhostInValidPosition = false;
    public Transform modelParent = null;

    [Header("UI")]
    [SerializeField] private GameObject buidingUI;


    private void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {
            SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // isBuilding = !isBuilding;
            ShowPopupBuildingUI(!buidingUI.activeInHierarchy);
            SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            isDestroy = !isDestroy;
        }

        if (isBuilding && !isDestroy)
        {
            ghostBuild();
            if (Input.GetMouseButtonDown(0))
            {
                Placebuild();
            }
        }
        else if (ghostBuildGameObject)
        {
            Destroy(ghostBuildGameObject);
            ghostBuildGameObject = null;
        }

        if (isDestroy)
        {
            GhostDestroy();
            if (Input.GetMouseButtonDown(0))
            {
                Destroybuild();
            }

        }

    }

    private void SetActive(bool active)
    {
        isBuilding = active;
    }

    private void ShowPopupBuildingUI(bool active)
    {
        buidingUI.SetActive(active);
        Camera.main.transform.root.GetComponent<PlayerController>().enabled = !active;
        Camera.main.transform.GetComponentInParent<CameraController>().enabled = !active;
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SelectBuildingType(int BuildType)
    {
        if (BuildType == 0)
        {
            this.selectBuildTyte = SelectBuildType.wall;
        }
        else if (BuildType == 1)
        {
            this.selectBuildTyte = SelectBuildType.floor;
        }
    }

    public void SelectBuildingIndex(int index)
    {
        this.currentBuildingIndex = index;
        SetActive(true);
        ShowPopupBuildingUI(false);
    }

    private void ghostBuild()
    {
        GameObject currentBuild = GetCurrentBuild();
        CreateGhostPrefabs(currentBuild);
        MoveGhostPrefabsToRaycast();
        CheckBuildValidity();
    }

    private void Placebuild()
    {
        if (ghostBuildGameObject != null && isGhostInValidPosition)
        {
            GameObject newBuild = Instantiate(GetCurrentBuild(), ghostBuildGameObject.transform.position, ghostBuildGameObject.transform.rotation);
            newBuild.GetComponentInChildren<Collider>().enabled = true;
            // Destroy(ghostBuildGameObject);
            // ghostBuildGameObject = null;
            // isBuilding = false;
            foreach (Connector connector in newBuild.GetComponentsInChildren<Connector>())
            {
                connector.UpdateConnect(true);
            }
        }
    }

    private GameObject GetCurrentBuild() // get object buidling with type
    {
        switch (selectBuildTyte)
        {
            case SelectBuildType.wall:
                return wallGameObject[currentBuildingIndex];
            case SelectBuildType.floor:
                return floorGameObject[currentBuildingIndex];
        }
        return null;
    }

    private void CreateGhostPrefabs(GameObject currentBuild)
    {
        if (ghostBuildGameObject == null)
        {
            ghostBuildGameObject = Instantiate(currentBuild);
            modelParent = ghostBuildGameObject.transform.GetChild(0);
            ghostifyModel(modelParent, ghostMaterialValid);
            ghostifyModel(ghostBuildGameObject.transform);

        }
    }

    private void MoveGhostPrefabsToRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // get ray when move mouse
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPos = hit.point;
            ghostBuildGameObject.transform.position = newPos;
        }
    }

    private void CheckBuildValidity()
    {
        Collider[] colliders = Physics.OverlapSphere(ghostBuildGameObject.transform.position,
        connectOverlabRadius, connectorLayer);

        if (colliders.Length > 0)
        {
            GhostConnectBuild(colliders);
        }
        else
        {
            GhostSeperatorBuild();
            if (isGhostInValidPosition)
            {
                Collider[] overlapColliders = Physics.OverlapBox(ghostBuildGameObject.transform.position,
                new Vector3(2f, 2f, 2f), ghostBuildGameObject.transform.rotation);

                foreach (Collider overlapCollider in overlapColliders)
                {
                    if (overlapCollider.gameObject != ghostBuildGameObject && overlapCollider.transform.root.CompareTag("Buildables"))
                    {
                        ghostifyModel(modelParent, ghostMaterialInvalid);
                        isGhostInValidPosition = false;
                        return;
                    }
                }
            }
        }
    }

    private void GhostConnectBuild(Collider[] colliders)
    {
        Connector bestConnector = null;
        foreach (Collider collider in colliders)
        {
            Connector connector = collider.GetComponent<Connector>();
            if (connector.canConnectTo)
            {
                bestConnector = connector;
                break;
            }
        }

        if (bestConnector == null ||
        selectBuildTyte == SelectBuildType.wall && bestConnector.isConnectToWall
        || selectBuildTyte == SelectBuildType.floor && bestConnector.isConnectToFloor)
        {
            ghostifyModel(modelParent, ghostMaterialInvalid);
            isGhostInValidPosition = false;
            return;
        }

        SnapGhostPrefabsToConnect(bestConnector);
    }

    public void SnapGhostPrefabsToConnect(Connector connector)
    {
        Transform ghostConnector = FindSnapConnector(connector.transform, ghostBuildGameObject.transform.GetChild(1));
        ghostBuildGameObject.transform.position = connector.transform.position - (ghostConnector.position - ghostBuildGameObject.transform.position);
        if (selectBuildTyte == SelectBuildType.wall)
        {
            Quaternion newRotaion = ghostBuildGameObject.transform.rotation;
            newRotaion.eulerAngles = new Vector3(newRotaion.eulerAngles.x, connector.transform.rotation.eulerAngles.y, newRotaion.eulerAngles.z);
            ghostBuildGameObject.transform.rotation = newRotaion;
        }
        ghostifyModel(modelParent, ghostMaterialValid);
        isGhostInValidPosition = true;
    }

    private void GhostSeperatorBuild()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (selectBuildTyte == SelectBuildType.wall)
            {
                ghostifyModel(modelParent, ghostMaterialInvalid);
                isGhostInValidPosition = false;
                return;
            }

            if (Vector3.Angle(hit.normal, Vector3.up) < maxGroundAngle)
            {
                ghostifyModel(modelParent, ghostMaterialValid);
                isGhostInValidPosition = true;
            }
            else
            {
                ghostifyModel(modelParent, ghostMaterialInvalid);
                isGhostInValidPosition = false;
            }


        }
    }

    private Transform FindSnapConnector(Transform snapConnector, Transform ghostConnector)
    {
        ConnectorPosition connectorPosition = GetOppositePosition(snapConnector.GetComponent<Connector>());

        foreach (Connector connector in ghostConnector.GetComponentsInChildren<Connector>())
        {
            if (connector.connectorPosition == connectorPosition)
            {
                return connector.transform;
            }
        }

        return null;
    }

    private ConnectorPosition GetOppositePosition(Connector connector)
    {
        ConnectorPosition connectorPosition = connector.connectorPosition;
        if (selectBuildTyte == SelectBuildType.wall && connector.connectorParentType == SelectBuildType.floor)
        {
            return ConnectorPosition.Bottom;
        }

        if (selectBuildTyte == SelectBuildType.floor &&
            connector.connectorParentType == SelectBuildType.wall &&
            connector.connectorPosition == ConnectorPosition.Top
            )
        {
            if (connector.transform.root.rotation.y == 0)
            {
                return GetConnectorCloestToPlayer(true);
            }
            else
            {
                return GetConnectorCloestToPlayer(false);
            }
        }

        switch (connectorPosition)
        {
            case ConnectorPosition.Left:
                return ConnectorPosition.Right;
            case ConnectorPosition.Right:
                return ConnectorPosition.Left;
            case ConnectorPosition.Bottom:
                return ConnectorPosition.Top;
            case ConnectorPosition.Top:
                return ConnectorPosition.Bottom;
            default:
                return ConnectorPosition.Bottom;
        }
    }

    private ConnectorPosition GetConnectorCloestToPlayer(bool topBottom)
    {
        Transform cameraTranform = Camera.main.transform;
        if (topBottom)
            return cameraTranform.position.z >= ghostBuildGameObject.transform.position.z ?
            ConnectorPosition.Bottom : ConnectorPosition.Top;
        else
            return cameraTranform.position.x >= ghostBuildGameObject.transform.position.x ?
            ConnectorPosition.Left : ConnectorPosition.Right;
    }

    // change materials of the model current build
    private void ghostifyModel(Transform modelParent, Material ghostMaterials = null)
    {
        if (ghostMaterials != null)
        {
            foreach (MeshRenderer meshRenderer in modelParent.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.material = ghostMaterials;
                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    meshRenderer.materials[i] = ghostMaterials;
                }
            }
        }
        else
        {
            foreach (Collider collider in modelParent.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }
    }

    private void Destroybuild()
    {
        if (LastHitDestroyTranform)
        {
            foreach (Connector connector in LastHitDestroyTranform.GetComponentsInChildren<Connector>())
            {
                connector.gameObject.SetActive(false);
                connector.UpdateConnect(true);
            }
            Destroy(LastHitDestroyTranform.gameObject);
            LastHitDestroyTranform = null;
            // isDestroy = false;
        }
    }

    private void GhostDestroy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.root.CompareTag("Buildables"))
            {
                if (!LastHitDestroyTranform)
                {
                    LastHitDestroyTranform = hit.transform.root;
                    materials.Clear();
                    foreach (MeshRenderer lastHitRenderers in LastHitDestroyTranform.GetComponentsInChildren<MeshRenderer>())
                    {
                        materials.Add(lastHitRenderers.material);
                    }

                    ghostifyModel(LastHitDestroyTranform.GetChild(0), ghostMaterialInvalid);
                }
                else if (hit.transform.root != LastHitDestroyTranform)
                {
                    ResetLastHitDestroyTranform();
                }
            }
            else if (LastHitDestroyTranform)
            {
                ResetLastHitDestroyTranform();
            }
        }

    }

    private void ResetLastHitDestroyTranform()
    {
        int counter = 0;
        foreach (MeshRenderer lastHitMeshRenderers in LastHitDestroyTranform.GetComponentsInChildren<MeshRenderer>())
        {
            lastHitMeshRenderers.material = materials[counter];
            counter++;
        }
        LastHitDestroyTranform = null;

    }
}

[System.Serializable]
public enum SelectBuildType
{
    wall = 0,
    floor = 1
}
