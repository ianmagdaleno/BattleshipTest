using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentsManager : MonoBehaviour
{
    [SerializeField] private HealthShipManager shipStatsManager;

    public void ChangeTeamSail(ShipStruct newTeamSail)
    {
        shipStatsManager.Initialize(newTeamSail);
    }
}
