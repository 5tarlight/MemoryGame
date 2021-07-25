using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public ButtonItem[] buttons;
  public Level level;
  public Button startButton;
  public Scrollbar scrollbar;
  private bool isInGame;
  public List<int> seq;

  private void Awake() {
    isInGame = false;
    seq = new List<int>();
    startButton.interactable = true;
  }

  private void Update() {
    startButton.interactable = !isInGame;
  }

  public void OnStart() {
    var step = level.level;
    seq.Clear();

    for (int i = 0; i < step; i++)
    {
      seq.Add(Random.Range(0, 10));
    }

    var str = "";
    foreach (var s in seq)
      str += s;

    Debug.Log(str);
  }
}
