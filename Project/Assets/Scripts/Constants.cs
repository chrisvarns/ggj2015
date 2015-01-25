using UnityEngine;
using System.Collections;


public enum Status : int
{
    BROKEN = 0,
    DAMAGED = 1,
    HEALTHY = 2,
    __SIZE__
};

public class Constants 
{
    public const int kMaxOxygen = 20;
    public const int kHDTargetPower = 30;
    public const float kHDDamagedExplosionChance = 0.4f;
    public const int kMaxCrew = 4;
}
