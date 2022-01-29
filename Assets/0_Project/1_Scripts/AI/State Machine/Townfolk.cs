using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townfolk : BaseAI
{

    bool isDead = false;
    public bool IsDead => isDead;
    

    protected override void Start()
    {
        base.Start();

        var Wandering = new Wandering(this, agent, anim);
        stateMachine.SetState(Wandering);
    }

    protected override void Update()
    {
        if (isDead)
            return;
        
        base.Update();
        
        stateMachine.Tick();
        
        if (prevPos < transform.position.x)
            sr.flipX = false;
        else if (prevPos > transform.position.x)
            sr.flipX = true;

        prevPos = transform.position.x;
    }

    public void SetDead()
    {
        Invoke("SetDeadAnimation", 0.5f);
        Invoke("PLayDeadAudio", 0.3f);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        isDead = true;
        anim.SetBool("IsWalking", false);
    }

    void SetDeadAnimation()
    {
        anim.SetTrigger("IsDead");
    }

    void PLayDeadAudio()
    {
        AudioManager.instance.PlayRandomSFX(audiodata, "Dead");
    }
}
