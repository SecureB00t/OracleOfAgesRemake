using UnityEngine;

public class Sword : Tool
{

    public override void Use()
    {
        GetComponentInParent<Animator>().SetTrigger("attack");
    }
}