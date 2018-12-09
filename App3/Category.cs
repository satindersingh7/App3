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
    [Activity(Label = "Category")]
    public class Category : Activity
    {

         Button btnplay,btnback;
         ListView lv1;
       
        string[] items;
        string t="";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.category);
            lv1 = FindViewById<ListView>(Resource.Id.lv);
            btnplay = FindViewById<Button>(Resource.Id.btnhigh);

            btnplay = FindViewById<Button>(Resource.Id.btnhigh);

            items = new string[] {
            "Movies",
            "Sports",
            "Computer Science",
            "Country",
          
        };

            
          
            lv1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);

            lv1.ItemClick += (s, e) => {
                 t = ""+e.Position;

                if (t.Equals(""))
                {
                    Toast.MakeText(this, "Please Select One Category", ToastLength.Long).Show();
                }

                else
                {
                    StartActivity(new Intent(this, typeof(Next)).PutExtra("sid", "" + t));
                    Finish();
                }


                Toast.MakeText(this, "You Selected "+items[e.Position]+" !", ToastLength.Long).Show();

            };

            btnplay.Click += (s, e) => {

                StartActivity(new Intent(this, typeof(Score)));
                Finish();

            };


        }
    }
}