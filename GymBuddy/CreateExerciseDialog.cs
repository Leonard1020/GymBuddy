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
    public class CreateExerciseEventArgs : EventArgs
    {
        public string Name { get; set; }

        public CreateExerciseEventArgs(string name)
        {
            Name = name;
        }
    }

    class CreateExerciseDialog : DialogFragment
    {
        public event EventHandler<CreateExerciseEventArgs> OnCreateExersice;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.CreateExerciseDialog, container, false);
            var createExerciseButton = view.FindViewById<Button>(Resource.Id.create_exercise);
            var exerciseName = view.FindViewById<EditText>(Resource.Id.exercise_name);

            createExerciseButton.Click += (sender, args) =>
                                          {
                                              if (OnCreateExersice != null)
                                              {
                                                  //TODO
                                                  OnCreateExersice.Invoke(this, new CreateExerciseEventArgs(exerciseName.Text));
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