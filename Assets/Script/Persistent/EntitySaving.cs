using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySaving : MonoBehaviour
{
    [SerializeField] private string id = "";

    public string GetUniqueID() => id;

    public object CaptureState()
    {
        Dictionary<string, object> dataCapture = new Dictionary<string, object>();
        foreach (ISaveData saveData in GetComponents<ISaveData>())
        {
            dataCapture[saveData.GetType().ToString()] = saveData.CaptureState();
        }
        return dataCapture;
    }


    public void RestoreState(object state)
    {
        Dictionary<string, object> dataRestore = (Dictionary<string, object>)state;
        foreach (ISaveData saveData in GetComponents<ISaveData>())
        {
            if (dataRestore.ContainsKey(saveData.GetType().ToString()))
            {
                saveData.RestoreState(dataRestore[saveData.GetType().ToString()]);
            }
        }
    }
}
