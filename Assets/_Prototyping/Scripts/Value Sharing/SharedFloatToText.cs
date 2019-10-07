using UnityEngine;
using UnityEngine.UI;

public class SharedFloatToText : MonoBehaviour
{
   public SharedFloat stringVariable;
    [SerializeField]
    private int decimalPlaces = 2; 
      Text textMesh;

    private void Start()
    {
        textMesh = this.GetComponent<Text>(); 
    }

    private void Update()
    {
        textMesh.text = System.Math.Round(stringVariable.value, decimalPlaces).ToString();
    }
}
