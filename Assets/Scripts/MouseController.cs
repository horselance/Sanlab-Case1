using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private bool _isMouseDown = false, _hasRaycastTarget = false;
    private Main _main;
    private Camera _camera;
    void Start()
    {
        _main = Main.Instance;
        _camera = _main.MainCamera;
    }

    void Update()
    {
        if (_main.Completed)
            return;
        if (Input.GetMouseButtonDown(0))
            OnMouseButtonDown();
        if (Input.GetMouseButtonUp(0))
            OnMouseButtonUp();
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void OnMouseButtonDown()
    {
        PistonPart part = RaycastToPart();
        if (part != null)
        {
            part.OnRaycastHit();
            OnRaycastHit();
        }
        else
            OnRaycastSpace();
    }

    void OnMouseButtonUp()
    {
        _hasRaycastTarget = false;
    }

    void OnRaycastHit()
    {
        _hasRaycastTarget = true;
    }

    // Raycast hits nothing.
    void OnRaycastSpace()
    {
        Debug.Log("OnRaycastHitSpace");
    }

    // If there is a raycast hit target which is a PistonPart, returns PistonPart, else returns null.
    public PistonPart RaycastToPart()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 6;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            try
            {
                PistonPart part = hit.transform.gameObject.GetComponent<PistonPart>();
                return part;
            }
            catch
            {
                return null;
            }
        }
        else
            return null;
    }
}
