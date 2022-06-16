namespace Syspro.PilotAssessment.Domain.Models;

public class Plateau
{
    public int X { get; set; }
    public int Y { get; set; }

    public Plateau(int x, int y)
    {
        X = x;
        Y = y;
    }
}