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
    [Activity(Label = "Exit")]
    public class Exit : Activity
    {

        Button btnplay;
        TextView edt;
        string uname,points;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.exit);
            // Create your application here
            edt = FindViewById<TextView>(Resource.Id.txt);
            btnplay = FindViewById<Button>(Resource.Id.btnplay);

            edt.Text= "Hello "+Intent.GetStringExtra("name")+"\n"+"You have score "+ Intent.GetStringExtra("score");


            btnplay.Click += (s, e) => {

                StartActivity(new Intent(this, typeof(Category)));
            Finish();

            };

        }
    }
}