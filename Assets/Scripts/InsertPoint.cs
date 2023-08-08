using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertPoint : MonoBehaviour 
{
    public PistonPartType PointPartType;
    public Transform PointTransform;
    public bool IsInserted;
    public Vector3 AnimationPoint;
    private MeshRenderer _meshRenderer;
    private Main _main;

    private void Start()
    {
        _main = Main.Instance;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetMaterialEnable(bool enabled = true)
    {
        _meshRenderer?.material.SetColor("_FresnelColor", enabled ? _main.SilhoutteEnabledColor : _main.SilhoutteDisabledColor);
    }
}
