using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldVFXManager : PersistentSingleton<WorldVFXManager>
{
    [Header("VFX")]
    public GameObject BloodPletterVFX;
    public GameObject ChopTreeVFX;
    public GameObject DamePopupVFX;

    public void CreatePopupDamage(Vector3 position, int dame)
    {
        DamePopUp damePopup = Instantiate(DamePopupVFX, position + new Vector3(0, 1f, 0), Quaternion.identity).GetComponent<DamePopUp>();
        damePopup.SetDamePopup(dame);
    }
}
