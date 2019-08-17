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
    public GameObject kitaikebukuro;
    public GameObject shimoitabashi;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    private List<GameObject> housous = new List<GameObject>();
    private List<GameObject> atss = new List<GameObject>();
    private int[] atssignal = { 30, -2, 60 };//-1:,-2:速度制限解除
    private int housouindex = 0;
    private bool tj02A = true;
    private bool tj02B = true;
    private bool tj03A = true;
    private bool tj03B = true;
    private bool housouA = true;
    private bool housouB = true;
    private bool flag = false;
    public AudioSource audioSource;
    public AudioSource housou;
    private bool abletogo = false;
    private bool dooropen = true;
    private float nowdis = 0;
    public Text speed;
    public Text mode;
    public Text distance;
    public Text cango;
    public Text atsspeed;
    public Text doorstatus;
    public AudioSource EBsound;
    public AudioClip EBhousou;
    public AudioClip EBbreak;
    public AudioSource notch;
    public AudioClip changenotch;
    public AudioSource atschange;
    public AudioSource chime;
    public AudioSource jokohousou;
    public AudioSource door;
    public AudioClip openthedoor;
    public AudioClip closethedoor;

    private float time = 0;
    private float stadis = 0.0f;
    private float speedbefore = 0.0f;
    private bool fullrun = false;
    private int[] stationdis = { 920, 1900 };
    private string[] stationname = { "北池袋", "下板橋" };
    private bool[] stop = { true, true };
    private int staindex = 0;
    private int atsindex = 0;


    public Transform goal;

    public float accelerate = 3.2f;

    public int index = 8;

    public void setacc(int index)

    {
        if (audioSource.isPlaying == false || accelerate != 8)
        {
            audioSource.clip = acc;
            if (accelerate > 8)
            {
                audioSource.time = acc.length * (agent.speed * 3.6f / 120f);
            }
            else if (accelerate < 8)
            {
                audioSource.time = acc.length * ((agent.speed * 3.6f) / 120f);
            }



            audioSource.Play();



        }



        if (index == 12)
        {
            notch.Play();
            audioSource.loop = false;
            accelerate = 3.3f;//P4
            mode.text = "Mode:P4";
            audioSource.pitch = 1.0f;
            fullrun = false;


        }
        else if (index == 11)
        {
            notch.Play();
            audioSource.loop = false;
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
            notch.Play();
            audioSource.loop = false;
            accelerate = 1.7f;//P2
            mode.text = "Mode:P2";

            fullrun = false;
            audioSource.pitch = 0.5f;

        }
        else if (index == 9)
        {
            notch.Play();
            audioSource.loop = false;

            fullrun = false;
            audioSource.pitch = 0.25f;

            mode.text = "Mode:P1";
            accelerate = 0.9f;//P1
        }
        else if (index == 8)
        {
            notch.Play();
            audioSource.pitch = 1.0f;
            audioSource.Pause();
            if (agent.speed != 0)
            {
                audioSource.clip = rapidcc;
                audioSource.loop = true;
                audioSource.Play();
            }

            mode.text = "Mode:N";
            accpos = agent.speed / 120;
            accelerate = 0;//N
        }
        else if (index == 7)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B1";

            fullrun = false;
            audioSource.pitch = -0.125f;

            accelerate = -0.05f;//B1
        }
        else if (index == 6)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B2";

            fullrun = false;
            audioSource.pitch = -0.25f;

            accelerate = -0.3f;//B2
        }
        else if (index == 5)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B3";
            fullrun = false;
            audioSource.pitch = -0.4f;

            accelerate = -0.9f;//B3
        }
        else if (index == 4)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B4";

            fullrun = false;
            audioSource.pitch = -0.5f;

            accelerate = -1.5f;//B4
        }
        else if (index == 3)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B5";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -2.1f;//B5
        }
        else if (index == 2)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B6";

            fullrun = false;
            audioSource.pitch = -0.8f;

            accelerate = -2.8f;//B6
        }
        else if (index == 1)
        {
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B7";

            fullrun = false;
            audioSource.pitch = -1.0f;

            accelerate = -5.3f;//B7
        }
        else if (index == 0)
        {
            notch.Play();
            EBsound.clip = EBbreak;
            EBsound.Play();
            housou.clip = EBhousou;
            housou.Play();
            audioSource.loop = false;
            mode.text = "Mode:EB";

            fullrun = false;
            audioSource.pitch = -2.0f;

            accelerate = -7.5f;//EB

        }

    }

    IEnumerator FuncCoroutine()
    {
        while (true)
        {

            if (agent.speed != 0)
            {
                time += 0.1f;
            }
            if (dooropen==false)
            {
                if ((agent.speed + (0.01f * accelerate)) * 3.6 < 120)
                {
                    agent.speed += (0.01f * accelerate);
                }
                else if ((agent.speed + (0.01f * accelerate)) * 3.6 >= 120)
                {
                    accelerate = 8;
                    audioSource.pitch = 1.0f;
                    audioSource.Pause();
                    audioSource.clip = rapidcc;
                    audioSource.loop = true;
                    audioSource.Play();

                    mode.text = "Mode:N";
                    accpos = agent.speed / 120;
                    accelerate = 0;//N
                }

            }

          
            if (stadis < 1 && agent.speed == 0)
            {
                staindex++;
                nowdis = Vector3.Distance(agent.transform.position, housous[staindex].transform.position);
                housouA = true;
                housouB = true;
            }
            speedbefore = agent.speed;
            speed.text = "Speed:" + agent.speed * 3.6 + "km/h";
            stadis = Vector3.Distance(agent.transform.position, housous[staindex].transform.position);
            distance.text = stationname[staindex] + "まで後：" + stadis;
            // 何か処理
            yield return new WaitForSeconds(0.01f);
        }
    }


    void Start()
    {


        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        atss.Add(obj1);
        atss.Add(obj2);
        atss.Add(obj3);

        housous.Add(kitaikebukuro);
        housous.Add(shimoitabashi);

        stadis = Vector3.Distance(agent.transform.position, housous[0].transform.position);
        nowdis = stadis;
        StartCoroutine(FuncCoroutine());

        Invoke("shingo", 32.0f);



    }

    void shingo()
    {
        abletogo = true;
        cango.text = "信号：進行";
    }

    private void ats()
    {
        if (Vector3.Distance(agent.transform.position, atss[atsindex].transform.position) < 10)
        {
            if (atssignal[atsindex] == -2)
            {
                atsspeed.text = "速度制限解除";
            }
            else
            {
                atsspeed.text = "速度制限:" + atssignal[atsindex];
            }
            atschange.Play();
            atsindex++;

        }

    }

    private void runhousou()
    {
        if (nowdis - stadis > 5 && housouA == true)
        {
            if (staindex == 0)
            {
                housou.clip = kitaikebukuroA;
                housou.Play();
            }
            else if (staindex == 1)
            {
                housou.clip = shimoitabashiA;
                housou.Play();
            }
            housouA = false;
        }

        if (stadis < 300 && housouB == true)
        {
            if (staindex == 0)
            {
                housou.clip = kitaikebukuroB;
                housou.Play();
            }
            else if (staindex == 1)
            {
                housou.clip = shimoitabashiB;
                housou.Play();
            }
            housouB = false;
        }

    }





    void Update()
    {
        if (agent.speed == 0)
        {
            audioSource.Stop();
            EBsound.Stop();

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index < 12)
            {
                index++;
                setacc(index);

            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index > 0)
            {
                index--;
                setacc(index);

            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            chime.Play();
        }

        if (agent.speed == 0)
        {
            audioSource.Pause();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (agent.speed==0.0f)
            {
                if (dooropen == true&&abletogo == true )
                {
                    door.clip = closethedoor;
                    door.Play();
                    dooropen = false;
                    doorstatus.text = "ドア状態：閉めている";
                }
                else if(dooropen == false)
                {
                    door.clip = openthedoor;
                    door.Play();
                    dooropen = true;
                    doorstatus.text = "ドア状態：開いている";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            jokohousou.Play();
        }


        ats();
        runhousou();
        //int layer1 = LayerMask.NameToLayer("Rail");
        //int layer2 = LayerMask.NameToLayer("Player");
        //Physics.IgnoreLayerCollision(layer1, layer2,true);


    }

   
}