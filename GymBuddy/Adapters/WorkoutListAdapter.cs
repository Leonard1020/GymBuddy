using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using GymBuddy.Entities;

namespace GymBuddy.Adapters
{
    internal class WorkoutListAdapter : BaseAdapter<Workout>
    {
        private readonly List<Workout> _workouts;
        private readonly Context _context;

        public WorkoutListAdapter(Context c, List<Workout> w)
        {
            _workouts = w;
            _context = c;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.WorkoutListViewRow, parent, false);

            var name = row.FindViewById<TextView>(Resource.Id.workout_name);
            var lastCompleted = row.FindViewById<TextView>(Resource.Id.last_completed);
            var creator = row.FindViewById<TextView>(Resource.Id.workout_creator);

            var workout = _workouts[position];

            name.Text = workout.Name;
            lastCompleted.Text = workout.LastCompleted != null ? Convert.ToString(workout.LastCompleted) : "Not yet complete";
            if (workout.Creator != null)
            {
                creator.Text = $"Created By: {workout.Creator}";
            }
            else
            {
                creator.Visibility = ViewStates.Invisible;
            }

            return row;
        }

        public override int Count => _workouts.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Workout this[int position] => _workouts[position];
    }
}