using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MoveDestination : MonoBehaviour
{

    private NavMeshAgent agent;

    private bool isrun = false;
    private float accpos = 0.0f;
    private float dccpos = 0.0f;
    public AudioClip acc;
    public AudioClip dcc;
    public AudioClip rapidcc;
    public AudioClip memoria;
    public AudioClip kitaikebukuroA;
    public AudioClip kitaikebukuroB;
    public AudioClip shimoitabashiA;
    public AudioClip shimoitabashiB;
    private bool tj02A = true;
    private bool tj02B = true;
    private bool tj03A = true;
    private bool tj03B = true;
    private AudioSource audioSource;
    public AudioSource housou;
    private bool abletogo = false;
    public Text speed;
    public Text mode;
    public Text distance;
    public Text cango;
    public Text atsspeed;
    private float time=0;
    private float dis = 0.0f;
    private float speedbefore = 0.0f;
    private bool fullrun = false;
    private int[] stationdis = {920,1900};
    private string[] stationname = { "北池袋","下板橋"};
    private bool[] stop = {true,true};
    private int staindex = 0;


    public Transform goal;

    public float accelerate = 3.2f;

    public int index = 8;

    public void setacc(int index)

    {
        if (audioSource.isPlaying==false)
        {
            audioSource.clip = acc;
            if (accelerate>8) {
                audioSource.time = acc.length * (agent.speed * 3.6f / 120f);
            }else if (accelerate < 8)
            {
                audioSource.time = acc.length * ((agent.speed * 3.6f + 20.0f)/ 120f);
            }



            audioSource.Play();



        }



        if (index == 12)
        {
            accelerate = 3.3f;//P4
            mode.text = "Mode:P4";
            if (agent.speed<=95) {
                audioSource.pitch = 1.0f;
                fullrun = false;
            }

        }
        else if (index == 11)
        {
            accelerate = 2.4f;//P3
            mode.text = "Mode:P3";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = 0.75f;
            }
        }
        else if (index == 10)
        {
            accelerate = 1.7f;//P2
            mode.text = "Mode:P2";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = 0.5f;
            }
        }
        else if (index == 9)
        {
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = 0.25f;
            }
            mode.text = "Mode:P1";
            accelerate = 0.9f;//P1
        }
        else if (index == 8)
        {
            audioSource.pitch = 1.0f;
            audioSource.Pause();
            if (agent.speed*3.6>95&&fullrun==false)
            {
                audioSource.clip = rapidcc;
                audioSource.Play();
            }

            mode.text = "Mode:N";
            accpos =agent.speed/120;
            accelerate = 0;//N
        }
        else if (index == 7)
        {
            mode.text = "Mode:B1";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -0.125f;
            }
            accelerate = -0.05f;//B1
        }
        else if (index == 6)
        {
            mode.text = "Mode:B2";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -0.25f;
            }
            accelerate = -0.3f;//B2
        }
        else if (index == 5)
        {
            mode.text = "Mode:B3";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -0.4f;
            }
            accelerate = -0.9f;//B3
        }
        else if (index == 4)
        {
            mode.text = "Mode:B4";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -0.5f;
            }
            accelerate = -1.5f;//B4
        }
        else if (index == 3)
        {
            mode.text = "Mode:B5";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -0.6f;
            }
            accelerate = -2.1f;//B5
        }
        else if (index == 2)
        {
            mode.text = "Mode:B6";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -0.8f;
            }
            accelerate = -2.8f;//B6
        }
        else if (index == 1) {
            mode.text = "Mode:B7";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -1.0f;
            }
            accelerate = -3.3f;//B7
         }else if (index == 0)
        {
            mode.text = "Mode:EB";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = -2.0f;
            }
            accelerate = -4.5f;//EB

        }

    }

    IEnumerator FuncCoroutine()
    {
        while (true)
        {
            if (agent.speed!=0)
            {
                time += 0.1f;
            }
            agent.speed += (0.01f * accelerate);
            if (index==8||accelerate==0.0f)
            {
                dis += agent.speed*0.04f;
            }
            else
            {
                dis += ((speedbefore + agent.speed) / 2.0f)*0.04f;
            }
            speedbefore = agent.speed;
            speed.text = "Speed:" + agent.speed*3.6+"km/h";
            distance.text =stationname[staindex] +"まで後："+(stationdis[staindex] - dis);
            // 何か処理
            yield return new WaitForSeconds(0.01f);
        }
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        StartCoroutine(FuncCoroutine());
        audioSource = gameObject.GetComponent<AudioSource>();
        Invoke("shingo", 32.0f);



    }

    void shingo()
    {
        abletogo = true;
        cango.text = "信号：進行";
    }

    private void ats()
    {

    }

    private void runhousou()
    {

        if (tj02A==true&&dis>5) {
            tj02A = false;
            housou.clip = kitaikebukuroA;
            housou.Play();
        
          }
        if (tj02B==true&&dis>600) {
            tj02B = false;
            housou.clip = kitaikebukuroB;
            housou.Play();
        }


    }




    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (index < 12) {
                index++;
                setacc(index);

            }

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (index > 0)
            {
                index--;
                setacc(index);

            }
        }

        if (agent.speed==0)
        {
            audioSource.Pause();
        }
        if (agent.speed * 3.6 > 90&&fullrun==false)
        {
            audioSource.clip = rapidcc;
            fullrun = true;
            audioSource.Play();
        }
        if (stationdis[staindex] - dis<0)
        {
            staindex++;
        }
        ats();
        runhousou();
       
    }
}