using UnityEngine;
using System.Collections;

public class AnimateWeapon : MonoBehaviour
{

    public delegate void Attack(bool b);
    public static event Attack OnAttack;

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
            if (OnAttack != null)
                OnAttack(true);
            animation.Play("AttackAnimation");
        }
        else
        {
            if (OnAttack != null)
                OnAttack(false);
        }
    }
}
