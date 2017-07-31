using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilityModel : AbilityModel {
  [SerializeField] int castTime;

  public int ChargeTime { get { return castTime; } }
}
