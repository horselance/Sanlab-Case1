using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PistonPart : MonoBehaviour
{
    public Vector3 RestPosition;
    public List<InsertPoint> InsertPoints = new List<InsertPoint>();
    public PistonPartType PartType;
    public float SnapDistance;
    public bool Insertable = false;
    protected bool IsInAnimation = false;
    protected bool IsDragging  = false;
    protected bool IsDragLocked = false;
    protected bool IsMouseDown = false;
    protected bool IsInserted = false;
    protected Vector3 _boltSpinAngle;
    protected Main _main;


    protected Camera Cam;

    public abstract void OnRaycastHit();
    protected abstract void OnMouseButtonUp();

    private void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        if (IsMouseDown && Input.GetMouseButtonUp(0))
            OnMouseButtonUp();
    }
    
    protected virtual void Init()
    {
        InitRestPosition();
        _main = Main.Instance;
        Cam = _main.MainCamera;
        _boltSpinAngle = new Vector3(0f, 0f, 3600f);
    }

    protected void InitRestPosition()
    {
        RestPosition = transform.position;
    }
}

public enum PistonPartType
{
    Rod = 0,
    Piston = 1,
    PinClip1 = 2,
    PinClip2 = 3,
    WristPin = 4,
    RodBearingRodSide = 5,
    RodBearingCapSide = 6,
    RodCap = 7,
    RodBolt = 8
}
