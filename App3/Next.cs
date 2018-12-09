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
    [Activity(Label = "Next")]
    public class Next : Activity
    {
        Button btnplay;
        EditText edt;
        string t;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.next);

            edt = FindViewById<EditText>(Resource.Id.edt);
            btnplay = FindViewById<Button>(Resource.Id.btnplay);

            t = Intent.GetStringExtra("sid");

            btnplay.Click += (s, e) => {


                if (edt.Text.ToString().Equals(""))
                {
                    Toast.MakeText(this, "Please Select Name", ToastLength.Long).Show();
                }

                else
                {
                    StartActivity(new Intent(this, typeof(MainActivity)).PutExtra("sid", "" + t).PutExtra("name", edt.Text.ToString()));
                    Finish();
                }

            };
        }
    }
}