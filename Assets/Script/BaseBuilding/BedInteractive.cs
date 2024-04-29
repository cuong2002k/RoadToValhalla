using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractive : MonoBehaviour, IInteractable, ISelectable
{
    public string GetNameItemSelect()
    {
        return "Sleep";
    }

    public void Interact()
    {
        PlayerManager.Instance.SetRespawnPos(this.transform.position);
    }
}
