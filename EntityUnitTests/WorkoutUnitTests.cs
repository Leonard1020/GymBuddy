using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GymBuddy.Entities;

namespace EntityUnitTests
{
    [TestClass]
    public class WorkoutUnitTests
    {
        [TestMethod]
        public void Workout_CheckDefaults_HappyPath()
        {
            var exercises = new List<IExercise>
            {
                new WeightedExercise("Squats", 5, 5, 225),
                new BodyWeightExercise("Pushups", 4, 25)
            };
            var workout = new Workout("Workout!", "Leonard", DateTime.Now, exercises);
            Assert.AreEqual("Workout!", workout.Name);
            Assert.AreEqual("Leonard", workout.Creator);
            //Assert.IsTrue(workout.LastCompleted.HasValue);
            //Assert.AreEqual(2, workout.Exercises.Count);
        }

        [TestMethod]
        public void Workout_NullParams_UseDefaults()
        {
            var workout = new Workout(null, null, null, null);
            Assert.AreEqual("Nameless Workout", workout.Name);
            Assert.IsNull(workout.Creator);
            Assert.IsNull(workout.LastCompleted);
            Assert.IsNotNull(workout.Exercises);
            //Assert.IsTrue(workout.Exercises.Count == 0);
        }
    }
}
