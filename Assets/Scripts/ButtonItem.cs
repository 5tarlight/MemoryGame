using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonItem : MonoBehaviour
{
  private new Renderer renderer;
  private Color before;
  public GameManager gameManager;
  public int index;

  private void Awake() {
    renderer = GetComponent<Renderer>();
    before = renderer.material.color;
  }

  private void OnMouseDown() {
    if (gameManager.isAnswering)
      renderer.material.color = Color.green;
  }

  private void OnMouseUp() {
    if (gameManager.isAnswering)
    {
      gameManager.RegisterAnswer(index);
      renderer.material.color = before;
    }
  }
}
