﻿// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Cars;
using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Exceptions;
using Aydsko.iRacingData.Hosted;
using Aydsko.iRacingData.Leagues;
using Aydsko.iRacingData.Lookups;
using Aydsko.iRacingData.Member;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Searches;
using Aydsko.iRacingData.Series;
using Aydsko.iRacingData.Stats;
using Aydsko.iRacingData.Tracks;

namespace Aydsko.iRacingData;

public interface IDataClient
{
    /// <summary>Supply the username and password if they weren't supplied through the <see cref="iRacingDataClientOptions"/> object.</summary>
    /// <param name="username">iRacing user name to use for authentication.</param>
    /// <param name="password">Password associated with the iRacing user name used to authenticate.</param>
    /// <exception cref="iRacingClientOptionsValueMissingException">Either <paramref name="username"/> or <paramref name="password"/> were <see langword="null"/> or white space.</exception>
    void UseUsernameAndPassword(string username, string password);

    /// <summary>Retrieves details about the car assets, including image paths and descriptions.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing a dictionary which maps the car identifier to a <see cref="CarAssetDetail"/> object for each car.</returns>
    /// <remarks>All image paths are relative to <c>https://images-static.iracing.com</c>.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<IReadOnlyDictionary<string, CarAssetDetail>>> GetCarAssetDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieves details about the car classes.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="CarClass"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Common.CarClass[]>> GetCarClassesAsync(CancellationToken cancellationToken = default);

    /// <summary>The best laps in various cars & tracks for the given customer's career or that or the authenticated user.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="carId">Car to return statistics for. Should pass <see langword="null"/> for the first call & then should be an identifier from <see cref="MemberBests.CarsDriven"/>.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the best laps in a <see cref="MemberBests"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberBests>> GetBestLapStatisticsAsync(int? customerId = null, int? carId = null, CancellationToken cancellationToken = default);

    /// <summary>Return a summary of statistics for the given customer's career or that or the authenticated user.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberCareer"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberCareer>> GetCareerStatisticsAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieves details about the cars.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="global::Aydsko.iRacingData.Cars.CarInfo"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Cars.CarInfo[]>> GetCarsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieves league membership</summary>
    /// <param name="includeLeague">Indicates if league information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="global::Aydsko.iRacingData.Leagues.LeagueMembership"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Leagues.LeagueMembership[]>> GetLeagueMembershipAsync(bool includeLeague = false, CancellationToken cancellationToken = default);

    /// <summary>Retrieves league's seasons</summary>
    /// <param name="leagueId">League Id to return seasons for.</param>
    /// <param name="retired">If <see langword="true"/> include seasons which are no longer active.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="global::Aydsko.iRacingData.Leagues.LeagueSeasons"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<LeagueSeasons>> GetLeagueSeasonsAsync(int leagueId, bool includeRetired = false, CancellationToken cancellationToken = default);

    /// <summary>Retrieves league's season's sessions</summary>
    /// <param name="leagueId">League Id to return sessions for.</param>
    /// <param name="seasonId">Season Id to return sessions for.</param>
    /// <param name="resultsOnly">If <see langword="true"/> include only sessions which results are available.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="global::Aydsko.iRacingData.Leagues.LeagueSeasonSessions"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<LeagueSeasonSessions>> GetLeagueSeasonSessionsAsync(int leagueId, int seasonId, bool resultsOnly = false, CancellationToken cancellationToken = default);

    /// <summary>Retrieves a list of the iRacing Divisions.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Division"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Division[]>> GetDivisionsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieves a list of the iRacing Race Categories.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Category"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Category[]>> GetCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieves a list of the iRacing Event Types.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Constants.EventType"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Constants.EventType[]>> GetEventTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>Sessions that can be joined as a driver or spectator, and also includes non-league pending sessions for the user.</summary>
    /// <param name="packageId">If set, return only sessions using this car or track package id.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Constants.EventType"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<CombinedSessionsResult>> ListHostedSessionsCombinedAsync(int? packageId = null, CancellationToken cancellationToken = default);

