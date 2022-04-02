﻿// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Stats;

public class SeasonDriverStandingsHeader
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = null!;

    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = null!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = null!;

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = null!;

    [JsonPropertyName("last_updated")]
    public DateTime LastUpdated { get; set; }
}

[JsonSerializable(typeof(SeasonDriverStandingsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonDriverStandingsHeaderContext : JsonSerializerContext
{ }
