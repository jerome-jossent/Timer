//Coded by Abishek J Reuben
using UnityEngine;
using System.Collections.Generic;
using System;

public class ExplodeView : MonoBehaviour
{    
    #region Variables
    public List<SubMeshes> childSubMeshes;
    public List<MeshRenderer> childMeshRenderers;
    public bool isInExplodedView = false;
    public float explosionSpeed = 0.1f;
    bool isMoving = false;
    #endregion

    #region UnityFunctions
    private void Awake()
    {
        childSubMeshes = new List<SubMeshes>();
        childMeshRenderers = new List<MeshRenderer>();
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            SubMeshes mesh = new SubMeshes();
            mesh.meshRenderer = item;
            mesh.originalPosition = item.transform.position;
            mesh.explodedPosition = item.bounds.center * 1.5f;
            childSubMeshes.Add(mesh);
            childMeshRenderers.Add(item);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            if (isInExplodedView)
            {
                foreach (SubMeshes item in childSubMeshes)
                {
                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.explodedPosition, explosionSpeed);
                    if (Vector3.Distance(item.meshRenderer.transform.position, item.explodedPosition) < 0.001f)                    
                        isMoving = false;                    
                }
            }
            else
            {
                foreach (SubMeshes item in childSubMeshes)
                {
                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.originalPosition, explosionSpeed);
                    if (Vector3.Distance(item.meshRenderer.transform.position, item.originalPosition) < 0.001f)
                        isMoving = false;
                }
            }
        }
    }
    #endregion

    public void _SetexplodedPosition(float val)
    {
        for (int i = 0; i < childMeshRenderers.Count; i++)        
            childSubMeshes[i].explodedPosition = childMeshRenderers[i].bounds.center * val * 100;        
    }


    #region CustomFunctions
    public void ToggleExplodedView()
    {
        if (isInExplodedView)
        {
            isInExplodedView = false;
            isMoving = true;
        }
        else
        {
            isInExplodedView = true;
            isMoving = true;
        }
    }
    #endregion
}

[Serializable]
public class SubMeshes
{
    public MeshRenderer meshRenderer;
    public Vector3 originalPosition;
    public Vector3 explodedPosition;
}