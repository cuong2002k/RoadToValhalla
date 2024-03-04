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

    private void Start()
    {
        int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentMesh = new SkinnedMeshRenderer[size];
    }

    public void EquipSkinnedMesh(EquipmentType type, SkinnedMeshRenderer skinnedMesh)
    {
        int index = (int)type;
        if (currentMesh[index] != null)
        {
            Destroy(currentMesh[index].gameObject);
            currentMesh[index] = null;
        }

        SkinnedMeshRenderer skinnedMeshSpawn = Instantiate(skinnedMesh, _bodyTranform);
        skinnedMeshSpawn.rootBone = GetRootBone(type);
        skinnedMeshSpawn.bones = GetBones(type);
        currentMesh[index] = skinnedMesh;

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
