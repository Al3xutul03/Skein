using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour
{
    [SerializeField]
    private List<Material> targetMaterials;
    [SerializeField]
    private float groundDistance = 0.2f;

    private TurnManager turnManager;
    private MeshRenderer meshRenderer;
    private Dictionary<Type, Material> materialList;
    private GameObject targetedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnManager = GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
        meshRenderer = GetComponent<MeshRenderer>();

        targetedObject = null;
        meshRenderer.enabled = false;

        materialList = new Dictionary<Type, Material>
        {
            { typeof(Enemy), targetMaterials[1]},
            { typeof(Player), targetMaterials[2]},
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedObject != null)
        {
            this.transform.position = targetedObject.transform.position + Vector3.up * groundDistance;
        }

        if (turnManager.IsCurrentCharacter(targetedObject) &&
            meshRenderer.material != targetMaterials[0] &&
            targetedObject.GetComponent<Player>() != null)
        {
            meshRenderer.material = targetMaterials[0];
        }
    }

    public void ChangeTarget(Enemy target)
    {
        targetedObject = target.gameObject;
        meshRenderer.enabled = true;
        meshRenderer.material = materialList[typeof(Enemy)];
    }

    public void ChangeTarget(Player target)
    {
        targetedObject = target.gameObject;
        meshRenderer.enabled = true;

        if (turnManager.IsCurrentCharacter(target.gameObject))
        {
            meshRenderer.material = targetMaterials[0];
        }
        meshRenderer.material = materialList[typeof(Player)];
    }
}
