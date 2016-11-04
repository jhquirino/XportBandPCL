//-----------------------------------------------------------------------
// <copyright file="MSHealthActivitySegment.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents a segment associated with an activity.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthActivitySegment
    {

        #region Properties

        /// <summary>
        /// Gets or sets the segment type.
        /// </summary>
        [JsonProperty(PropertyName = "segmentType",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthSegmentType Type { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the segment.
        /// </summary>
        [JsonProperty(PropertyName = "segmentId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the start time of the segment.
        /// </summary>
        [JsonProperty(PropertyName = "startTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the segment.
        /// </summary>
        [JsonProperty(PropertyName = "endTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the duration of the segment.
        /// </summary>
        [JsonProperty(PropertyName = "duration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the summary of calories burned during the segment.
        /// </summary>
        [JsonProperty(PropertyName = "caloriesBurnedSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthCaloriesBurnedSummary CaloriesBurnedSummary { get; set; }

        /// <summary>
        /// Gets or sets the heart rate summary for the segment.
        /// </summary>
        [JsonProperty(PropertyName = "heartRateSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthHeartRateSummary HeartRateSummary { get; set; }

        /// <summary>
        /// Gets or sets the breakdown of the heart rate zones during the segment.
        /// </summary>
        [JsonProperty(PropertyName = "heartRateZones",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthHeartRateZones HeartRateZones { get; set; }

        /// <summary>
        /// Gets or sets the split distance used for the segment.
        /// </summary>
        [JsonProperty(PropertyName = "splitDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? SplitDistance { get; set; }

        /// <summary>
        /// Gets or sets the summary of distance data during the segment.
        /// </summary>
        [JsonProperty(PropertyName = "distanceSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthDistanceSummary DistanceSummary { get; set; }

        /// <summary>
        /// Gets or sets the length of time the user was paused during the segment.
        /// </summary>
        [JsonProperty(PropertyName = "pausedDuration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? PausedDuration { get; set; }

        /// <summary>
        /// Gets or sets the ordinal of the circuit within the workout.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.GuidedWorkout"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "circuitOrdinal",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? CircuitOrdinal { get; set; }

        /// <summary>
        /// Gets or sets the type of the circuit.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.GuidedWorkout"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "circuitType",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? CircuitType { get; set; }

        /// <summary>
        /// Gets or sets the length of time in minutes the user was asleep during the segment.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.Sleep"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "sleepTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? SleepTime { get; set; }

        /// <summary>
        /// Gets or sets the mapping of an event to a logical date.
        /// </summary>
        /// <remarks>
        /// This is the same as the DayId for the sleep activity.
        /// Only available on <see cref="MSHealthSegmentType.Sleep"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "dayId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? DayId { get; set; }

        /// <summary>
        /// Gets or sets the sleep state.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.Sleep"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "sleepType",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthSleepType SleepType { get; set; }

        /// <summary>
        /// Gets or sets the hole number on the golf course.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.GolfHole"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "holeNumber",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? HoleNumber { get; set; }

        /// <summary>
        /// Gets or sets the steps taken by the user for the hole.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.GolfHole"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "stepCount",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? StepCount { get; set; }

        /// <summary>
        /// Gets or sets the distance walked by the user for the hole.
        /// </summary>
        /// <remarks>
        /// Only available on <see cref="MSHealthSegmentType.GolfHole"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "distanceWalked",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? DistanceWalked { get; set; }

        #endregion

    }

}
