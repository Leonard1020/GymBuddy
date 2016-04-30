using System;
using GymBuddy.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityUnitTests
{
    [TestClass]
    public class BodyWeightUnitTests
    {
        [TestMethod]
        public void BodyWeightExercise_CheckDefaults_NoPR()
        {
            var exercise = new BodyWeightExercise("Pushups", 5, 20);
            Assert.AreEqual("Pushups", exercise.Name);
            Assert.AreEqual(5, exercise.Sets);
            Assert.AreEqual(20, exercise.Reps);
            Assert.AreEqual(0, exercise.Pr);
        }

        [TestMethod]
        public void BodyWeightExercise_CheckDefaults_WithPR()
        {
            var exercise = new BodyWeightExercise("Pushups", 5, 20, 100);
            Assert.AreEqual("Pushups", exercise.Name);
            Assert.AreEqual(5, exercise.Sets);
            Assert.AreEqual(20, exercise.Reps);
            Assert.AreEqual(100, exercise.Pr);
        }

        [TestMethod]
        public void BodyWeightExercise_NullName_UseDefaultName()
        {
            var exercise = new BodyWeightExercise(null, 5, 20);
            Assert.AreEqual("Nameless Body Weight Exercise", exercise.Name);
        }

        [TestMethod]
        public void BodyWeightExercise_NegativeSetsReps_DefaultToZero()
        {
            var exercise = new BodyWeightExercise("Name", -5, -20);
            Assert.AreEqual(0, exercise.Sets);
            Assert.AreEqual(0, exercise.Reps);
        }
    }

    [TestClass]
    public class SuperSetUnitTests
    {
        [TestMethod]
        public void SuperSet_CheckDefaults_HappyPath()
        {
            var pushups = new BodyWeightExercise("Pushups", 5, 20);
            var pullups = new BodyWeightExercise("Pullups", 5, 5);
            var sSet = new SuperSet("SuperSet", 3, pushups, pullups);
            Assert.AreEqual("SuperSet", sSet.Name);
            Assert.AreEqual(3, sSet.Cycles);
            Assert.AreEqual(pushups, sSet.First);
            Assert.AreEqual(pullups, sSet.Second);
        }

        [TestMethod]
        public void SuperSet_NegativeSets_UseDefault()
        {
            var pushups = new BodyWeightExercise("Pushups", 5, 20);
            var sSet = new SuperSet("Name", -3, pushups, pushups);
            Assert.AreEqual(0, sSet.Cycles);
        }

        [TestMethod]
        public void SuperSet_PassingNull_UseDefaults()
        {
            var sSet = new SuperSet(null, 3, null, null);
            Assert.AreEqual("Nameless Super Set", sSet.Name);
            Assert.IsNull(sSet.First);
            Assert.IsNull(sSet.Second);
        }
    }

    [TestClass]
    public class TimedExerciseUnitTests
    {
        [TestMethod]
        public void TimedExercise_CheckDefaults_HappyPath()
        {
            var pushups = new BodyWeightExercise("Pushups", 5, 20);
            var timed = new TimedExercise(pushups, new TimeSpan(0, 1, 0));
            Assert.AreEqual("Timed Pushups", timed.Name);
            Assert.AreEqual(1, timed.Time.Minutes);
            Assert.AreEqual(pushups, timed.Exercise);
        }

        [TestMethod]
        public void TimedExercise_CustomName_UsesCustomName()
        {
            var pushups = new BodyWeightExercise("Pushups", 5, 20);
            var timed = new TimedExercise(pushups, new TimeSpan(0, 1, 0), "BestEver");
            Assert.AreEqual("BestEver", timed.Name);
        }

        [TestMethod]
        public void TimedExercise_NullExercise_UseDefaults()
        {
            var timed = new TimedExercise(null, new TimeSpan(0, 1, 0));
            Assert.AreEqual("Timed Exercise", timed.Name);
        }

        [TestMethod]
        public void TimedExercise_NullExerciseWithCustomName_UseCustomName()
        {
            var timed = new TimedExercise(null, new TimeSpan(0, 1, 0), "Custom");
            Assert.AreEqual("Custom", timed.Name);
        }
    }

    [TestClass]
    public class WeightedExerciseUnitTests
    {
        [TestMethod]
        public void WeightedExercise_SmallConstructor_HappyPath()
        {
            var small = new WeightedExercise("Squats", 5, 5);
            Assert.AreEqual("Squats", small.Name);
            Assert.AreEqual(5, small.Sets);
            Assert.AreEqual(5, small.Reps);
            Assert.AreEqual(0, small.StartingWeight);
            Assert.AreEqual(0, small.WeightIncrements);
        }

        [TestMethod]
        public void WeightedExercise_MiddleConstructor_HappyPath()
        {
            var middle = new WeightedExercise("Squats", 5, 5, 225);
            Assert.AreEqual("Squats", middle.Name);
            Assert.AreEqual(5, middle.Sets);
            Assert.AreEqual(5, middle.Reps);
            Assert.AreEqual(225, middle.StartingWeight);
            Assert.AreEqual(0, middle.WeightIncrements);
        }

        [TestMethod]
        public void WeightedExercise_LargeConstructor_HappyPath()
        {
            var large = new WeightedExercise("Squats", 5, 5, 225, 5);
            Assert.AreEqual("Squats", large.Name);
            Assert.AreEqual(5, large.Sets);
            Assert.AreEqual(5, large.Reps);
            Assert.AreEqual(225, large.StartingWeight);
            Assert.AreEqual(5, large.WeightIncrements);
        }

        [TestMethod]
        public void WeightedExercise_NullName_UseDefault()
        {
            var exe = new WeightedExercise(null, 5, 5, 225);
            Assert.AreEqual("Nameless Weighted Exercise", exe.Name);
        }

        [TestMethod]
        public void WeightedExercise_NegativeInputs_UseDefault()
        {
            var exercise = new WeightedExercise("Squats", -5, -20, -225, -5);
            Assert.AreEqual(0, exercise.Sets);
            Assert.AreEqual(0, exercise.Reps);
            Assert.AreEqual(0, exercise.StartingWeight);
            Assert.AreEqual(-5, exercise.WeightIncrements);
        }
    }
}
