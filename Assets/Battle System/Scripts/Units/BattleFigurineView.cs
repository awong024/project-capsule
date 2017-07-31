using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFigurineView : MonoBehaviour {
  [SerializeField] Image unitSprite;
  [SerializeField] Image healthBar;
  [SerializeField] Image abilitySprite;
  [SerializeField] Animator animator;
  [SerializeField] Image buffSprite;
	
  public void Render(FigurineModel model) {
    unitSprite.sprite = model.Sprite;

    if (model.CastAbility != null) {
      abilitySprite.sprite = model.CastAbility.AbilityIcon;
    }
    EnableAbilityButton(false);
  }

  public void UpdateHealthBar(float fillValue) {
    healthBar.fillAmount = fillValue;
  }

  public void EnableAbilityButton(bool enable) {
    if (abilitySprite != null && abilitySprite.gameObject.activeSelf != enable) {
      abilitySprite.gameObject.SetActive(enable);
    }
  }

  public void PlayAttackAnimation() {
    animator.SetTrigger("Attack");
  }

  public void PlayHitAnimation() {
    animator.SetTrigger("Hit");
  }

  public void PlayDeathAnimation() {
    animator.SetTrigger("Death");
  }

  //Hardcoded
  public void EnableBuffSprite(bool enable) {
    if (buffSprite != null && buffSprite.gameObject.active != enable) {
      buffSprite.gameObject.SetActive(enable);
    }
  }
}
