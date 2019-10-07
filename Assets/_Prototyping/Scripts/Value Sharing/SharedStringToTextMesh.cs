using UnityEngine;

public class SharedStringToTextMesh : MonoBehaviour
{
   public SharedString stringVariable;
    TextMesh textMesh;

    private void Start()
    {
        textMesh = this.GetComponent<TextMesh>(); 
    }

    private void Update()
    {
        textMesh.text = stringVariable.value;
    }
}
