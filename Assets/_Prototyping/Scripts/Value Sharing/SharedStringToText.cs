using UnityEngine;
using UnityEngine.UI;

public class SharedStringToText : MonoBehaviour
{
   public SharedString stringVariable;
   Text textMesh;

    private void Start()
    {
        textMesh = this.GetComponent<Text>(); 
    }

    private void Update()
    {
        textMesh.text = stringVariable.value;
    }
}
