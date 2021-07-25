using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
  public bool isAnswering;
  public List<int> answers;
  private bool isRight;

  private void Awake() {
    isInGame = false;
    seq = new List<int>();
    startButton.interactable = true;
    index = 0;
    isAnswering = false;
    answers = new List<int>();
    isRight = false;
  }

  private void Update() {
    startButton.interactable = !isInGame;
  }

  public void OnStart() {
    var step = level.level;
    seq.Clear();

    for (int i = 0; i < step; i++)
    {
      seq.Add(Random.Range(0, 9));
    }

    index = 0;
    InvokeRepeating("ChangeColor", 0f, 1f);
  }

  private void ChangeColor()
  {
    if (index < seq.Count)
    {
      Debug.Log(index);
      Debug.Log(seq[index]);
      before = buttons[seq[index]].transform.GetComponent<Renderer>().material.color;
      buttons[seq[index]].transform.GetComponent<Renderer>().material.color = Color.red;
      Invoke("Rollback", 0.5f);
    }
    else
    {
      CancelInvoke("ChangeColor");
      answers.Clear();
      isAnswering = true;
    }
  }

  private void Rollback()
  {
    buttons[seq[index]].transform.GetComponent<Renderer>().material.color = before;
    index++;
  }

  public void RegisterAnswer(int answer) {
    if (isAnswering)
    {
      answers.Add(answer);
      if (level.level <= answers.Count)
      {
        isAnswering = false;
        EndGame();
      }
    }
  }

  private void EndGame() {
    var correct = "";
    foreach (var s in seq)
      correct += s;

    var answer = "";
    foreach (var s in answers)
      answer += s;

    isRight = correct == answer;
    Invoke("Check", 0.05f);
  }

  private void Check()
  {
    var color = isRight ? Color.green : Color.red;

    foreach (var b in buttons)
      b.transform.GetComponent<Renderer>().material.color = color;

    Invoke("ResetGame", 1f);
  }

  private void ResetGame()
  {
    foreach(var b in buttons)
      b.transform.GetComponent<Renderer>().material.color = before;
    
    isInGame = false;
    seq.Clear();
    startButton.interactable = true;
    index = 0;
    isAnswering = false;
    answers.Clear();
    isRight = false;
  }
}
