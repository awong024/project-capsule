using UnityEngine;
using System.Collections;

public class AbilityModel : ScriptableObject {
  [SerializeField] string abilityName;
  [SerializeField] Sprite abilityIcon;
  [SerializeField] TargetMode targetMode;
  [SerializeField] int cooldown;
  [SerializeField] string animationFX_name;

  [SerializeField] UnitBuffModel generatedBuff;
  [SerializeField] AbilityEffectModel effectModel;

  public enum TargetMode {
    Self,
    Team,
    Enemy
  }

  public string AbilityName           { get { return abilityName; } }
  public Sprite AbilityIcon           { get { return abilityIcon; } }
  public TargetMode Target_Mode       { get { return targetMode; } }
  public int Cooldown                 { get { return cooldown; } }
  public string AnimationName         { get { return animationFX_name; } }

  public UnitBuffModel GeneratedBuff  { get { return generatedBuff; } }
  public AbilityEffectModel Effect    { get { return effectModel; } }
}