    /// <summary>Sessions that can be joined as a driver. Without spectator and non-league pending sessions for the user.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Constants.EventType"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<HostedSessionsResult>> ListHostedSessionsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about one or more other drivers by their customer identifier.</summary>
    /// <param name="customerIds">An array of one or more customer identifiers.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="DriverInfo"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<DriverInfo[]>> GetDriverInfoAsync(int[] customerIds, bool includeLicenses, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the awards earned by the given customer or the currently authenticated user.</summary>
    /// <param name="customerId">A customer identifier to retrieve awards for or leave as <see langword="null" /> to default to the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="MemberAward"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberAward[]>> GetDriverAwardsAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Get information about a league.</summary>
    /// <param name="leagueId">The unique identifier for the league.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<League>> GetLeagueAsync(int leagueId, bool includeLicenses = false, CancellationToken cancellationToken = default);

    /// <summary>Information about license levels available in the iRacing system.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="License"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<LicenseLookup[]>> GetLicenseLookupsAsync(CancellationToken cancellationToken = default);

    /// <summary>Information about reference data defined by the system.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="LookupGroup"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<LookupGroup[]>> GetLookupsAsync(CancellationToken cancellationToken = default);

    /// <summary>Information about the Clubs configured in iRacing.</summary>
    /// <param name="seasonYear">Year to retrieve club information from.</param>
    /// <param name="seasonQuarter">Quarter to retrieve club information from.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="ClubHistoryLookup"/> objects.</returns>
    /// <remarks>Returns an earlier history if requested quarter does not have a club history.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<ClubHistoryLookup[]>> GetClubHistoryLookupsAsync(int seasonYear, int seasonQuarter, CancellationToken cancellationToken = default);

    /// <summary>Search for one or more drivers.</summary>
    /// <param name="searchTerm">A customer id or partial name to search on.</param>
    /// <param name="leagueId">Narrow the search to the roster of the given league.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="DriverSearchResult"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<DriverSearchResult[]>> SearchDriversAsync(string searchTerm, int? leagueId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the authenticated member's division.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="eventType">The type of events to return, either <see cref="Common.EventType.TimeTrial" /> or <see cref="Common.EventType.Race" />.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="MemberDivision" /> object containing the result.</returns>
    /// <remarks>Divisions are 0-based: 0 is Division 1, 10 is Rookie. See <see cref="GetDivisionsAsync(CancellationToken)"/> for more information.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberDivision>> GetMemberDivisionAsync(int seasonId, Common.EventType eventType, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the recent race participation for the currently authenticated member.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the member's recent races in a <see cref="MemberRecentRaces"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberRecentRaces>> GetMemberRecentRacesAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve overall summary figures for the <paramref name="customerId"/> given or the current authenticated user.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the member's recent races in a <see cref="MemberRecentRaces"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberSummary>> GetMemberSummaryAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the statistics for the currently authenticated member, grouped by year.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the member's statistics in a <see cref="MemberYearlyStatistics"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberYearlyStatistics>> GetMemberYearlyStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve the <see cref="MemberInfo"/> for the currently authenticated user.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberInfo"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberInfo>> GetMyInfoAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve the <see cref="MemberProfile"/> representing details about a driver.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberInfo"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberProfile>> GetMemberProfileAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the driver standings for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="clubId">Club identifier to search. Defaults to "all" (-1).</param>
    /// <param name="division">Division to search. Note that divisions are zero-based. See <see cref="GetDivisionsAsync(CancellationToken)"/>.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of standings.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Standings)>> GetSeasonDriverStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, int clubId = -1, int? division = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the qualifying results for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="clubId">Club identifier to search. Defaults to "all" (-1).</param>
    /// <param name="division">Division to search. Note that divisions are zero-based. See <see cref="GetDivisionsAsync(CancellationToken)"/>.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of qualifying results.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Results)>> GetSeasonQualifyResultsAsync(int seasonId, int carClassId, int raceWeekNumber, int clubId = -1, int? division = null, CancellationToken cancellationToken = default);

