using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
  public Scrollbar scrollbar;
  private Text text;
  public float level;

  private void Awake() {
    this.text = GetComponent<Text>();
    level = 0f;
  }

  // Start is called before the first frame update
  void Start()
  {
    scrollbar.value = 0;
  }

    // Update is called once per frame
  void Update()
  {
    level = Small(Mathf.Floor(scrollbar.value * 10) + 1, 10f);
    this.text.text = level.ToString();
  }

  private float Small(float a, float b) {
    if (a > b) return b;
    else return a;
  }
}
