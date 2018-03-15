using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*prefab Dmage text script
 * currently has methods to move but not sedtroy themselves will add after bug fixed
 */

public class TestDamage : MonoBehaviour {
    private float speed;    //local speed
    private Vector3 direction;  //local direction
    private float fade;     //future fade timer

    private void Update()   //move in certain direction
    {
        float move = speed * Time.deltaTime;

        transform.Translate(direction * move);
    }

    public void Initialize(float tempSpeed, Vector3 tempDirection, float fadeTime)  //set speed and direction
    {
        speed = tempSpeed;
        direction = tempDirection;
        fade = fadeTime;

        StartCoroutine(Fade());
    }

	private IEnumerator Fade()
	{
        
		float Alpha = GetComponent<Text> ().color.a;

		float rate = 1f / fade;
		float progress = 0f;

		while (progress < 1f)
		{
            Color tempColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(Alpha, 0, progress));
            progress += rate * Time.deltaTime;
            yield return null;

        }

        Destroy(gameObject);
	}
}
