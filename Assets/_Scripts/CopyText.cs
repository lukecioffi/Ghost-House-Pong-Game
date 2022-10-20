using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopyText : MonoBehaviour
{
    [SerializeField] TextMeshPro text_to_copy;
    [SerializeField] TextMeshPro my_text;
	
	Color color;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		my_text.SetText(text_to_copy.text);
		my_text.color = new Color(my_text.color.r, my_text.color.g, my_text.color.b, text_to_copy.color.a);
    }
}
