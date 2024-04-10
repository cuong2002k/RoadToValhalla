using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public ConnectorPosition connectorPosition;
    public SelectBuildType connectorParentType;
    [HideInInspector] public bool canConnectTo = true;
    [HideInInspector] public bool isConnectToWall = false;
    [HideInInspector] public bool isConnectToFloor = false;

    [SerializeField] private bool canConnectToWall = true;
    [SerializeField] private bool canConnectToFloor = true;

    public void UpdateConnect(bool canRoot = false)
    {
        Collider[] connectionCollider = Physics.OverlapSphere(transform.position, transform.lossyScale.x / 2f);
        isConnectToFloor = !canConnectToFloor;
        isConnectToWall = !canConnectToWall;
        foreach (Collider col in connectionCollider)
        {
            if (col.GetInstanceID() == GetComponent<Collider>().GetInstanceID())
            {
                continue;
            }

            if(!col.gameObject.activeInHierarchy){
                continue;
            }

            if (gameObject.layer == col.gameObject.layer)
            {
                Connector connector = col.GetComponent<Connector>();

                if (connector.connectorParentType == SelectBuildType.floor) isConnectToFloor = true;
                if (connector.connectorParentType == SelectBuildType.wall) isConnectToWall = true;
                if (canRoot) UpdateConnect();
            }
        }
        canConnectTo = true;
        if (isConnectToFloor && isConnectToWall)
        {
            canConnectTo = false;
        }
    }
}

[SerializeField]
public enum ConnectorPosition
{
    Left,
    Right,
    Top,
    Bottom
}
