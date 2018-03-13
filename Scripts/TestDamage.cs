using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDamage : MonoBehaviour {
    private float speed;
    private Vector3 direction;
    private float fade;

    private void Update()
    {
        float move = speed * Time.deltaTime;

        transform.Translate(direction * move);
    }

    public void Initialize(float tempSpeed, Vector3 tempDirection)
    {
        speed = tempSpeed;
        direction = tempDirection;
    }
}
