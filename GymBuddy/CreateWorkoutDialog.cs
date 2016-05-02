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

namespace GymBuddy
{
    public class CreateWorkoutEventArgs : EventArgs
    {
        public string Name { get; set; }

        public CreateWorkoutEventArgs(string name)
        {
            Name = name;
        }
    }

    class CreateWorkoutDialog : DialogFragment
    {
        public event EventHandler<CreateWorkoutEventArgs> OnCreateWorkout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.CreateWorkoutDialog, container, false);
            var createWorkoutButton = view.FindViewById<Button>(Resource.Id.create_workout);
            var workoutName = view.FindViewById<EditText>(Resource.Id.workout_name);

            createWorkoutButton.Click += (sender, args) =>
            {
                if (OnCreateWorkout != null)
                {
                    //TODO
                    OnCreateWorkout(this, new CreateWorkoutEventArgs(workoutName.Text));
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