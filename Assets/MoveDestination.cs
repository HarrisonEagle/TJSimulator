using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class MoveDestination : MonoBehaviour
{

    private NavMeshAgent agent;

    private bool isrun = false;
    private float accpos = 0.0f;
    private float dccpos = 0.0f;
    public AudioClip acc;
    public AudioClip dcc;
    public AudioClip rapidcc;
    public AudioClip maxcc;
    public AudioClip memoria;
    public GameObject test1;
    public GameObject test2;
    public int syubetsu;//1.Local2.SemiExp3.Exp4.Rapid5.RapidExp6.TJ
    public AudioClip kitaikebukuroA;
    public AudioClip kitaikebukuroB;
    public AudioClip shimoitabashiA;
    public AudioClip shimoitabashiB;
    public AudioClip ooyamaA;
    public AudioClip ooyamaB;
    public AudioClip nakaitabashiA;
    public AudioClip nakaitabashiB;
    public AudioClip tokiwadaiA;
    public AudioClip tokiwadaiB;
    public AudioClip kamiitabashiA;
    public AudioClip kamiitabashiB;
    public AudioClip tobunerimaA;
    public AudioClip tobunerimaB;
    public AudioClip shimoakatsukaA;
    public AudioClip shimoakatsukaB;
    public AudioClip narimasuALocal;
    public AudioClip NarimasuA;
    public AudioClip NarimasuB;
    public GameObject kitaikebukuro;
    public GameObject shimoitabashi;
    public GameObject ooyama;
    public GameObject nakaitabashi;
    public GameObject tokiwadai;
    public GameObject kamiitabashi;
    public GameObject tobunerima;
    public GameObject shimoakatsuka;
    public GameObject narimasu;
    public GameObject wakoshi;
    public GameObject asaka;
    public GameObject ikebukuro;
    public GameObject obj;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;
    public GameObject obj8;
    public GameObject obj9;

    //踏切関連
    public AudioSource crosssound;
    private List<GameObject> crosses = new List<GameObject>();
    public GameObject fumikiri;
    public GameObject fumikiri1;
    public GameObject fumikiri2;
    public GameObject fumikiri3;
    public GameObject fumikiri4;
    bool crossflag = true;
    int fumikiriindex = 0;

    private List<GameObject> housous = new List<GameObject>();
    private List<GameObject> atss = new List<GameObject>();
    private List<GameObject> points = new List<GameObject>();
    private int[] atssignal = { 50,100,55,100,65,75,65,120,75,120};//-1:,-2:速度制限解除,-3:終点放送,-4ポイント通過
    private int housouindex = 0;
    private int[] yoteis = {46920,47040,47220,47400,47580,47820,48000,48180,48360 };
    private int yoteiindex = 0;
    private bool tj02A = true;
    private bool tj02B = true;
    private bool tj03A = true;
    private bool tj03B = true;
    private bool housouA = true;
    private bool housouB = true;
    private bool housouH = true;
    private bool flag = false;
    private bool pointflag = true;
    public AudioSource audioSource;
    public AudioSource housou;
    public AudioSource breakoff;
    private bool abletogo = false;
    private bool dooropen = true;
    private float nowdis = 0;
    public Text speed;
    public Text mode;
    public Text distance;
    public Text cango;
    public Text atsspeed;
    public Text doorstatus;
    public Text Timetext;
    public Text yotei;
    public Text tyakudan;
    public AudioClip EBhousou;
    public AudioSource notch;
    public AudioClip changenotch;
    public AudioSource atschange;
    public AudioSource chime;
    public AudioSource jokohousou;
    public AudioSource door;
    public AudioClip openthedoor;
    public AudioClip closethedoor;
    public AudioSource laststop;
    public AudioSource pointpass;
    public AudioSource hanyoumelody;

    public AudioSource Pass;

    public GameObject tobu8000;
    public bool passed8000 = false;
    public GameObject tobu30000;
    public bool passed30000 = false;

    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    public GameObject point5;
    public GameObject point6;
    public GameObject point7;
    public GameObject point8;
    public GameObject point9;
    public GameObject point10;
    public GameObject point11;
    public GameObject point12;

    private float time = 0;
    private float stadis = 0.0f;
    private float speedbefore = 0.0f;
    private bool fullrun = false;
    private string[] stationname = {"北池袋","下板橋","大山","中板橋","ときわ台","上板橋","東武練馬","下赤塚","成増","和光市","朝霞","朝霞台","志木" };
    bool[] stop;
    
    private int staindex = 0;
    private int atsindex = 0;
    private bool approachstation = false;
    private bool less02 =false;

    int timenow = 46730;
    int init = 46730;
    int pointindex = 0;

    private int starttime;
    private int now;
    private int duration;


    private int beforeindex;

    public Transform goal;

    public float accelerate = 3.2f;

    public int index = 8;

    public void setacc(int index)//加速モードとモーター音

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
                audioSource.time = acc.length * ((agent.speed * 3.6f) / 100f);
            }



            audioSource.Play();



        }



        if (index == 12)
        {
            beforeindex = 12;
            notch.Play();
            audioSource.loop = false;
            accelerate = 2.4f;//P4
            mode.text = "Mode:P4";
            audioSource.pitch = 0.8f;
            fullrun = false;


        }
        else if (index == 11)
        {
            beforeindex = 11;
            notch.Play();
            audioSource.loop = false;
            accelerate = 1.8f;//P3
            mode.text = "Mode:P3";
            if (agent.speed <= 95)
            {
                fullrun = false;
                audioSource.pitch = 0.8f;
            }
        }
        else if (index == 10)
        {
            beforeindex = 10;
            notch.Play();
            audioSource.loop = false;
            accelerate = 1.3f;//P2
            mode.text = "Mode:P2";

            fullrun = false;
            audioSource.pitch = 0.8f;

        }
        else if (index == 9)
        {
            beforeindex = 9;
            notch.Play();
            audioSource.loop = false;

            fullrun = false;
            audioSource.pitch = 0.8f;

            mode.text = "Mode:P1";
            accelerate = 0.9f;//P1
        }
        else if (index == 8)
        {
            if (beforeindex == 7 && agent.speed * 3.6 <= 20)
            {
                breakoff.Play();
            }
            beforeindex = 8;
            notch.Play();
            audioSource.pitch = 0.65f;
            audioSource.Pause();
            if (agent.speed != 0)
            {
                if (agent.speed*3.6>=100.0f)
                {
                    audioSource.clip = maxcc;
                }
                else
                {
                    audioSource.clip = rapidcc;
                }
                audioSource.loop = true;
                audioSource.Play();
            }

            mode.text = "Mode:N";
            accpos = agent.speed / 120;
            accelerate = -0.01f;//N
        }
        else if (index == 7)
        {
            
            beforeindex = 7;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B1";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -0.3f;//B1
        }
        else if (index == 6)
        {
            beforeindex = 6;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B2";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -0.9f;//B2
        }
        else if (index == 5)
        {
            beforeindex = 5;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B3";
            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -1.5f;//B3
        }
        else if (index == 4)
        {

            beforeindex = 4;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B4";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -2.1f;//B4
        }
        else if (index == 3)
        {
            beforeindex = 3;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B5";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -2.8f;//B5
        }
        else if (index == 2)
        {
            beforeindex = 2;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B6";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -3.6f;//B6
        }
        else if (index == 1)
        {
            beforeindex = 1;
            notch.Play();
            audioSource.loop = false;
            mode.text = "Mode:B7";

            fullrun = false;
            audioSource.pitch = -0.6f;

            accelerate = -4.2f;//B7
        }
        else if (index == 0)
        {
            beforeindex = 0;
            notch.Play();
            if (agent.speed!=0.0f)
            {
                housou.clip = EBhousou;
                housou.Play();
            }
            audioSource.loop = false;
            mode.text = "Mode:EB";

            fullrun = false;
            audioSource.pitch = -2.0f;

            accelerate = -5.3f;//EB

        }

    }

   
    IEnumerator FuncCoroutine()
    {
        while (true)
        {

            
            if (dooropen==false)
            {
                if ((agent.speed + (0.01f * accelerate)) * 3.6 < 160)
                {
                    agent.speed += (0.01f * accelerate);
                }
                else if ((agent.speed + (0.01f * accelerate)) * 3.6 >= 160)
                {
                    //accelerate = 8;
                    audioSource.pitch = 0.65f;
                    audioSource.Pause();
                    audioSource.clip = maxcc;
                    audioSource.loop = true;
                    audioSource.Play();

                    //mode.text = "Mode:N";
                    accpos = agent.speed / 120;

                }

            }
            stadis = Vector3.Distance(agent.transform.position, housous[staindex].transform.position) - 0.5f;


            speedbefore = agent.speed;
            speed.text = "Speed:" + agent.speed * 3.6 + "km/h";

            string status = detectstop();

            distance.text = stationname[staindex] +status + stadis;
            // 何か処理
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void changeyotei()
    {
        int hour = (yoteis[yoteiindex] - yoteis[yoteiindex] % 3600) / 3600;
        int temp = yoteis[yoteiindex] % 3600;
        int minute = (temp - temp % 60) / 60;
        int second = temp % 60;
        yotei.text = "到着予定時刻:"+hour+":"+minute+":"+second;
       
    }

    void Start()
    {


        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        Invoke("shingo", 15.0f);

        atss.Add(obj);
        atss.Add(obj1);
        atss.Add(obj2);
        atss.Add(obj3);
        atss.Add(obj4);
        atss.Add(obj5);
        atss.Add(obj6);
        atss.Add(obj7);
        atss.Add(obj8);
        atss.Add(obj9);



        if (syubetsu == 0)
        {
            bool[] sstop = { true, true, true, true, true, true, true, true, true, true, true, true, true };
            stop = sstop;
        }
        else if(syubetsu == 4)
        {
            bool[] sstop = { false, false, false, false, false, false, false, false, true, true, false, true, true };
            stop = sstop;
        }

        housous.Add(kitaikebukuro);
        housous.Add(shimoitabashi);
        housous.Add(ooyama);
        housous.Add(nakaitabashi);
        housous.Add(tokiwadai);
        housous.Add(kamiitabashi);
        housous.Add(tobunerima);
        housous.Add(shimoakatsuka);
        housous.Add(narimasu);
        housous.Add(wakoshi);
        //housous.Add(ikebukuro);

        crosses.Add(fumikiri);
        crosses.Add(fumikiri1);
        crosses.Add(fumikiri2);
        crosses.Add(fumikiri3);
        crosses.Add(fumikiri4);

        points.Add(point1);
        points.Add(point2);
        points.Add(point3);
        points.Add(point4);
        points.Add(point5);
        points.Add(point6);
        points.Add(point7);
        points.Add(point8);
        points.Add(point9);
        points.Add(point10);
        points.Add(point11);
        points.Add(point12);



        stadis = Vector3.Distance(agent.transform.position, housous[0].transform.position);
        nowdis = stadis;
        StartCoroutine(FuncCoroutine());

        changeyotei();
        



    }

    void shingo()
    {

        housouH = true;
        housouA = true;
        housouB = true;
        abletogo = true;
            cango.text = "信号：進行";
        cango.color = Color.green;
        

    }

    private void ats()//速度制限
    {
        if (Vector3.Distance(agent.transform.position, atss[atsindex].transform.position) < 5)
        {
            if (atssignal[atsindex] == -2)
            {
                atsspeed.text = "速度制限解除";
            }
            else if (atssignal[atsindex] == -3)
            {
                laststop.Play();
            }
            else if (atssignal[atsindex] == -4)
            {
                pointpass.Play();
            }
            else
            {
                atsspeed.text = "速度制限:" + atssignal[atsindex];
            }
            atschange.Play();
            atsindex++;

        }

    }

    private string detectstop() //停車駅判定
    {
        if (stadis < 2.0 && stop[staindex] == false)
        {
            staindex++;
            nowdis = Vector3.Distance(agent.transform.position, housous[staindex].transform.position);
            housouA = true;
            housouB = true;
        }
        else if(stop[staindex] == true)
        {
            if (stadis<200f&& approachstation == false)
            {
                approachstation = true;
                distance.color = new Color(255f / 255f, 241f / 255f, 0f / 255f);
                
            }
            else if (stadis<=3f)
            {
                less02 = true;
                if (agent.speed == 0.0f)
                {
                    if (staindex==housous.Count-1)
                    {
                        housouA = true;
                        housouB = true;
                        abletogo = false;
                        cango.text = "信号：停止";
                        return "終点　全線走破";
                    }
                    else
                    {
                        
                        staindex++;
                        nowdis = Vector3.Distance(agent.transform.position, housous[staindex].transform.position);
                        stadis = Vector3.Distance(agent.transform.position, housous[staindex].transform.position) - 0.5f;

                        if (timenow>yoteis[yoteiindex])//遅延
                        {
                            tyakudan.text = "遅延"+(timenow- yoteis[yoteiindex])+"s";
                        }
                        else if(timenow - yoteis[yoteiindex]<=5.0f&& timenow - yoteis[yoteiindex] >= -5.0f)
                        {
                            tyakudan.text = "定刻";
                        }else if (timenow < yoteis[yoteiindex])//早着
                        {
                            tyakudan.text = "早着"+(yoteis[yoteiindex] - timenow) +"s";
                        }

                        yoteiindex++;
                        changeyotei();

                        abletogo = false;
                        cango.text = "信号：停止";
                        cango.color = Color.red;

                        approachstation = false;
                        less02 = false;
                        distance.color = new Color(51f / 255f, 51f / 255f, 51f / 255f);

                        if (stop[staindex]==false)
                        {
                            return "通過　まで後：";
                        }
                        else{
                            return "停車　まで後：";
                        }
                    }
                }
                return "合格範囲";

            }
            else if (approachstation == true&&less02==true&&stadis>0.5f)
            {
                distance.color = new Color(255f / 255f, 1f / 255f, 0f / 255f);
                return "オーバーラン！";
            }
        }

        if (stop[staindex] == true)
        {
            return "停車　まで後：";
        }
        else
        {
            return "通過まで後：";
        }




    }

    public void detectpoint()
    {

        float distance = Vector3.Distance(agent.transform.position, points[pointindex].transform.position);

        Debug.Log(distance);

      if (distance <= 10.0f)
        {
            
            if (pointpass.isPlaying == false)
            {
                pointpass.Play();
            }
            pointindex++;
        }
    }

    private void runhousou()
    {
        if (nowdis - stadis > 10 && housouA == true)
        {
            if (syubetsu==0)
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
                else if (staindex == 2)
                {
                    housou.clip = ooyamaA;
                    housou.Play();
                }
                else if (staindex == 3)
                {
                    housou.clip = nakaitabashiA;
                    housou.Play();
                }
                else if (staindex == 4)
                {
                    housou.clip = tokiwadaiA;
                    housou.Play();
                }
                else if (staindex == 5)
                {
                    housou.clip = kamiitabashiA;
                    housou.Play();
                }
                else if (staindex == 6)
                {
                    housou.clip = tobunerimaA;
                    housou.Play();
                }
                else if (staindex == 7)
                {
                    housou.clip = shimoakatsukaA;
                    housou.Play();
                }
                else if (staindex == 8)
                {
                    housou.clip = narimasuALocal;
                    housou.Play();
                }
            }
            else if (syubetsu == 4)
            {
                if (staindex == 0)
                {
                    housou.clip = NarimasuA;
                    housou.Play();
                }
            }
            housouindex++;
            housouA = false;
            return;
        }

        if (stadis < 250 && housouB == true && stop[staindex]==true)
        {
            if (syubetsu == 0)
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
                else if (staindex == 2)
                {
                    housou.clip = ooyamaB;
                    housou.Play();
                }
                else if (staindex == 3)
                {
                    housou.clip = nakaitabashiB;
                    housou.Play();
                }
                else if (staindex == 4)
                {
                    housou.clip = tokiwadaiB;
                    housou.Play();
                }
                else if (staindex == 5)
                {
                    housou.clip = kamiitabashiB;
                    housou.Play();
                }
                else if (staindex == 6)
                {
                    housou.clip = tobunerimaB;
                    housou.Play();
                }
                else if (staindex == 7)
                {
                    housou.clip = shimoakatsukaB;
                    housou.Play();
                }
                else if (staindex == 8)
                {
                    housou.clip = NarimasuB;
                    housou.Play();
                }
            }
            else if (syubetsu == 4)
            {
                if (staindex == 8)
                {
                    housou.clip = NarimasuB;
                    housou.Play();
                }
            }
            housouindex++;
            housouH = false;
            housouB = false;
        }

    }


    private void detectcross()
    {

        float distance = Vector3.Distance(agent.transform.position,crosses[fumikiriindex].transform.position);

        if (distance>10.0f&&crossflag==false)
        {
            crossflag = true;
            crosssound.Stop();
            if (fumikiriindex+1<=crosses.Count-1)
            {
                fumikiriindex++;
            }
        }else if (distance<=10.0f&&crossflag==true)
        {
            crossflag = false;
            if (crosssound.isPlaying==false)
            {
                crosssound.Play();
            }
        }
    }




    void Update()
    {
        if (agent.speed == 0)
        {
            audioSource.Stop();
            

        }
        if (agent.speed!= 0)
        {
            tyakudan.text = "";
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
                    agent.speed = 0.0f;
                    doorstatus.text = "ドア状態：閉";
                    doorstatus.color = Color.blue;
                }
                else if(dooropen == false)
                {
                    door.clip = openthedoor;
                    door.Play();
                    dooropen = true;
                    doorstatus.text = "ドア状態：開";
                    doorstatus.color = Color.red;
                    if (abletogo==false)
                    {
                        hanyoumelody.Play();
                        Invoke("shingo", 10.0f);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (agent.speed==0.0f)
            {
                jokohousou.Play();
            }
        }

        timenow = init + (int)Time.time;
        TimeSpan result = TimeSpan.FromHours(timenow);
        int hour = (timenow - timenow % 3600) / 3600;
        int temp = timenow % 3600;
        int minute = (temp - temp % 60) / 60;
        int second = temp % 60;
        Timetext.text = "現在時刻:"+hour+":"+minute+":"+second;

        detectpoint();
        detectcross();
        ats();

        if (Vector3.Distance(agent.transform.position,tobu8000.transform.position)<=25.0f&&passed8000==false)
        {
            passed8000 = true;
            Pass.Play();
        }
        if (Vector3.Distance(agent.transform.position, tobu30000.transform.position) <= 25.0f && passed30000 == false)
        {
            passed30000 = true;
            Pass.Play();
        }

        if (agent.speed >= 15.0f)
        {
           cango.text = "";
        }

        if (housouH==true)
        {
            runhousou();
        }
        //int layer1 = LayerMask.NameToLayer("Rail");
        //int layer2 = LayerMask.NameToLayer("Player");
        //Physics.IgnoreLayerCollision(layer1, layer2,true);


    }

   
}