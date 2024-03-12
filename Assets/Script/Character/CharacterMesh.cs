using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterMesh : MonoBehaviour
{
    [SerializeField]
    private Transform _bodyTranform;

    [SerializeField]
    public SkinnedMeshRenderer[] originalMesh;

    [SerializeField]
    private SkinnedMeshRenderer[] currentMesh;
    private GameObject[] currentEquipmentObject;

    private void Awake()
    {
        int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentMesh = new SkinnedMeshRenderer[size];
        currentEquipmentObject = new GameObject[size];
    }

    public void EquipSkinnedMesh(EquipmentType type, SkinnedMeshRenderer skinnedMesh)
    {
        int index = (int)type;
        if (currentMesh[index] != null)
        {
            Destroy(currentEquipmentObject[index]);
            currentMesh[index] = null;
        }
        GameObject equipmentSpawn = Instantiate(skinnedMesh, _bodyTranform).gameObject;
        SkinnedMeshRenderer skinnedMeshSpawn = equipmentSpawn.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshSpawn.rootBone = GetRootBone(type);
        skinnedMeshSpawn.bones = GetBones(type);
        currentMesh[index] = skinnedMesh;
        currentEquipmentObject[index] = equipmentSpawn;
    }

    private Transform[] GetBones(EquipmentType type)
    {
        return originalMesh[(int)type].bones;
    }

    public Transform GetRootBone(EquipmentType type)
    {
        return originalMesh[(int)type].rootBone;
    }


}
