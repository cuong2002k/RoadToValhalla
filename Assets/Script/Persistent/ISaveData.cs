using UnityEngine;
public interface ISaveData
{
    [SerializeField] public string id { get; set; }
    object CaptureState();
    void RestoreState(object state);
}
