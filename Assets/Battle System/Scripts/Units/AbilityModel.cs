using UnityEngine;
using System.Collections;

public class AbilityModel : ScriptableObject {
  [SerializeField] string abilityName;
  [SerializeField] Sprite abilityIcon;
  [SerializeField] int chargeTime;
  [SerializeField] string animationFX_name;

  public string AbilityName { get { return abilityName; } }
  public Sprite AbilityIcon { get { return abilityIcon; } }
  public int ChargeTime { get { return chargeTime; } }
  public string AnimationName { get { return animationFX_name; } }
}
