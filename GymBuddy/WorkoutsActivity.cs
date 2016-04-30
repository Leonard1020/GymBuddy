using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using GymBuddy.Adapters;
using GymBuddy.Entities;
using Newtonsoft.Json;

namespace GymBuddy
{
    [Activity(Label = "GymBuddy", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/GymBuddyTheme")]
    public class WorkoutsActivity : ActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.workout_toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "My Workouts";

            #region Fillers for now

            var pushups = new BodyWeightExercise("Pushups", 5, 20);
            var pullups = new BodyWeightExercise("Pullups", 3, 8);

            var exercises = new List<IExercise>
            {
                pushups,
                pullups,
                new WeightedExercise("Squats", 5, 5, 225),
                new TimedExercise(pullups, new TimeSpan(0, 5, 0)),
                new SuperSet("The Finisher", 4, pushups, pullups)
            };

            var workouts = new List<Workout>
            {
                new Workout("ArmX", "Leonard", DateTime.Now, null),
                new Workout("LegX", null, null, null),
                new Workout("BackX", "Leonard", null, exercises),
                new Workout("Agility", null, DateTime.Now, null),
                new Workout("Strength", "Leonard", DateTime.Now, exercises),
                new Workout("Body Weight", "Leonard", null, exercises)
            };

            #endregion

            var workoutsListView = FindViewById<ListView>(Resource.Id.workout_list);
            var adapter = new WorkoutListAdapter(this, workouts);

            workoutsListView.Adapter = adapter;
            workoutsListView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(ExercisesActivity));
                var selectedWorkout = adapter[args.Position];

                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                intent.PutExtra("selectedWorkout", JsonConvert.SerializeObject(selectedWorkout, settings));
                StartActivity(intent);
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            var startWorkout = menu.FindItem(Resource.Id.start_current_workout);
            startWorkout.SetVisible(false);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}

