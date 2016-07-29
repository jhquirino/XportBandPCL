//-----------------------------------------------------------------------
// <copyright file="MSHealthActivity.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents details about an activity.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthActivity
    {

        #region Constants

        /// <summary>
        /// Split Distance value for Kilometers.
        /// </summary>
        public const double SPLIT_DISTANCE_KILOMETER = 100000;

        /// <summary>
        /// Split Distance value for Miles.
        /// </summary>
        public const double SPLIT_DISTANCE_MILE = 160934;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier of the activity (unique by user).
        /// </summary>
        [JsonProperty(PropertyName = "id",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who completed the activity.
        /// </summary>
        [JsonProperty(PropertyName = "userId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the device which generated the activity.
        /// </summary>
        [JsonProperty(PropertyName = "deviceId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Gets or sets the start time of the activity.
        /// </summary>
        [JsonProperty(PropertyName = "startTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the activity.
        /// </summary>
        [JsonProperty(PropertyName = "endTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the mapping of an event to a logical date.
        /// </summary>
        /// <remarks>
        /// For most events, other than sleep, the default assignment is based on the event's start time. 
        /// This is subject to change in the future. For example, if a sleep activity starts before 5 AM, 
        /// the DayId is the previous day.
        /// </remarks>
        [JsonProperty(PropertyName = "dayId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? DayId { get; set; }

        /// <summary>
        /// Gets or sets the time the activity was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the app that created the activity.
        /// </summary>
        [JsonProperty(PropertyName = "createdBy",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the type of the activity.
        /// </summary>
        [JsonProperty(PropertyName = "activityType",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthActivityType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity.
        /// </summary>
        [JsonProperty(PropertyName = "name",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the duration of the activity.
        /// </summary>
        [JsonProperty(PropertyName = "duration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the UV exposure as time in the sun.
        /// </summary>
        [JsonProperty(PropertyName = "uvExposure",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string UVExposure { get; set; }

        /// <summary>
        /// Gets or sets the summaries associated with this activity.
        /// </summary>
        [JsonProperty(PropertyName = "minuteSummaries",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthSummary> MinuteSummaries { get; set; }

        /// <summary>
        /// Gets or sets the summary of calories burned during the activity.
        /// </summary>
        [JsonProperty(PropertyName = "caloriesBurnedSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthCaloriesBurnedSummary CaloriesBurnedSummary { get; set; }

        /// <summary>
        /// Gets or sets the heart rate summary for the activity.
        /// </summary>
        [JsonProperty(PropertyName = "heartRateSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthHeartRateSummary HeartRateSummary { get; set; }

        /// <summary>
        /// Gets or sets the performance summary for the activity.
        /// </summary>
        [JsonProperty(PropertyName = "performanceSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthPerformanceSummary PerformanceSummary { get; set; }

        /// <summary>
        /// Gets or sets the summary of distance data during the activity.
        /// </summary>
        [JsonProperty(PropertyName = "distanceSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthDistanceSummary DistanceSummary { get; set; }

        /// <summary>
        /// Gets or sets the  length of time the user was paused during the activity.
        /// </summary>
        [JsonProperty(PropertyName = "pausedDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? PausedDuration { get; set; }

        /// <summary>
        /// Gets or sets the split distance used for the activity.
        /// </summary>
        [JsonProperty(PropertyName = "splitDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? SplitDistance { get; set; }

        /// <summary>
        /// Gets or sets the map points for the activity.
        /// </summary>
        [JsonProperty(PropertyName = "mapPoints",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthMapPoint> MapPoints { get; set; }

        /// <summary>
        /// Gets or sets the segments associated with this activity.
        /// </summary>
        [JsonProperty(PropertyName = "activitySegments",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivitySegment> ActivitySegments { get; set; }

        /// <summary>
        /// Gets or sets the localized name of the exercise being completed.
        /// </summary>
        [JsonProperty(PropertyName = "exerciseTypeName",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string ExerciseTypeName { get; set; }

        /// <summary>
        /// Gets or sets the number of complete circuit rounds actually performed.
        /// </summary>
        /// <remarks>
        /// A round is the repetition of a circuit of exercises. 
        /// Partial circuits are not counted.
        /// Only available on <see cref="MSHealthActivityType.GuidedWorkout"/>
        /// </remarks>
        [JsonProperty(PropertyName = "roundsPerformed",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? RoundsPerformed { get; set; }

        /// <summary>
        /// Gets or sets the number of exercise repetitions actually performed.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.GuidedWorkout"/>
        /// </remarks>
        [JsonProperty(PropertyName = "repetitionsPerformed",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? RepetitionsPerformed { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the workout plan.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.GuidedWorkout"/>
        /// </remarks>
        [JsonProperty(PropertyName = "workoutPlanId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string WorkoutPlanId { get; set; }

        /// <summary>
        /// Gets or sets the length of time the user was awake during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "awakeDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? AwakeDuration { get; set; }

        /// <summary>
        /// Gets or sets the total length of time the user was asleep during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "sleepDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? SleepDuration { get; set; }

        /// <summary>
        /// Gets or sets the number of times the user woke up during the activity,
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "numberOfWakeups",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? NumberOfWakeups { get; set; }

        /// <summary>
        /// Gets or sets the length of time it took the user to fall asleep.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "fallAsleepDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? FallAsleepDuration { get; set; }

        /// <summary>
        /// Gets or sets the ratio of sleep duration to total duration of the activity,
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "sleepEfficiencyPercentage",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? SleepEfficiencyPercentage { get; set; }

        /// <summary>
        /// Gets or sets the length of time the user was in a restless sleep state.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "totalRestlessSleepDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? TotalRestlessSleepDuration { get; set; }

        /// <summary>
        /// Gets or sets the length of time the user was in a restful sleep state.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "totalRestfulSleepDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? TotalRestfulSleepDuration { get; set; }

        /// <summary>
        /// Gets or sets the resting heart rate during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "restingHeartRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? RestingHeartRate { get; set; }

        /// <summary>
        /// Gets or sets the time the user fell asleep.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "fallAsleepTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? FallAsleepTime { get; set; }

        /// <summary>
        /// Gets or sets the time the user woke up,.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Sleep"/>
        /// </remarks>
        [JsonProperty(PropertyName = "wakeupTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? WakeupTime { get; set; }

        /// <summary>
        /// Gets or sets the total number of steps a user took during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Golf"/>
        /// </remarks>
        [JsonProperty(PropertyName = "totalStepCount",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? TotalStepCount { get; set; }

        /// <summary>
        /// Gets or sets the total distance a user walked during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Golf"/>
        /// </remarks>
        [JsonProperty(PropertyName = "totalDistanceWalked",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? TotalDistanceWalked { get; set; }

        /// <summary>
        /// Gets or sets the number of holes played where the user scored par or better during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Golf"/>
        /// </remarks>
        [JsonProperty(PropertyName = "parOrBetterCount",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? ParOrBetterCount { get; set; }

        /// <summary>
        /// Gets or sets the distance of the longest drive hit by the user during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Golf"/>
        /// </remarks>
        [JsonProperty(PropertyName = "longestDriveDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? LongestDriveDistance { get; set; }

        /// <summary>
        /// Gets or sets the distance of the longest stroke hit by the user during the activity.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Golf"/>
        /// </remarks>
        [JsonProperty(PropertyName = "longestStrokeDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? LongestStrokeDistance { get; set; }

        /// <summary>
        /// Gets or sets the list of child activities.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Golf"/>
        /// </remarks>
        [JsonProperty(PropertyName = "childActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> ChildActivities { get; set; }

        /// <summary>
        /// Gets or sets the elevation gained.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Hike"/>
        /// </remarks>
        [JsonProperty(PropertyName = "ascentRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public decimal? AscentRate { get; set; }

        /// <summary>
        /// Gets or sets the maximum elevation gain.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthActivityType.Hike"/>
        /// </remarks>
        [JsonProperty(PropertyName = "maxAscentRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public decimal? MaxAscentRate { get; set; }

        #endregion

    }

}
