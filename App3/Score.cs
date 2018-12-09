using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App3
{
    [Activity(Label = "Score")]
    public class Score : Activity
    {
        TextView txt;
        public List<catpojo> list;
        catpojo c;
        DataStore dataStore;
        Button back;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.score);
            txt = FindViewById<TextView>(Resource.Id.txt);
            back = FindViewById<Button>(Resource.Id.btnback);
            // Create your application here
            dataStore = new DataStore(this);

            list = new List<catpojo>();

            list = dataStore.getscore(this);

           // Toast.MakeText(this, "" + list.Count, ToastLength.Long).Show();

            for (int i = 0; i < list.Count; i++)
            {
                c = new catpojo();
                c.Subid = list[i].Subid;
                c.Name = list[i].Name;

                txt.Text = txt.Text + "\n" + c.Name + " " + c.Subid;
            }

            back.Click += (s, e) => {

                StartActivity(new Intent(this, typeof(Category)));
                Finish();

            };

        }
    }
}