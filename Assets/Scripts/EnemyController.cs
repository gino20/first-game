using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator enemyAnim;
    protected AudioSource deathAudio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyAnim = GetComponent<Animator>();
        // Todo: 有多个audio下的优化
        deathAudio = GetComponent<AudioSource>();
    }

    public void BeElimatedHandler()
    {
        deathAudio.Play();
        enemyAnim.SetTrigger("beElimated");
    }

    public void DestroyHandler()
    {
        Destroy(enemyAnim.gameObject);
    }
}
