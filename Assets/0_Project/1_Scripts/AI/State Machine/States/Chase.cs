using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    readonly Police policeAI;
    NavMeshAgent agent;
    Animator anim;
    PlayerCore playercore;
    TowniesManager tm;
    Vector2 randInsideCircle;
    float originalSpeed;
    
    Vector2 targetPos;

    float timer;
    float startChaseTime = 0.75f;
    bool beganChase = false;

    public Chase(Police policeAI, NavMeshAgent agent, Animator anim, PlayerCore playercore, TowniesManager tm)
    {
        this.policeAI = policeAI;
        this.agent = agent;
        this.anim = anim;
        this.playercore = playercore;
        this.tm = tm;
        originalSpeed = policeAI.Speed;
        beganChase = false;
    }
    
    public void OnEnter()
    {
        if (AudioManager.instance.BGM_Source.clip.name == "HeckyllandJyde_Loop")
        {
            AudioManager.instance.BGM_Source.Stop();
        }
        
        AudioManager.instance.PlayRandomSFX(policeAI.audiodata, "POPO_OI");
        agent.speed = policeAI.ChaseSpeed;
        agent.ResetPath();
        policeAI.IsPursing = true;
        policeAI.LastPos = playercore.transform.position;
        randInsideCircle = Random.insideUnitCircle * playercore.radiusSize;
        anim.speed = 1.5f;
        agent.isStopped = false;
        timer = 0;
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
        beganChase = false;
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        if (timer < startChaseTime)
            return;

        if (!beganChase)
        {
            tm.AddPursuiter(policeAI);
            AudioManager.instance.PlaySFX(policeAI.audiodata, "Whistle");
            beganChase = true;
        }

        if (policeAI.IsOnSight)
        {
            anim.SetBool("IsWalking", true);
            if (Vector2.SqrMagnitude(policeAI.transform.position - playercore.transform.position) > playercore.radiusSize - 1.5f)
            {
                policeAI.LastPos = playercore.transform.position;
            }
            else
            {
                policeAI.LastPos = playercore.transform.position = randInsideCircle;
            }
        }
        
        agent.SetDestination(policeAI.LastPos);
    }

    IEnumerator StartChasing()
    {
        yield return new WaitForSeconds(startChaseTime);
    }
    
}
