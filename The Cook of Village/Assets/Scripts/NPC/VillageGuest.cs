using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGuest : MonoBehaviour
{
    private enum VillageNPC {Cow, Elephant, Fox}
    [SerializeField]private VillageNPC villageNPC;

    public int Holiday;
}