    /// <summary>World record times for a car at a particular track. Optionally by year and season.</summary>
    /// <param name="carId">Unique identifier for the car.</param>
    /// <param name="trackId">Unique identifier for the track.</param>
    /// <param name="seasonYear">Optional, if supplied limits times to the given year.</param>
    /// <param name="seasonQuarter">Optional, if supplied limits times to a given quarter. Must be used with <paramref name="seasonYear"/>.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall information and an array of world record results.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(WorldRecordsHeader Header, WorldRecordEntry[] Entries)>> GetWorldRecordsAsync(int carId, int trackId, int? seasonYear = null, int? seasonQuarter = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the time trial results for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="clubId">Club identifier to search. Defaults to "all" (-1).</param>
    /// <param name="division">Division to search. Note that divisions are zero-based. See <see cref="GetDivisionsAsync(CancellationToken)"/>.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of time trial results.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Results)>> GetSeasonTimeTrialResultsAsync(int seasonId, int carClassId, int raceWeekNumber, int clubId = -1, int? division = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the time trial standings for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="clubId">Club identifier to search. Defaults to "all" (-1).</param>
    /// <param name="division">Division to search. Note that divisions are zero-based. See <see cref="GetDivisionsAsync(CancellationToken)"/>.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of time trial standings.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SeasonTimeTrialStandingsHeader Header, SeasonTimeTrialStanding[] Standings)>> GetSeasonTimeTrialStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, int clubId = -1, int? division = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the team standings for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of team standings.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SeasonTeamStandingsHeader Header, SeasonTeamStanding[] Standings)>> GetSeasonTeamStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the races run during a week in the season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="eventType">The type of events to return.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the races in a <see cref="SeasonResults"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<SeasonResults>> GetSeasonResultsAsync(int seasonId, Common.EventType eventType, int raceWeekNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the season & series.</summary>
    /// <param name="includeSeries">Indicate if the series details should be included.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="SeasonSeries"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<SeasonSeries[]>> GetSeasonsAsync(bool includeSeries, CancellationToken cancellationToken = default);

    /// <summary>Retrieve a list of series.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="SeasonSeries"/> array.</returns>
    /// <remarks>To get series and seasons for which standings should be available, filter the list where <see cref="StatisticsSeries.Official" /> is <see langword="true" />.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<StatisticsSeries[]>> GetStatisticsSeriesAsync(CancellationToken cancellationToken = default);

    /// <summary>Get the lap details for a particular driver in the given single-driver subsession.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="customerId">A customer identifier value for the driver in the race to return laps for.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetSingleDriverSubsessionLapsAsync(int subSessionId, int simSessionNumber, int customerId, CancellationToken cancellationToken = default);

    /// <summary>Get the results of a subsession, if the authenticated user is authorized to view them.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>> GetSubSessionLapChartAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default);

    /// <summary>Get the results of a subsession, if the authenticated user is authorized to view them.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, CancellationToken cancellationToken = default);

    /// <summary>Get the lap details for a team in the given team subsession.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="teamId">The unique team identifier value for the team from the race to return laps for.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing overall session details in a <see cref="SubsessionLapsHeader"/> object along with an array of <see cref="SubsessionLap" />.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetTeamSubsessionLapsAsync(int subSessionId, int simSessionNumber, int teamId, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the track assets.</summary>
    /// <remarks>Image paths are relative to https://images-static.iracing.com/.</remarks>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<IReadOnlyDictionary<string, TrackAssets>>> GetTrackAssetsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the tracks.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Tracks.Track[]>> GetTracksAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve a list of session events.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the subsession's event log header details in a <see cref="SubsessionEventLogHeader"/> and <see cref="SubsessionEventLogItem"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] LogItems)>> GetSubsessionEventLogAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve a list of the current season's series.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the series detail in a <see cref="SeriesDetail"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<SeriesDetail[]>> GetSeriesAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve a the current season's series assets.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing a dictionary with the series identifier as the key and the assets details in a <see cref="SeriesAsset"/> item.</returns>
    /// <remarks>All image paths are relative to <c>https://images-static.iracing.com</c>.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<IReadOnlyDictionary<string, SeriesAsset>>> GetSeriesAssetsAsync(CancellationToken cancellationToken = default);

