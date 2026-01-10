using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.OpenRoute
{

    public sealed record GeocodeResponseDto(
           List<ResultDto> Results
       );

    public sealed record ResultDto(
        DataSourceDto Datasource,
        double Lon,
        double Lat,
        string ResultType,
        string Formatted,
        string AddressLine1,
        string AddressLine2,
        string Category,
        TimezoneDto Timezone,
        string PlusCode,
        string PlusCodeShort,
        RankDto Rank,
        double Confidence,
        double ConfidenceCityLevel,
        double ConfidenceStreetLevel,
        double ConfidenceBuildingLevel,
        string MatchType,
        string PlaceId,
        BboxDto Bbox,
        QueryDto Query
    );

    public sealed record DataSourceDto(
        string SourceName,
        string Attribution,
        string License,
        string Url,
        string Country,
        string CountryCode,
        string State,
        string County,
        string City,
        string Postcode,
        string Suburb,
        string Street,
        string HouseNumber,
        string Iso3166_2,
        string StateCode
    );

    public sealed record TimezoneDto(
        string Name,
        string OffsetSTD,
        int OffsetSTDSeconds,
        string OffsetDST,
        int OffsetDSTSeconds,
        string AbbreviationSTD,
        string AbbreviationDST
    );

    public sealed record RankDto(
        double Importance,
        double Popularity
    );

    public sealed record BboxDto(
        double Lon1,
        double Lat1,
        double Lon2,
        double Lat2
    );

    public sealed record QueryDto(
        string Text,
        ParsedDto Parsed
    );

    public sealed record ParsedDto(
        string HouseNumber,
        string Street,
        string Postcode,
        string City,
        string Country,
        string ExpectedType
    );
}
