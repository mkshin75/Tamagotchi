using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimController : MonoBehaviour
{
    public Animator petAnim;
    // Start is called before the first frame update
    void Start()
    {
        petAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            petAnim.SetTrigger("isHappy");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            petAnim.SetTrigger("isSad");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            petAnim.SetTrigger("isNormal");
        }
    }
}
