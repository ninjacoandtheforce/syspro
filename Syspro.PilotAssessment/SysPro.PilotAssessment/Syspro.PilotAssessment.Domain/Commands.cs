using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syspro.PilotAssessment.Domain.Enums;
using Syspro.PilotAssessment.Domain.Models;

namespace Syspro.PilotAssessment.Domain
{
    public class Commands
    {

        public Aircraft Aircraft { get; set; }

        public Commands(Aircraft rover)
        {
            Aircraft = rover;
            Aircraft.EndLocation = Aircraft.StartLocation;
        }

        public Aircraft ExecuteCommands()
        {
            var commandArray = Aircraft.Commands.ToCharArray();
            foreach (var c in commandArray)
            {
                if (c == 'L')
                {
                    TurnLeft();
                }

                if (c == 'R')
                {
                    TurnRight();
                }

                if (c == 'M')
                {
                    if (!CanRoverMove()) return Aircraft;
                    Move();
                }
            }

            return Aircraft;
        }

        private bool CanRoverMove()
        {
            var coord = new Coordinate(Aircraft.EndLocation.Coordinate.X, Aircraft.EndLocation.Coordinate.Y);
            switch (Aircraft.EndLocation.Heading)
            {
                case DirectionEnum.N:
                    coord = new Coordinate(coord.X, coord.Y += 1);
                    break;
                case DirectionEnum.E:
                    coord = new Coordinate(coord.X += 1, coord.Y);
                    break;
                case DirectionEnum.S:
                    coord = new Coordinate(coord.X, coord.Y -= 1);
                    break;
                case DirectionEnum.W:
                    coord = new Coordinate(coord.X -= 1, coord.Y);
                    break;
            }

            if (coord.Y < 0 ||
                coord.Y > Aircraft.Plateau.Y ||
                coord.X < 0 ||
                coord.X > Aircraft.Plateau.X)
            {
                Aircraft.IsSuccess = false;
                Aircraft.Message =
                    $@"Rover {Aircraft.AircraftId} will be moving out of bounds of the Plateau and will crash and burn.";
                return false;
            }

            var collisionCoordinate = Aircraft.Hazards.FirstOrDefault(p =>
                p.X == coord.X && p.Y == coord.Y);
            if (collisionCoordinate != null)
            {
                Aircraft.IsSuccess = false;
                Aircraft.Message = $@"Rover {Aircraft.AircraftId} will be crashing into another stationary rover.";
                return false;
            }

            Aircraft.IsSuccess = true;
            return true;
        }

        private void TurnLeft()
        {
            var rotation = (int)Aircraft.EndLocation.Heading;
            rotation -= 90;
            Aircraft.EndLocation.Heading = rotation == 0 ? DirectionEnum.N : (DirectionEnum)rotation;
        }

        private void TurnRight()
        {
            var rotation = (int)Aircraft.EndLocation.Heading;
            rotation += 90;
            Aircraft.EndLocation.Heading = rotation > 360 ? (DirectionEnum)(rotation - 360) : (DirectionEnum)rotation;
        }

        private void Move()
        {
            switch (Aircraft.EndLocation.Heading)
            {
                case DirectionEnum.N:
                    Aircraft.EndLocation.Coordinate.Y += 1;
                    break;
                case DirectionEnum.E:
                    Aircraft.EndLocation.Coordinate.X += 1;
                    break;
                case DirectionEnum.S:
                    Aircraft.EndLocation.Coordinate.Y -= 1;
                    break;
                case DirectionEnum.W:
                    Aircraft.EndLocation.Coordinate.X -= 1;
                    break;
            }
        }
    }
}
