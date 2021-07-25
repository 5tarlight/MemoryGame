using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
  private int index;
  private Color before;

  private void Awake() {
    isInGame = false;
    seq = new List<int>();
    startButton.interactable = true;
    index = 0;
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

    index = 0;
    InvokeRepeating("ChangeColor", 0f, 1f);
  }

  private void ChangeColor()
  {
    if (index < seq.Count)
    {
      before = buttons[seq[index]].transform.GetComponent<Renderer>().material.color;
      buttons[seq[index]].transform.GetComponent<Renderer>().material.color = Color.red;
      Invoke("Rollback", 0.5f);
    }
    else
      CancelInvoke("ChangeColor");
  }

  private void Rollback()
  {
    buttons[seq[index]].transform.GetComponent<Renderer>().material.color = before;
    index++;
  }
}
