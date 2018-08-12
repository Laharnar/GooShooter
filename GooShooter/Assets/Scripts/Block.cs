using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [System.NonSerialized] public bool isSlimeActive = false;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        Material mat = meshRenderer.sharedMaterials[0];
		Material[] materials = new Material[2];
		materials[0] = mat;
		meshRenderer.sharedMaterials = materials;
    }

    public void ToggleSlime(bool enabled)
    {
        isSlimeActive = enabled;
        meshRenderer.materials[1] = null;

        if (enabled)
        {
			Material[] materials = meshRenderer.materials;
			materials[1] = GameManager.Instance.slimeMaterials[Random.Range(0, GameManager.Instance.slimeMaterials.Length)];
            meshRenderer.materials = materials;
        }
    }
}