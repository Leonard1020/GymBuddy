using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GymBuddy.Adapters;
using GymBuddy.Entities;
using Newtonsoft.Json;

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

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            _currentWorkout = JsonConvert.DeserializeObject<Workout>(Intent.GetStringExtra("selectedWorkout"), settings);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.exercise_toolbar);
            toolbar.Title = _currentWorkout.Name;
            SetSupportActionBar(toolbar);

            var exerciseListView = FindViewById<ListView>(Resource.Id.exercise_list);
            _adapter = new ExerciseListAdapter(this, _currentWorkout.Exercises);

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
                CreateExerciseDialog exerciseDialog = new CreateExerciseDialog();
                FragmentTransaction transaction = FragmentManager.BeginTransaction();

                exerciseDialog.OnCreateExersice += (sender, args) =>
                                                   {
                                                       _currentWorkout.Exercises.Add(new BodyWeightExercise(args.Name, 0, 0));
                                                       _adapter.NotifyDataSetChanged();
                                                   };
                    exerciseDialog.Show(transaction, "Create Exercise");
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}