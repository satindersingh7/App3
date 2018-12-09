using Android.App;
using Android.Widget;
using Android.OS;
using Java.Util;
using Android.Views;
using Java.Interop;
using System.Collections.Generic;
using Android.Content;

namespace App3
{
    [Activity(Label = "Game", Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        public string[] subcatdata = { "0", "0", "0", "0", "1", "1", "1", "1", "2", "2", "2", "2", "3", "3", "3", "3" };
        public string[] catdata = { "BLADE", "UNDERWORLD", "GOAL", "SPIDERMAN", "CRICKET", "RUGBY", "SOCCER", "HOCKEY", "JAVA", "DOTNET", "PHP", "ANDROID", "INDIA", "NEWZEALAND", "AUSTRALIA", "USA" };
        public  List<catpojo> list;
        catpojo c;
        DataStore dataStore;

        //  private string[] words;
        private Random rand;
        private string currWord;
        private LinearLayout wordLayout;
        private TextView[] charViews;
        private GridView letters;
        private LetterAdapter ltrAdapt;
        //body part images
        private ImageView[] bodyParts;
        //number of body parts
        private int numParts = 6;
        //current part - will increment when wrong answers are chosen
        private int currPart;
        //number of characters in current word
        private int numChars;
        //number correctly guessed
        private int numCorr;
        static int points = 0;
        string uname;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            wordLayout = FindViewById<LinearLayout>(Resource.Id.word);
            letters = FindViewById<GridView>(Resource.Id.letters);

            uname = Intent.GetStringExtra("name");
            dataStore = new DataStore(this);

            dataStore.deleterecord(this);

            list = new List<catpojo>();

          //  Toast.MakeText(this, "" + Intent.GetStringExtra("sid"), ToastLength.Long).Show();


           
                for (int i = 0; i < subcatdata.Length; i++)
                {
                    c = new catpojo();
                    c.Subid = subcatdata[i];
                    c.Name = catdata[i];
                    dataStore.insercat(this, c);
                }
            
        


            list = dataStore.getcat(this,Intent.GetStringExtra("sid"));
         
            rand = new Random();
            currWord = "";



            bodyParts = new ImageView[numParts];
            bodyParts[0] = FindViewById<ImageView>(Resource.Id.head);
            bodyParts[1] = FindViewById<ImageView>(Resource.Id.body);
            bodyParts[2] = FindViewById<ImageView>(Resource.Id.arm1);
            bodyParts[3] = FindViewById<ImageView>(Resource.Id.arm2);
            bodyParts[4] = FindViewById<ImageView>(Resource.Id.leg1);
            bodyParts[5] = FindViewById<ImageView>(Resource.Id.leg2);


            playGame();
          //  Toast.MakeText(this, "" + list.Count, ToastLength.Long).Show();

        }

        private void playGame()
        {
            
            string newWord = list[rand.NextInt(list.Count)].Name;
           
            while (newWord.Equals(currWord))
            {
                newWord = list[rand.NextInt(list.Count)].Name;
            }
            currWord = newWord;

            charViews = new TextView[currWord.Length];
            // poista vanhat sanat
            wordLayout.RemoveAllViews();

            for (int c = 0; c < currWord.Length; c++)
            {
                charViews[c] = new TextView(this);
                charViews[c].Text=currWord[c]+"";

                charViews[c].LayoutParameters=new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                charViews[c].Gravity= GravityFlags.Center;
                charViews[c].SetTextColor(Resources.GetColor(Resource.Color.white));
                charViews[c].SetBackgroundResource(Resource.Drawable.letter_bg);
                //add to layout
                wordLayout.AddView(charViews[c]);

            }
            ltrAdapt = new LetterAdapter(this);
            letters.SetAdapter(ltrAdapt);

            currPart = 0;
            numChars = currWord.Length;
            numCorr = 0;

            for (int p = 0; p < numParts; p++)
            {
                bodyParts[p].Visibility = ViewStates.Invisible;
            }
        }

        [Export("letterPressed")]
        public void letterPressed(View view)
        {

            string ltr = ((TextView)view).Text.ToString();
            char letterChar = ltr[0];

            view.Enabled=false;
            view.SetBackgroundResource(Resource.Drawable.letter_down);


            bool correct = false;
            for (int k = 0; k < currWord.Length; k++)
            {
                if (currWord[k] == letterChar)
                {
                    correct = true;
                    numCorr++;
                    charViews[k].SetTextColor(Resources.GetColor(Resource.Color.black));
                }
            }

            if (correct)
            {
                //correct guess
                if (numCorr == numChars)
                {
                    //user has won
                    // Disable Buttons
                    disableBtns();

                    // Display Alert Dialog
                    AlertDialog.Builder winBuild = new AlertDialog.Builder(this);
                    winBuild.SetTitle("Yay, well done!");
                    winBuild.SetMessage("You won!\n\nThe answer was:\n\n" + currWord);
                    points = points + 10;
                    winBuild.SetPositiveButton("Play Again", (c, ev) =>
                    {
                        // Ok button click task  
                        this.playGame();
                       
                    });

                    winBuild.SetNegativeButton("Exit", (c, ev) =>
                    {
                       
                        dataStore.inserscore(this, uname, points + "");
                    //    Toast.MakeText(this, "" + uname + " " + points, ToastLength.Long).Show();
                        StartActivity(new Intent(this, typeof(Exit)).PutExtra("name", uname).PutExtra("score", points + ""));
                        this.Finish();
                    });



                    winBuild.Show();
                }
            }
            else if (currPart < numParts)
            {
                //some guesses left
                bodyParts[currPart].Visibility = ViewStates.Visible;
                currPart++;
            }
            else
            {
                //user has lost
                disableBtns();

                // Display Alert Dialog
                AlertDialog.Builder loseBuild = new AlertDialog.Builder(this);
                loseBuild.SetTitle("Oopsie");
                loseBuild.SetMessage("You lose!\n\nThe answer was:\n\n" + currWord);

                loseBuild.SetPositiveButton("Play Again", (c, ev) =>
            {
                // Ok button click task  
                this.playGame();
            });

                loseBuild.SetNegativeButton("Exit", (c, ev) =>
            {
                    
                dataStore.inserscore(this, uname, points+"");
           //     Toast.MakeText(this, "" + uname+" "+points, ToastLength.Long).Show();
                StartActivity(new Intent(this, typeof(Exit)).PutExtra("name",uname).PutExtra("score", points+""));
                this.Finish();
            });


                loseBuild.Show();

            }
        }
		            



        public void disableBtns()
        {
            int numLetters = letters.Count;
            for (int l = 0; l < numLetters; l++)
            {
                letters.GetChildAt(l).Enabled=false;
            }
        }


    }

}

