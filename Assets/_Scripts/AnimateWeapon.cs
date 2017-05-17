using UnityEngine;
using System.Collections;

public class AnimateWeapon : MonoBehaviour
{

    private Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animation.Play("AttackAnimation");
        }
    }
}
