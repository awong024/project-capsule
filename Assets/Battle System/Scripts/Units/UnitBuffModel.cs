using UnityEngine;
using System.Collections;

public class UnitBuffModel : ScriptableObject {
  
  public enum BuffType
  {
    Shield
  }

  [SerializeField] string buffName;
  [SerializeField] BuffType buffType;
  [SerializeField] int buffDuration;
  [SerializeField] int buffPower;

  public string BuffName { get { return buffName; } }
  public BuffType Buff_Type { get { return buffType; } }
  public int BuffDuration { get { return buffDuration; } }
  public int BuffPower { get { return buffPower; } }
}
