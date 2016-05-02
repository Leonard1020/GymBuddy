using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GymBuddy.Adapters;
using GymBuddy.Entities;

namespace GymBuddy
{
    [Activity(Label = "ExercisesActivity", Theme = "@style/GymBuddyTheme")]
    public class ExercisesActivity : ActionBarActivity
    {
        private Workout _currentWorkout;
        private ExerciseListAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WorkoutView);

            _currentWorkout = FileHelper.GetWorkout(Intent.GetStringExtra("selectedWorkoutName"));

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.exercise_toolbar);
            toolbar.Title = _currentWorkout.Name;
            SetSupportActionBar(toolbar);

            var exerciseListView = FindViewById<ListView>(Resource.Id.exercise_list);
            _adapter = new ExerciseListAdapter(this, _currentWorkout.Exercises, _currentWorkout);

            exerciseListView.Adapter = _adapter;

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            if (item.ItemId == Resource.Id.dumbbell)
            {
                var exerciseDialog = new CreateExerciseDialog();
                var transaction = FragmentManager.BeginTransaction();

                exerciseDialog.OnCreateExersice += (sender, args) =>
                {
                    _currentWorkout.Exercises.Add(args.Exercise);
                    FileHelper.UpdateWorkout(_currentWorkout);
                    _adapter.NotifyDataSetChanged();
                };
                exerciseDialog.Show(transaction, "Create Exercise");
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}