using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townfolk : BaseAI
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        var Wandering = new Wandering(this, agent, anim);
        stateMachine.SetState(Wandering);
    }

    private void Update()
    {
        stateMachine.Tick();
        
        if (prevPos < transform.position.x)
            sr.flipX = false;
        else if (prevPos > transform.position.x)
            sr.flipX = true;

        prevPos = transform.position.x;
    }

}
