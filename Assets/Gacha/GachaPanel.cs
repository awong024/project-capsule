using UnityEngine;
using System.Collections;

public class GachaPanel : MonoBehaviour {
  [SerializeField] Animator animator;
  [SerializeField] PrizePanel prizePanel;

  //Hardcoded Collection
  [SerializeField] FigurineModel[] figurines;
	
  void Start() {
    PullGacha();
  }

  public void PullGacha() {
    animator.SetTrigger("GachaGet");

    FigurineModel prize = figurines[UnityEngine.Random.Range(0, figurines.Length)];
    prizePanel.Render(prize);
  }
}
