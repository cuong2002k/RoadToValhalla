using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public ConnectorPosition connectorPosition;
    public SelectBuildType connectorParentType;
    public bool canConnectTo = true;
    public bool isConnectToWall = false;
    public bool isConnectToFloor = false;

    private bool canConnectToWall = true;
    private bool canConnectToFloor = true;

    public void UpdateConnect(bool canRoot = false)
    {
        Collider[] connectionCollider = Physics.OverlapSphere(transform.position, transform.lossyScale.x / 2f);
        isConnectToFloor = !canConnectToFloor;
        isConnectToWall = !canConnectToWall;
        foreach (Collider collider in connectionCollider)
        {
            if (collider.GetInstanceID() == GetComponent<Collider>().GetInstanceID())
            {
                continue;
            }

            if (!collider.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (this.gameObject.layer == collider.gameObject.layer)
            {
                Connector connector = collider.GetComponent<Connector>();
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, transform.lossyScale.x / 2f);
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
