using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataHolder", menuName = "Player/PlayerDataHolder")]
public class PlayerDataHolder : ScriptableObject
{
    public int totalCoins;
    public int totalFuelCapacityUpgrades;
    public int totalSpeedUpgrades;
}
