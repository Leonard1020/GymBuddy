using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using GymBuddy.Adapters;
using GymBuddy.Entities;
using Newtonsoft.Json;
using Environment = System.Environment;

namespace GymBuddy
{
    [Activity(Label = "GymBuddy", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/GymBuddyTheme")]
    public class WorkoutsActivity : ActionBarActivity
    {
        private List<Workout> _workouts = new List<Workout>();
        private WorkoutListAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.workout_toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "My Workouts";
            
            #region hide
            /*
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
                // Create our connection
    string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    var db = new SQLiteConnection(System.IO.Path.Combine(folder, "notes.db"));
    db.CreateTable<Note>();

// Insert note into the database
var note = new Note { Message = "Test Note" };
    db.Insert (note);

// Show the automatically set ID and message.
Console.WriteLine ("{0}: {1}", note.Id, note.Message);
*/
            #endregion

            _workouts = FileHelper.GetWorkoutList();
            
            _adapter = new WorkoutListAdapter(this, _workouts);
            var workoutsListView = FindViewById<ListView>(Resource.Id.workout_list);
            workoutsListView.Adapter = _adapter;
            workoutsListView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(ExercisesActivity));
                intent.PutExtra("selectedWorkoutName", _adapter[args.Position].Name);
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.dumbbell)
            {
                var workoutDialog = new CreateWorkoutDialog();
                var transaction = FragmentManager.BeginTransaction();

                workoutDialog.OnCreateWorkout += (sender, args) =>
                {
                    var workout = new Workout(args.Name, "me", null, new List<IExercise>());
                    _workouts.Add(workout);
                    FileHelper.AddWorkout(workout);
                    _adapter.NotifyDataSetChanged();
                };
                workoutDialog.Show(transaction, "Create Workout");
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

