using UnityEngine;
using System.Collections;

public enum BuffType {
  Barrier,    //Absorb X0% of the next Y Damage (i.e. partial or absolute shielding)
  Replenish,  //Heal for X health every tick
  Purify,     //Cleanse debuffs every tick + Heals for moderate amount on last tick
  Champion,   //Increase threat generation
  Angel,      //The next fatal blow taken instead heals for X amount

  //Stat Booster
  Might,      //Increase Strength
  Haste,      //Increase Agility
  Brilliance, //Increase Intellect

  //Debuff
  DoT,        //Damage over Time (poison, etc)
  Weaken,     //Decrease Strength
  Slow,       //Decrease Agility
  Suppression,//Decrease Intellect
}

public class UnitBuff : ScriptableObject {
  
}
