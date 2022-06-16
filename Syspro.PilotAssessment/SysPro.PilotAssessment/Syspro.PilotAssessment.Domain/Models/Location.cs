using Syspro.PilotAssessment.Domain.Enums;

namespace Syspro.PilotAssessment.Domain.Models;

public class Location
{
    public Coordinate Coordinate { get; set; }
    public DirectionEnum Heading { get; set; }

    public Location(Coordinate coordinate, DirectionEnum directionEnum)
    {
        Coordinate = coordinate;
        Heading = directionEnum;
    }
}