using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaiv : MonoBehaviour
{
    [SerializeField] GameObject gos;
    [SerializeField] GameObject egs;

    public void GOS()
    {
        gos.SetActive(true);
        EquipmentSystem.LevelEnded = true;
    }

    public void EGS()
    {
        egs.SetActive(true);
        EquipmentSystem.LevelEnded = true;
    }
}
