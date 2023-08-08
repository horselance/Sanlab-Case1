using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DynamicPart : PistonPart
{
    public List<DynamicPart> NextEnabledParts;
    protected override void Update()
    {
        base.Update();
        if (IsDragging)
            OnDrag();
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }

    // Starts when this part is a hit for raycast
    public virtual void OnDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Cam.WorldToScreenPoint(RestPosition).z;
        float posY = Cam.ScreenToWorldPoint(mousePosition).y;
        Vector3 targetPosition = new Vector3(RestPosition.x, posY, RestPosition.z);
        InsertPoint targetInsertPoint = InsertPoints[0];
        if (Mathf.Abs(targetPosition.y - targetInsertPoint.transform.position.y) < SnapDistance)
        {
            transform.position = targetInsertPoint.transform.position + targetInsertPoint.AnimationPoint;
            targetInsertPoint.gameObject.SetActive(true);
            IsDragLocked = true;
            InsertPoints[0].SetMaterialEnable(Insertable);
        }
        else
        {
            transform.position = targetPosition;
            targetInsertPoint.gameObject.SetActive(false);
            IsDragLocked = false;
        }

    }
    protected override void OnMouseButtonUp()
    {
        if (IsDragLocked && Insertable)
            DoInsertPoint();
        else
            MoveToRestPosition();
        IsDragging = false;
        IsDragLocked = false;
        IsMouseDown = false;
    }

    // Successfully placed part
    public virtual void DoInsertPoint()
    {
        IsInAnimation = true;
        foreach (var part in NextEnabledParts)
        {
            part.Insertable = true;
        }
        IsInserted = true;
        _main.ModifyPartsCount(1);
        InsertPoint targetInsertPoint = InsertPoints[0];
        _main.Audio.PlayOneShot(_main.placedSound);
        transform.DOKill();
        transform.DOMove(targetInsertPoint.transform.position, 0.7f)
            .OnComplete(() =>
            {
                if (this == null) return; 
                IsInAnimation = false;
            });

        if (PartType == PistonPartType.RodBolt)
            transform.DOLocalRotate(_boltSpinAngle, 0.7f, RotateMode.LocalAxisAdd);

        targetInsertPoint.gameObject.SetActive(false);
    }

    // Reset this part's position to initial value
    public virtual void MoveToRestPosition()
    {
        if (!IsInserted && !IsDragging)
            return;
        
        IsInAnimation = true;
        foreach (var part in NextEnabledParts)
        {
            part.Insertable = false;
            part.MoveToRestPosition();
        }
        InsertPoints[0].gameObject.SetActive(false);
        transform.DOKill();
        transform.DOMove(RestPosition, 0.4f)
            .OnComplete(() =>
            {
                if (this == null) return;
                IsInAnimation = false;
                if (IsInserted)
                {
                    Main.Instance.ModifyPartsCount(-1);
                    IsInserted = false;
                }
            });
    }

    public override void OnRaycastHit()
    {
        Debug.Log("This is a dynamic part");
        if (!IsInAnimation)
        {
            IsDragging = true;
            IsMouseDown = true;
        }
    }
}