    /// <summary>Search for hosted and league sessions over a maximum period of 90 days.</summary>
    /// <param name="searchParameters">Parameters object containing the values to use in the search.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the search's header details in a <see cref="HostedResultsHeader"/> and <see cref="HostedResultItem"/> array for the results.</returns>
    /// <remarks>
    /// <para>
    /// For scraping results the most effective approach is to keep track of the maximum <see cref="HostedResultItem.EndTime"/>
    /// found during a search then make the subsequent call using that date/time as the <see cref="HostedSearchParameters.FinishRangeBegin"/>
    /// and skip any subsessions that are duplicated. Results are ordered by subsessionid which is a proxy for start time.
    /// </para>
    /// <para>
    /// Valid searches must be structured as follows:
    /// <list type="bullet">
    /// <item>requires one of: <see cref="SearchParameters.StartRangeBegin"/>, <see cref="SearchParameters.FinishRangeBegin"/></item>
    /// <item>requires one of: <see cref="SearchParameters.ParticipantCustomerId"/>, <see cref="HostedSearchParameters.HostCustomerId"/>, <see cref="HostedSearchParameters.SessionName"/></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(HostedResultsHeader Header, HostedResultItem[] Items)>> SearchHostedResultsAsync(HostedSearchParameters searchParameters, CancellationToken cancellationToken = default);

    /// <summary>Search for official sessions over a maximum period of 90 days.</summary>
    /// <param name="searchParameters">Parameters object containing the values to use in the search.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the search's header details in a <see cref="OfficialSearchResultHeader"/> and <see cref="OfficialSearchResultItem"/> array for the results.</returns>
    /// <remarks>
    /// <para>
    /// For scraping results the most effective approach is to keep track of the maximum <see cref="OfficialSearchResultItem.EndTime"/>
    /// found during a search then make the subsequent call using that date/time as the <see cref="SearchParameters.FinishRangeBegin"/>
    /// and skip any subsessions that are duplicated. Results are ordered by subsessionid which is a proxy for start time.
    /// </para>
    /// <para>
    /// Valid searches require at least one of:
    /// <list type="bullet">
    /// <item><see cref="OfficialSearchParameters.SeasonYear"/> and <see cref="OfficialSearchParameters.SeasonQuarter"/></item>
    /// <item><see cref="SearchParameters.StartRangeBegin"/></item>
    /// <item><see cref="SearchParameters.FinishRangeBegin"/></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<(OfficialSearchResultHeader Header, OfficialSearchResultItem[] Items)>> SearchOfficialResultsAsync(OfficialSearchParameters searchParameters, CancellationToken cancellationToken = default);

    /// <summary>Obtains the source data to generate charts about a customer's account.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="categoryId">The category the chart data should be for. 1 - Oval; 2 - Road; 3 - Dirt oval; 4 - Dirt road</param>
    /// <param name="chartType">Data to return in the chart. 1 - iRating; 2 - TT Rating; 3 - License/SR</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the <see cref="MemberChart"/> information.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<MemberChart>> GetMemberChartData(int? customerId, int categoryId, MemberChartType chartType, CancellationToken cancellationToken = default);

    /// <summary>Searches the league directory based on the given parameters.</summary>
    /// <param name="searchParameters">Parameters object containing the values to use in the search.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the search's results as a <see cref="LeagueDirectoryResultPage"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<LeagueDirectoryResultPage>> SearchLeagueDirectoryAsync(SearchLeagueDirectoryParameters searchParameters, CancellationToken cancellationToken = default);

    /// <summary>Get a list of the Seasons run during the given year and quarter numbers.</summary>
    /// <param name="seasonYear">The year to list seasons for.</param>
    /// <param name="seasonQuarter">The quarter to list seasons for.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the search's results as a <see cref="ListOfSeasons"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<ListOfSeasons>> ListSeasonsAsync(int seasonYear, int seasonQuarter, CancellationToken cancellationToken = default);

    /// <summary>Find sessions that begin or end after the given time.</summary>
    /// <param name="from">Optional instant to search from. Must be a time in the future. Defaults to the current instant.</param>
    /// <param name="includeEndAfterFrom">If <see langword="true"/> include sessions which begin before the <paramref name="from"/> time but end after it.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the search's results as a <see cref="RaceGuideResults"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<RaceGuideResults>> GetRaceGuideAsync(DateTimeOffset? from = null, bool? includeEndAfterFrom = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieves a list of country lookup values.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Country"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <exception cref="iRacingUnauthorizedResponseException">If the iRacing API returns a <c>401 Unauthorized</c> response.</exception>
    Task<DataResponse<Country[]>> GetCountriesAsync(CancellationToken cancellationToken = default);
}
