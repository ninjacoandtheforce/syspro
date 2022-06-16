namespace Syspro.PilotAssessment.Domain.Models;

public class Aircraft
{
    public Plateau Plateau { get; set; }
    public Location StartLocation { get; set; }
    public Location EndLocation { get; set; }
    public string Commands { get; set; }
    public bool SmokeOn { get; set; }
    public string Message { get; set; }
    public int AircraftId { get; set; }

    public Aircraft(Plateau plateau, Location startLocation, string commands, int roverId, bool smokeOn = false)
    {
        Plateau = plateau;
        StartLocation = startLocation;
        Commands = commands;
        SmokeOn = smokeOn;
        AircraftId = roverId;
    }
}