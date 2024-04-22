using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPopUpManager : MonoBehaviour
{
    public GameObject PopupDeadUIOject;
    public GameObject PopupWinUIOject;


    public void ShowDeadPopUp()
    {
        PopupDeadUIOject.SetActive(true);
        StartCoroutine(HideDeadPopUp());
    }

    IEnumerator HideDeadPopUp()
    {
        yield return new WaitForSeconds(3f);
        PopupDeadUIOject.SetActive(false);
    }

    public void ShowWinPopUp()
    {
        PopupWinUIOject.SetActive(true);
        StartCoroutine(HideWinPopUp());
    }

    IEnumerator HideWinPopUp()
    {
        yield return new WaitForSeconds(3f);
        PopupDeadUIOject.SetActive(false);
    }

}
