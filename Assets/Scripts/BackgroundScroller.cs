using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] float backgroundScrollSpeed = 0.5f;
  Material myMaterial;
  Vector2 offSet;
  void Start()
  {
    myMaterial = GetComponent<Renderer>().material;



  }

  // Update is called once per frame
  void Update()
  {
    var xScroll = Input.GetAxis("Horizontal") * Time.deltaTime * 10;
    Debug.Log(xScroll);

    myMaterial.mainTextureOffset += new Vector2(xScroll, backgroundScrollSpeed) * Time.deltaTime;
  }
}
