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
using Android.Views.Animations;

namespace App3
{
    [Activity(Label = "HangMan" , MainLauncher = true)]
    public class Splash : Activity
    {

         ImageView img;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.splash);


            img = FindViewById<ImageView>(Resource.Id.img);

            Animation myAnimation = AnimationUtils.LoadAnimation(this,Resource.Animation.fade);


            img.StartAnimation(myAnimation);


            myAnimation.SetAnimationListener(new MyAnimationListener(this));



        }
    }

    class MyAnimationListener : Java.Lang.Object,
        Android.Views.Animations.Animation.IAnimationListener
    {
        Activity self;

        public MyAnimationListener(Activity self)
        {
            this.self = self;
        }

        public void OnAnimationEnd(Animation animation)
        {
            self.StartActivity(new Intent(self, typeof(Category)));
            self.Finish();
        }

        public void OnAnimationRepeat(Animation animation)
        {
        }

        public void OnAnimationStart(Animation animation)
        {
        }
    }
}