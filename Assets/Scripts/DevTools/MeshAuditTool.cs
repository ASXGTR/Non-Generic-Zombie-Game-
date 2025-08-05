using UnityEngine;

public class MeshAuditTool : MonoBehaviour
{
    [Header("Target Mesh")]
    public SkinnedMeshRenderer targetMesh;

    void Start()
    {
        if (targetMesh == null)
        {
            Debug.LogWarning("MeshAuditTool: No target mesh assigned.");
            return;
        }

        Debug.Log($"🧪 Mesh Audit: {targetMesh.name}");

        // Audit blend shapes
        int blendShapeCount = targetMesh.sharedMesh.blendShapeCount;
        for (int i = 0; i < blendShapeCount; i++)
        {
            string shapeName = targetMesh.sharedMesh.GetBlendShapeName(i);
            float weight = targetMesh.GetBlendShapeWeight(i);
            Debug.Log($"Blend Shape [{i}]: {shapeName} = {weight}");
        }

        // Audit bone bindings
        Transform[] bones = targetMesh.bones;
        Debug.Log($"Bone Count: {bones.Length}");
        foreach (Transform bone in bones)
        {
            if (bone == null)
                Debug.LogWarning("⚠️ Missing bone reference.");
            else
                Debug.Log($"Bone: {bone.name}");
        }

        // Audit root bone
        if (targetMesh.rootBone != null)
            Debug.Log($"Root Bone: {targetMesh.rootBone.name}");
        else
            Debug.LogWarning("⚠️ No root bone assigned.");
    }
}
