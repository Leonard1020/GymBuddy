using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GymBuddy.Entities;

namespace GymBuddy
{
    public class CreateExerciseEventArgs : EventArgs
    {
        public IExercise Exercise { get; set; }

        public CreateExerciseEventArgs(IExercise exercise)
        {
            Exercise = exercise;
        }
    }

    internal class CreateExerciseDialog : DialogFragment
    {
        public event EventHandler<CreateExerciseEventArgs> OnCreateExersice;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.CreateExerciseDialog, container, false);
            var createExerciseButton = view.FindViewById<Button>(Resource.Id.create_exercise);
            var exerciseName = view.FindViewById<EditText>(Resource.Id.exercise_name);
            var setCount = view.FindViewById<EditText>(Resource.Id.body_weight_sets);
            var repCount = view.FindViewById<EditText>(Resource.Id.body_weight_reps);
            var startingWeight = view.FindViewById<EditText>(Resource.Id.starting_weight);
            var weightIncrement = view.FindViewById<EditText>(Resource.Id.weight_increase);
            var minCount = view.FindViewById<EditText>(Resource.Id.timed_minutes);
            var secCount = view.FindViewById<EditText>(Resource.Id.timed_secs);
            var spinner = view.FindViewById<Spinner>(Resource.Id.exercise_spinner);

            
            var adapter = ArrayAdapter.CreateFromResource(view.Context, Resource.Array.exercise_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            spinner.ItemSelected += (sender, args) =>
            {
                var selected = (string)spinner.GetItemAtPosition(args.Position);
                if (selected == "Body Weight")
                {
                    var bodyWeightLayout = view.FindViewById<LinearLayout>(Resource.Id.body_weight_type);
                    bodyWeightLayout.Visibility = ViewStates.Visible;

                    var weightedLayout = view.FindViewById<LinearLayout>(Resource.Id.weighted_type);
                    weightedLayout.Visibility = ViewStates.Gone;

                    var timedLayout = view.FindViewById<LinearLayout>(Resource.Id.timed_type);
                    timedLayout.Visibility = ViewStates.Gone;
                }
                if (selected == "Weighted")
                {
                    var bodyWeightLayout = view.FindViewById<LinearLayout>(Resource.Id.body_weight_type);
                    bodyWeightLayout.Visibility = ViewStates.Visible;

                    var weightedLayout = view.FindViewById<LinearLayout>(Resource.Id.weighted_type);
                    weightedLayout.Visibility = ViewStates.Visible;

                    var timedLayout = view.FindViewById<LinearLayout>(Resource.Id.timed_type);
                    timedLayout.Visibility = ViewStates.Gone;
                }
                if (selected == "Timed")
                {
                    var bodyWeightLayout = view.FindViewById<LinearLayout>(Resource.Id.body_weight_type);
                    bodyWeightLayout.Visibility = ViewStates.Gone;

                    var weightedLayout = view.FindViewById<LinearLayout>(Resource.Id.weighted_type);
                    weightedLayout.Visibility = ViewStates.Gone;

                    var timedLayout = view.FindViewById<LinearLayout>(Resource.Id.timed_type);
                    timedLayout.Visibility = ViewStates.Visible;
                }
            };
            

            createExerciseButton.Click += (sender, args) =>
            {
                var type = spinner.SelectedItem.ToString();

                var name = exerciseName.Text;
                if (string.IsNullOrWhiteSpace(name)) return;

                if (type == "Body Weight")
                {
                    if (string.IsNullOrWhiteSpace(setCount.Text)) return;
                    if (string.IsNullOrWhiteSpace(repCount.Text)) return;
                    var sets = Convert.ToInt32(setCount.Text);
                    var reps = Convert.ToInt32(repCount.Text);
                    OnCreateExersice?.Invoke(this, new CreateExerciseEventArgs(new BodyWeightExercise(name, sets, reps)));
                    Dismiss();
                }
                if (type == "Weighted")
                {
                    if (string.IsNullOrWhiteSpace(setCount.Text)) return;
                    if (string.IsNullOrWhiteSpace(repCount.Text)) return;
                    if (string.IsNullOrWhiteSpace(startingWeight.Text)) return;
                    var sets = Convert.ToInt32(setCount.Text);
                    var reps = Convert.ToInt32(repCount.Text);
                    var starting = Convert.ToInt32(startingWeight.Text);
                    var increment = string.IsNullOrWhiteSpace(weightIncrement.Text) ? Convert.ToInt32(weightIncrement.Text) : 0;
                    OnCreateExersice?.Invoke(this, new CreateExerciseEventArgs(new WeightedExercise(name, sets, reps, starting, increment)));
                }
                if (type == "Timed")
                {
                    var min = string.IsNullOrWhiteSpace(minCount.Text) ? Convert.ToInt32(minCount.Text) : 0;
                    var sec = string.IsNullOrWhiteSpace(secCount.Text) ? Convert.ToInt32(setCount.Text) : 0;
                    var time = new TimeSpan(0, min, sec);
                    var temp = new BodyWeightExercise(name, 0, 0);
                    OnCreateExersice?.Invoke(this, new CreateExerciseEventArgs(new TimedExercise(temp, time)));
                }
                Dismiss();
            };

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
    }
}