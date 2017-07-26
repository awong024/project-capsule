using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour
{
  [SerializeField] GameObject battlePanel;
  [SerializeField] GameObject gachaPanel;

  public void DisplayBattle() {
    EnablePanel(battlePanel, true);
    EnablePanel(gachaPanel, false);
  }

  public void DisplayGacha() {
    EnablePanel(gachaPanel, true);
    EnablePanel(battlePanel, false);
  }

  public void DisplayLobby() {
    EnablePanel(gachaPanel, false);
    EnablePanel(battlePanel, false);
  }

  private void EnablePanel(GameObject panel, bool enable) {
    panel.SetActive(enable);
  }
}
