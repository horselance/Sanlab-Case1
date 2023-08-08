using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Transform PistonParent;
    public Camera MainCamera;
    public int TotalPartsCount;
    public AudioSource Audio;
    public AudioClip placedSound;
    private int CompletedPartsCount;
    [HideInInspector] public bool Completed = false;
    [SerializeField] private TMP_Text congrText;
    [ColorUsage(true, true)]
    public Color SilhoutteEnabledColor, SilhoutteDisabledColor;


    void Awake()
    {
        Instance = this;
    }

    // Add or subtract from completed parts count.
    public void ModifyPartsCount(int count)
    {
        CompletedPartsCount += count;
        if (CompletedPartsCount == TotalPartsCount)
            CompleteSimulation();
    }

    // All parts are completed and simulation is over.
    private void CompleteSimulation()
    {
        Completed = true;
        congrText.gameObject.SetActive(true);
        congrText.transform.DOScale(1f, 0.5f).SetDelay(0.9f);
        PistonParent.DORotate(new Vector3(0f, 360f, 0f), 30f, RotateMode.FastBeyond360).SetDelay(1f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        PistonParent.DOScale(1.8f, 0.5f).SetDelay(1f);
        Debug.Log("Simulation Complete!");
    }
}
