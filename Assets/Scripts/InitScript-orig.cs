using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class InitScriptorig : MonoBehaviour {
	
	//string txtmuch = "45";
	string txtmuchx = "3";
	string txtmuchy = "5";
	string txtx = "2";
	string txty = "1";
	string txtsay = "Your gambols, your songs, your flashes of merriment, that were wont to set the table on a roar? Not one now to mock your own grinning? Quite chop-fallen.  Now get you to my lady's chamber, and tell her, let her paint an inch thick, to this favour she must come. Make her laugh at that.";
	string txtforward = "3";
	
	// for mode dropdowns
	private Vector2 scrollViewVector = Vector2.zero;
    public Rect dropDownRect = new Rect(125,50,125,300);
    public static string[] list = {"Choose Mode:","Baseline", "Random", "NLP", "Rules", "FDG"};
    int indexNumber;
    bool show = false;
	public enum playmodes { baseline, random, nlp, rules, fdg };
	public static playmodes mode = playmodes.baseline;
	public static bool runshort = false;
	
	float timer = 0.0f;
	float timerMax = 1.0f; // reset to 5 when working
	bool starting = false;
	
	// for file reading
	static char quote = System.Convert.ToChar (34);
	//StreamWriter[] charFiles = null;
	public static bool started = false;
	static string path = @"";
	static string inputFileName = Application.dataPath + @"//Files//InputFile.txt";
	static string bmlFileName = Application.dataPath + @"//Files//BMLFile.txt";
	static string miniinputFileName = Application.dataPath + @"//Files//miniInputFile.txt";
	static string minibmlFileName = Application.dataPath + @"//Files//miniBMLFile.txt";
	static StreamReader inputFile = null;
	
	// variables for legend
	public  Texture hamletT;
	public  Texture horatioT;
	public  Texture gravediggerT;
	public  Texture gravediggertwoT;
	public  Texture lanternT;
	public  Texture shovelT;
	public  Texture skull1T;
	public  Texture skull2T;
	public  Texture legendBkgrd;
	
	
	public float startx1 = 5f;
	public float startx2 = 110f;
	public float starty = 80f;
	public float widthtext = 90f;
	public float widthimg = 85f;
	public float heighttext = 30f;
	public float heightimg = 135f;
	public float startximg1 = 5f;
	public float startximg2 = 100f;
	public float startyimg = 100f;
	public float spacing;
	public float linex = .7f;
	public float liney = .5f;
	public Material mat;
	
	static bool intermission = false;
	static Color mycolor;
	static float itimer = 0.0f;
	static float itimerMax = 7.0f;
	static int inum = -1;
	static Texture2D mytexture;
	static Texture2D mytexture2;
	static Texture2D mytexture3;
	static GUIStyle newstyle;
	
	static bool wait = false;
	static float wtimer = 0.0f;
	static float wtimerMax = 1.0f;
	
	static float pauseamt = 0.0f;
	static float pausemax = 5.0f;

	// Use this for initialization
	void Start () {
		spacing = heightimg+heighttext+5f;
		mycolor = GUI.backgroundColor;
		mytexture = new Texture2D(Screen.width, Screen.height); // orange
		int y = 0;
        while (y < mytexture.height) {
            int x = 0;
            while (x < mytexture.width) {
                //Color color = ((x & y) ? Color.white : Color.gray);
                mytexture.SetPixel(x, y, new Color(255f/255f, 127f/255f, 0f/255f));//new Color(51f/255f, 178f/255f, 146f/255f)); // turquoise
                ++x;
            }
            ++y;
        }
        mytexture.Apply();
		mytexture2 = new Texture2D(Screen.width, Screen.height); // brown
		y = 0;
        while (y < mytexture2.height) {
            int x = 0;
            while (x < mytexture2.width) {
                //Color color = ((x & y) ? Color.white : Color.gray);
                mytexture2.SetPixel(x, y, new Color(89f/255f, 64f/255f, 39f/255f));//new Color(51f/255f, 178f/255f, 146f/255f)); // turquoise
                ++x;
            }
            ++y;
        }
        mytexture2.Apply();
		
		mytexture3 = new Texture2D(Screen.width, Screen.height); // yellow
		y = 0;
		while (y < mytexture3.height) {
			int x=0;
			while (x < mytexture3.width) {
				mytexture3.SetPixel(x, y, Color.yellow);
				++x;
			}
			++y;
		}
		mytexture3.Apply();
		
		newstyle = new GUIStyle();
		newstyle.normal.background = mytexture;
		newstyle.fontSize = 30;
		newstyle.normal.textColor = Color.black;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (starting) {
			timer += Time.deltaTime;
			if (timer >= timerMax) {
				// ready to start
				RunPlay ();
				timer = 0.0f;
				starting = false;
			}
		}
		//if (started && GlobalObjs.globalQueue.Count == 0) {
		//	callNextStep();
		//}
		if (intermission) {
			itimer += Time.deltaTime;
			if (itimer > itimerMax) {
				intermission = false;
				wait = true;
				itimer = 0.0f;
				//Debug.Log ("Removing inum="+inum);
				//GlobalObjs.removeOne(inum);
				//inum = -1;
			}
		}
		if (wait) {
			wtimer +=Time.deltaTime;
			if (wtimer > wtimerMax) {
				Debug.Log ("Removing inum="+inum);
				GlobalObjs.removeOne(inum);
				inum = -1;	
				wtimer = 0.0f;
				wait = false;
			}
		}
	}
	
	void OnGUI() {
		
		if (intermission) {
			// show blue screen for intermission with text
			//GUIStyle newstyle = new GUIStyle();
			//newstyle.normal.background = new Texture2D(Screen.width, Screen.height);
			//GUI.backgroundColor = Color.blue;
			//GUI.Box (new Rect(0,0,Screen.width, Screen.height), "", newstyle);
			if (indexNumber == 1 || indexNumber == 0) {
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mytexture, ScaleMode.ScaleToFit, false, 0);
				GUI.Label (new Rect((Screen.width/2) - 130, (Screen.height/2) + 60, Screen.width, 50), "This screen is orange", newstyle);
			} else if (indexNumber == 2) {
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mytexture3, ScaleMode.ScaleToFit, false, 0);
				GUI.Label (new Rect((Screen.width/2) - 130, (Screen.height/2) + 60, Screen.width, 50), "This screen is yellow", newstyle);
			} else {
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mytexture2, ScaleMode.ScaleToFit, false, 0);
				GUI.Label (new Rect((Screen.width/2) - 120, (Screen.height/2) + 60, Screen.width, 50), "This screen is brown", newstyle);
			}
			GUI.Label (new Rect((Screen.width/2) - 85, (Screen.height/2) - 120, Screen.width, 50), "INTERMISSION", newstyle);
			
			
		} else {
			GUI.backgroundColor = mycolor;
			// legend
			GUI.BeginGroup(new Rect(1200, -3, 200, 900));
				GUI.Box (new Rect(0,-3, 200,900), legendBkgrd);
			
				GUIStyle mystyle = new GUIStyle();
				mystyle.fontSize = 30;
				mystyle.normal.textColor = Color.white;
				GUI.Label (new Rect(20, startx1+20, widthtext*2, heighttext*2), "LEGEND", mystyle);
			
				GUI.Label (new Rect(startx2, starty, widthtext, heighttext), "Hamlet:");
				GUI.Label(new Rect(startximg2, startyimg,widthimg,heightimg), new GUIContent(hamletT));
				
				GUI.Label (new Rect(startx2, starty+(spacing*1), widthtext, heighttext), "Horatio:");
				GUI.Label(new Rect(startximg2, startyimg+(spacing*1),widthimg,heightimg), new GUIContent(horatioT));
			
				GUI.Label (new Rect(startx1, starty, widthtext, heighttext), "GraveDigger 1:");
				GUI.Label(new Rect(startximg1, startyimg,widthimg,heightimg), new GUIContent(gravediggerT));
			
				GUI.Label (new Rect(startx1, starty+(spacing*1), widthtext, heighttext), "GraveDigger 2:");
				GUI.Label(new Rect(startximg1, startyimg+(spacing*1),widthimg,heightimg), new GUIContent(gravediggertwoT));
			
				GUI.Label (new Rect(startx1, starty+(spacing*2), widthtext*2, heighttext*2), "--------------------------------------------");
			
				GUI.Label (new Rect(startx1, starty+30+(spacing*2), widthtext, heighttext), "Shovel:");
				GUI.Label(new Rect(startximg1, startyimg+30+(spacing*2),widthimg,heightimg*2), new GUIContent(shovelT));
			
				GUI.Label (new Rect(startx1, starty+30+(spacing*2.5f), widthtext, heighttext), "Lantern:");
				GUI.Label(new Rect(startximg1, startyimg+30+(spacing*2.5f),widthimg,heightimg*2), new GUIContent(lanternT));
			
				GUI.Label (new Rect(startx2, starty+30+(spacing*2), widthtext, heighttext), "Skull 1:");
				GUI.Label(new Rect(startximg2, startyimg+30+(spacing*2),widthimg,heightimg), new GUIContent(skull1T));
			
				GUI.Label (new Rect(startx2, starty+30+(spacing*2.5f), widthtext, heighttext), "Skull 2:");
				GUI.Label(new Rect(startximg2, startyimg+30+(spacing*2.5f),widthimg,heightimg), new GUIContent(skull2T));
			
			
			GUI.EndGroup();
			//GUI.DrawTexture(new Rect(100,60, 50,50), hamletT, ScaleMode.ScaleToFit, true, 0);
			// end legend
			
			if (started) {
				// show nothing
			} else {
				//txtmuch = GUI.TextField(new Rect(780, 30, 40, 30), txtmuch, 4);
				txtmuchx = GUI.TextField (new Rect(780, 30, 40, 30), txtmuchx, 4);
				txtmuchy = GUI.TextField (new Rect(830, 30, 40, 30), txtmuchy, 4);
				if (GUI.Button(new Rect(500,30,250,30),"Click to Rotate Hamlet")) {
					float howmuchx;
					float howmuchy;
					bool success = float.TryParse(txtmuchx, out howmuchx);
					success = float.TryParse (txtmuchy, out howmuchy);
					GlobalObjs.HamletFunc.doRotate(howmuchx, howmuchy, null);
					Debug.Log("Clicked the button to rotate");	
				}
				txtx = GUI.TextField (new Rect(780, 70, 40, 30), txtx, 4);
				txty = GUI.TextField (new Rect(830, 70, 40, 30), txty, 4);
				if (GUI.Button (new Rect(500, 70, 250, 30), "Click to Move Hamlet FWD")) {
					float x;
					float y;
					bool success = float.TryParse (txtx, out x);
					success = float.TryParse (txty, out y);
					GlobalObjs.HamletFunc.doWalk(x, y, null, false);
					Debug.Log ("Clicked the button to walk");
				}
				txtsay = GUI.TextField (new Rect(780, 110, 100, 30), txtsay, 100);
				if (GUI.Button (new Rect(500, 110, 250, 30), "Speak")) {
					GlobalObjs.HamletFunc.doSpeak(txtsay);
					Debug.Log ("Said something");
				}
				if (GUI.Button (new Rect(500, 150, 250, 30), "Stop")) {
					GlobalObjs.HamletFunc.doStopAll();
					Debug.Log ("Stopped everything");
				}
				txtforward = GUI.TextField (new Rect(780, 190, 100, 30), txtforward, 4);
				if (GUI.Button (new Rect(500, 190, 250, 30), "Move Forward")) {
					float thisamt;
					bool success = float.TryParse (txtforward, out thisamt);
					GlobalObjs.HamletFunc.doForward(thisamt);
					Debug.Log ("Moved forward "+thisamt);
				}
				if (GUI.Button (new Rect(125, 150, 100, 30), "Pickup")) {
					Debug.Log ("Pickup");
					//shrinking = true;
					GlobalObjs.HamletFunc.doPickup(GlobalObjs.Lantern);//.animation.Play("Shrink");
				}
				if (GUI.Button (new Rect(125, 190, 100, 30), "Putdown")) {
					Debug.Log ("Putdown");
					GlobalObjs.HamletFunc.doPutDown(GlobalObjs.Lantern);
				}
				if (GUI.Button (new Rect(125, 230, 100, 30), "Follow")) {
					Debug.Log ("Following");
					GlobalObjs.GraveDiggerFunc.doWalk (GlobalObjs.Grave.transform.position.x, GlobalObjs.Grave.transform.position.z, GlobalObjs.Grave, false);
					GlobalObjs.GraveDiggerTwoFunc.doWalk (GlobalObjs.GraveDigger.transform.position.x, GlobalObjs.GraveDigger.transform.position.z, GlobalObjs.GraveDigger, true);
				}
				if (GUI.Button (new Rect(125, 270, 100, 30), "Point")) {
					Debug.Log ("Pointing");
					GlobalObjs.HamletFunc.doPoint (GlobalObjs.Skull1);
				}
				if (GUI.Button(new Rect(125, 310, 100, 30), "Check Visible")) {
					Debug.Log ("Checking if Grave is visible");
					GlobalObjs.HamletFunc.moveTo = GlobalObjs.Grave.transform.position;
					Debug.Log ("Grave="+GlobalObjs.Grave.transform.position+", Hamlet="+GlobalObjs.Hamlet.transform.position);
					Debug.Log (GlobalObjs.HamletFunc.isVisible());
				}
				if (GUI.Button (new Rect(125, 350, 100, 30), "Look at")) {
					Debug.Log ("Look at Grave");
					GlobalObjs.GraveDiggerFunc.doRotate(GlobalObjs.Grave.transform.position.x, GlobalObjs.Grave.transform.position.z, GlobalObjs.Grave);
				}
				if (GUI.Button (new Rect(125, 390, 100, 30), "Intermission")) {
					Debug.Log ("Start Intermission");
					intermission = true;
					QueueObj temp = new QueueObj(null, null, new Vector3(0,0,0), QueueObj.actiontype.intermission);
					inum = temp.msgNum;
					GlobalObjs.globalQueue.Add(temp);
					Debug.Log ("Starting inum="+inum);
				}
				if (GUI.Button (new Rect(125, 470, 100, 30), "Run Short Version")) {
					runshort = !runshort;
					Debug.Log ("Run Short="+runshort);
				}
				if (GUI.Button (new Rect(125, 430, 100, 30), "Long Speech")) {
					Debug.Log ("Saying long message");
					GlobalObjs.HamletFunc.doSpeak("He hath borne me on his back a thousand times,and now how abhorred in my imagination it is--my gorge rises at it. Here hung those lips that I have kissed I know not how oft.Where be your gibes now? Your gambols, your songs, your flashes of merriment, that were wont to set the table on a roar? No tone now to mock your own grinning? Quite chop-fallen.");
					Debug.Log ("Done long message");
				}
				
				//bool useBML = GUI.Toggle(new Rect(500, 30, 100, 30), BML, "Use BML File?");
				if (GUI.Button (new Rect(25, 20, 100, 30), "Start Play")) {
					Debug.Log ("Starting Play "+Time.time);	
					//RunPlay();
					starting = true;
					timer = 0.0f;
				}
				
				
				// shows dropdown to choose what to run
				if(GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
		        {
		            if(!show)
		            {
		                show = true;
		            }
		            else
		            {
		                show = false;
		            }
		        }
		        if(show)
		        {
		            scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height),scrollViewVector,new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length*25))), "");           
		            for(int index = 0; index < list.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownRect.height, 25), ""))
		                {
		                    show = false;
		                    indexNumber = index;
							Debug.Log("Index="+indexNumber);
							if (index == 0 || index == 1) {
								newstyle.normal.background = mytexture;
								newstyle.normal.textColor = Color.black;
							} else {
								newstyle.normal.background = mytexture2;
								newstyle.normal.textColor = Color.white;
							}
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownRect.height, 25), list[index]);               
		            }
		            GUI.EndScrollView();   
		        }
		        else
		        {
		            GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), list[indexNumber]);
		        }
			}
		}
       
	}
	
	

	
	void RunPlay() {
		// check Mode & run based on that setting
		// use indexNumber, where 1=Baseline, 2=BML, etc
		
		Debug.Log ("Run in mode #"+indexNumber);
		starting = false;
		started = true;
		// need to add logic to do different actions based on mode chosen!!
		switch (indexNumber) {
		case 0: // baseline -- by default if don't choose or if click choose mode

			mode = playmodes.baseline;
			newstyle.normal.background = mytexture;
			newstyle.normal.textColor = Color.black;
			if (runshort) {
				inputFile = File.OpenText (miniinputFileName);
				GlobalObjs.Hamlet.transform.position = new Vector3(4.2f, 0f, 37f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, -1f, 0f, 0f);
				GlobalObjs.Horatio.transform.position = new Vector3(.9f, 0f, 35.3f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, .9f, 0f, -.5f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(16f, 0f, 34f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, -1f, 0f, -.1f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(54f, 0f, 49.9f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, .7f, 0f, -.7f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
			} else {
				inputFile = File.OpenText(inputFileName);
			}
			break;
		case 2: // random
			mode = playmodes.random;
			newstyle.normal.background = mytexture3;
			newstyle.normal.textColor = Color.black;
			if (runshort) {
				// place chars randomly
				GlobalObjs.Hamlet.transform.position = new Vector3(-7.9f, 0f, .8f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, -1f, 0f, -1f);
				GlobalObjs.Horatio.transform.position = new Vector3(-33f, 0f, 44.8f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(18.1f, 0f, 4.5f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, -.5f, 0f, .9f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(6f, 0f, -4f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, 1f, 0f, .1f);
				
				GlobalObjs.GraveDiggerFunc.doPickup(GlobalObjs.Skull2);
				GlobalObjs.HamletFunc.doPickup(GlobalObjs.Skull1);
				
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText (minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}
			break;
		case 1: //baseline
			mode = playmodes.baseline;
			newstyle.normal.background = mytexture;
			newstyle.normal.textColor = Color.black;
			if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(4.2f, 0f, 37f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, -1f, 0f, 0f);
				GlobalObjs.Horatio.transform.position = new Vector3(.9f, 0f, 35.3f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, .9f, 0f, -.5f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(16f, 0f, 34f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, -1f, 0f, -.1f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(54f, 0f, 49.9f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, .7f, 0f, -.7f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(miniinputFileName);
			} else {
				inputFile = File.OpenText (inputFileName);
			}
			break;
		case 3: // nlp
			mode = playmodes.nlp;
			newstyle.normal.background = mytexture2;
			newstyle.normal.textColor = Color.white;
			if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(-6.8f, 0f, 42.4f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, .6f, 0f, -.8f);
				GlobalObjs.Horatio.transform.position = new Vector3(-5f, 0f, 39.1f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, 1f, 0f, .2f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(14.3f, 0f, 34.1f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(49.1f, 0f, 50.2f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, -.6f, 0f, .8f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}
			break;
		case 4:
			mode = playmodes.rules;
			if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(-6.8f, 0f, 42.4f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, .6f, 0f, -.8f);
				GlobalObjs.Horatio.transform.position = new Vector3(-5f, 0f, 39.1f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, 1f, 0f, .2f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(14.3f, 0f, 34.1f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(49.1f, 0f, 50.2f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, -.6f, 0f, .8f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}
			break;
		case 5:
			mode = playmodes.fdg;
			if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(-6.8f, 0f, 42.4f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, .6f, 0f, -.8f);
				GlobalObjs.Horatio.transform.position = new Vector3(-5f, 0f, 39.1f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, 1f, 0f, .2f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(14.3f, 0f, 34.1f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(49.1f, 0f, 50.2f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, -.6f, 0f, .8f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}
			break;
		}
		// pause
		pausesome ();
//		callNextStep ();
	}
	
	public static void pausesome() {
		if (pauseamt >= pausemax) {
			
			callNextStep();
		} else {
			pauseamt += Time.deltaTime;
		}
	}
	
	public static void callNextStep() {
		
		string curLine = null;// = inputFile.ReadLine ();
		string[] parsedLine = null;
		bool firstiteration = true;
		
		
		while (firstiteration || (curLine != null && parsedLine[0] != "N")) {
			firstiteration = false;
       		curLine = inputFile.ReadLine ();
	        if (curLine != null) {
	           
	//            currentMessageNum++;
	            parsedLine = curLine.Split ('\t');
	            Debug.Log ("CJT LINE="+curLine);
				//Debug.Log ("First item=" +parsedLine[0]);
	            switch (parsedLine [1]) {
	                case "MOVE":
	                    //Debug.Log ("CJT MESSAGE="+parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
	                    //vhmsg.SendVHMsg ("vrExpress", parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
		                //Debug.Log ("Doing movement for "+parsedLine[2]+" doing:"+parsedLine[4]);	
						if (mode == playmodes.random) {
							doRandomMvmt(parsedLine[2], parsedLine[4]);
						} else {
							parseMovement(parsedLine[2], parsedLine[4]);    
						}
						break;
	                case "SPEAK":
	                    //if (parsedLine [1] == actor) {
	                        // find the speech tags & display only that text, start listening for enter key or mouse click?   
	                    //    showtext = findSpeech (parsedLine [3]);
	                    //} else {
	                        // else send the message to be spoken by the character
	                    //Debug.Log ("CJT MESSAGE="+parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
						
						CharFuncs who = GlobalObjs.getCharFunc(parsedLine[2]);
						string saywhat = findSpeech(parsedLine[4]);
						Debug.Log (who.name+" says: "+saywhat);
						who.doSpeak (saywhat);
						if (mode == playmodes.random) {
							int temp = UnityEngine.Random.Range (0,2);
							if (temp == 1) {
								doRandomMvmt(parsedLine[2], parsedLine[4]);
							}
						}
	                    //vhmsg.SendVHMsg ("vrSpeak", parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
	                    //}
	                    break;
					case "BREAK":
						Debug.Log ("Start Intermission");
						intermission = true;
						QueueObj temp = new QueueObj(null, null, new Vector3(0,0,0), QueueObj.actiontype.intermission);
						inum = temp.msgNum;
						GlobalObjs.globalQueue.Add(temp);
						Debug.Log ("Starting inum="+inum);
						break;
					case "PRINT":
						Debug.Log (Time.time);
						Debug.Log ("Coordinates:");
						Debug.Log ("Hamlet="+GlobalObjs.Hamlet.transform.position+","+GlobalObjs.Hamlet.transform.rotation);
						Debug.Log ("Horatio="+GlobalObjs.Horatio.transform.position+","+GlobalObjs.Horatio.transform.rotation);
						Debug.Log ("GraveDigger="+GlobalObjs.GraveDigger.transform.position+","+GlobalObjs.GraveDigger.transform.rotation);
						Debug.Log ("GraveDigger2="+GlobalObjs.GraveDiggerTwo.transform.position+","+GlobalObjs.GraveDiggerTwo.transform.rotation);
						if (GlobalObjs.Hamlet.transform.childCount != 0) {
							Debug.Log ("Hamlet children=");
							for (int i=0; i< GlobalObjs.Hamlet.transform.childCount; i++) {
								Debug.Log (GlobalObjs.Hamlet.transform.GetChild(i).name);
							}
							Debug.Log ("End Hamlet children");
						}
						if (GlobalObjs.Horatio.transform.childCount != 0) {
							Debug.Log ("Horatio children=");
							for (int i=0; i< GlobalObjs.Horatio.transform.childCount; i++) {
								Debug.Log (GlobalObjs.Horatio.transform.GetChild(i).name);
							}
							Debug.Log ("End Horatio children");
						}
						if (GlobalObjs.GraveDigger.transform.childCount != 0) {
							Debug.Log ("GraveDigger children=");
							for (int i=0; i< GlobalObjs.GraveDigger.transform.childCount; i++) {
								Debug.Log (GlobalObjs.GraveDigger.transform.GetChild(i).name);
							}
							Debug.Log ("End GraveDigger children");
						}
						if (GlobalObjs.GraveDiggerTwo.transform.childCount != 0) {
							Debug.Log ("GraveDiggerTwo children=");
							for (int i=0; i< GlobalObjs.GraveDiggerTwo.transform.childCount; i++) {
								Debug.Log (GlobalObjs.GraveDiggerTwo.transform.GetChild(i).name);
							}
							Debug.Log ("End GraveDiggerTwo children");
						}
						break;
	                default:
	                    // bad command, ignore
					Debug.Log ("Bad command");
	                    break;
	            }
	            //curLine = null;
	            //parsedLine = null;
	        } else {
	            // exit - nothing left to do
	            Debug.Log ("CJT MESSAGE=DONE!!");
	            inputFile.Close ();
	            started = false;
	            inputFile = null;
	            //currentMessageNum = 0;
	           // Application.Quit ();
	        }

		} //while (curLine != null && parsedLine[0] != "N");
		
	}
	
	
	static string findSpeech(string xml) {
        string myText = null;
        int startPos = 0;
        int endPos = 0;
        startPos = xml.IndexOf("application/ssml+xml"+quote+">");
        endPos = xml.IndexOf("</speech>");
        myText = xml.Substring(startPos+22,endPos-startPos-22);
        return myText;
    }
	
	static void doRandomMvmt(string name, string xmltxt) {
		// do a random movement for the current character
		CharFuncs who = GlobalObjs.getCharFunc(name);
		Debug.Log (who.thisChar.name);
		
		float targetx = UnityEngine.Random.Range(-50,51); // x position
			
		float targety = UnityEngine.Random.Range(-5, 70); // y position
		
		int whichfunction = UnityEngine.Random.Range(0, 5); // which character function to run
		
		int objnum;
		if (whichfunction == 2 || whichfunction == 3) {
			objnum = UnityEngine.Random.Range(4, 8); // can only pick up or put down one of these objects
		} else {
			objnum = UnityEngine.Random.Range(0, 18); // which person or object or location to look, rotate or point to
		}
		float temp = Mathf.Floor (UnityEngine.Random.Range(0,2)); 
		bool isobject = (temp == 1)?(true):(false); // whether to use the object or the position for target
		
		float temp2 = Mathf.Floor (UnityEngine.Random.Range(0,2)); // whether char is following or not -- only needed if isobject = true & object is a char
		bool following = (temp2 == 1)?(true):(false);
		bool ischar = false;
		GameObject whichobj = null;
		Debug.Log ("Objnum="+objnum+", whichfunc="+whichfunction+", isobj="+isobject);
		
		if (isobject || whichfunction >=2) {
			switch (objnum) {
				case 0:
					ischar = true;
					whichobj = GlobalObjs.Hamlet;
					break;
				case 1:
					ischar = true;
					whichobj = GlobalObjs.Horatio;
					break;
				case 2:
						ischar = true;
					whichobj = GlobalObjs.GraveDigger;
					break;
				case 3:
					ischar = true;
					whichobj = GlobalObjs.GraveDiggerTwo;
					break;
				case 4:
					ischar = false;
					whichobj = GlobalObjs.Skull1;
					following = false;
					break;
				case 5:
					ischar = false;
					whichobj = GlobalObjs.Skull2;
					following = false;
					break;
				case 6:
					ischar = false;
					whichobj = GlobalObjs.Lantern;
				following = false;
					break;
				case 7:
					ischar = false;
					whichobj = GlobalObjs.Shovel;
				following = false;
					break;
				case 8:
					ischar = false;
					whichobj = GlobalObjs.Center;
				following = false;
					break;
				case 9:
					ischar = false;
					whichobj = GlobalObjs.CenterRight;
				following = false;
					break;
				case 10:
					ischar = false;
					whichobj= GlobalObjs.CenterBackStage;
				following = false;
					break;
				case 11:
					ischar = false;
					whichobj = GlobalObjs.DownStage;
				following = false;
					break;
				case 12:
					ischar = false;
					whichobj = GlobalObjs.Grave;
				following = false;
					break;
				
				case 13:
					ischar = false;
					whichobj = GlobalObjs.StageLeft;
				following = false;
					break;
				case 14:
					ischar = false;
					whichobj = GlobalObjs.StageRight;
				following = false;
					break;
				case 15:
					ischar = false;
					whichobj = GlobalObjs.Steps;
				following = false;
					break;
				case 16:
					ischar = false;
					whichobj = GlobalObjs.Stool;
				following = false;
					break;
				case 17:
					ischar = false;
					whichobj = GlobalObjs.UpStage;
				following = false;
					break;
				default:
					ischar = false;
					whichobj = null;
				following = false;
					break;				
			}
		}
		Debug.Log ("Whichobj="+((whichobj == null)?("NULL"):(whichobj.name)));
				
		switch (whichfunction) { // check which function to call
			case 0:
				// walk
				if (isobject) {
					who.doWalk(whichobj.transform.position.x, whichobj.transform.position.z, whichobj, following);
				} else {
					who.doWalk(targetx, targety, null, following);
				}
				break;
			case 1:
						// rotate
				if (isobject) {
					who.doRotate(whichobj.transform.position.x, whichobj.transform.position.z, whichobj);
				} else {
					who.doRotate(targetx, targety, null);
				}		
				break;
			case 2:
				who.doPickup(whichobj);
						// pickup
				break;
			case 3:
						// putdown
				who.doPutDown(whichobj);
				break;
			case 4:
					// point
				who.doPoint(whichobj);
				break;
		}
	}
	
	static void parseMovement(string name, string xmltxt) {
		CharFuncs who = GlobalObjs.getCharFunc(name);
		Debug.Log (who.thisChar.name);
		//string action;
		float targetx = -1;
		float targety = -1;
		float targetx2 = -1;
		float targety2 = -1;
		GameObject target = null;
		bool following = false;
		
		string myText = null;
		int startPos = 0;
		int endPos = 0;
		string targetstr;
		
		if (xmltxt.Contains ("follow=")) {
			startPos = xmltxt.IndexOf ("follow="+quote);
			following = true;
		} else {
			startPos = xmltxt.IndexOf ("target="+quote);
		}
		//Debug.Log ("start="+startPos);
		//Debug.Log (xmltxt.Substring (startPos));
		endPos = xmltxt.Substring (startPos+8).IndexOf (quote);
		//Debug.Log (xmltxt.Substring(startPos)[7]);
		//Debug.Log (xmltxt.Substring(startPos)[7] == quote);
		//Debug.Log ("end="+endPos);
		targetstr = xmltxt.Substring(startPos+8, endPos);
		
		
		Debug.Log("Parsed target="+targetstr);
		if (targetstr.IndexOf(" ") > 0) {
			// this is a vector position, not an object
			string[] position = targetstr.Split (' ');
			bool success = float.TryParse(position[0], out targetx);
			success = float.TryParse (position[1], out targety);
			if (position.Length > 2) {
				success = float.TryParse(position[2], out targetx2);
				success = float.TryParse(position[3], out targety2);
			}
			
		} else {
			// this is an object
			target = GlobalObjs.getObject(targetstr);
			
		}
		
		// find out what action to take
		if (xmltxt.Contains ("pick-up") || xmltxt.Contains ("PICK-UP")) {
			Debug.Log ("Action=pickup");
			who.doPickup(target);	
		} else if (xmltxt.Contains("put-down") || xmltxt.Contains ("PUT-DOWN")) {
			Debug.Log ("Action=putdown");
			who.doPutDown(target);
		} else if (xmltxt.Contains ("locomotion") || xmltxt.Contains ("LOCOMOTION")) {
			Debug.Log ("Action=move");
			if (target != null) {
				who.doWalk (target.transform.position.x, target.transform.position.z, target, following);
			} else {
				who.doWalk (targetx, targety, null, following);
				if (targetx2 != -1) {
					who.doWalk (targetx2, targety2, null, following); // this one should get queued
				}
			}
		} else if (xmltxt.Contains ("gaze") || xmltxt.Contains ("GAZE")) {
			Debug.Log ("Action=turn");
			if (target != null) {
				who.doRotate(target.transform.position.x, target.transform.position.z, target);
			} else {
				who.doRotate(targetx, targety, null);
			}
		} else if (xmltxt.Contains ("POINT") || xmltxt.Contains ("point")) {
			Debug.Log ("Action=point");
			if (target != null) {
				who.doPoint(target);
			} else {
				Debug.Log ("Error no target");
			}
		} else {
			Debug.Log ("Error - unknown command");
		}
		
		
	}
}
