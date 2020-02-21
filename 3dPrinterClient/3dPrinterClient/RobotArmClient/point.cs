﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmClient
{
    /// <summary>
    /// point class. methods of adding, subtracting, switching units, and switching 
    /// between absolute and incremental on points are kept here.
    /// </summary>
    public class point
    {

        /// <summary>
        /// x - x position
        /// y - y position
        /// z - z position
        /// units - current units of the point
        /// reference - current reference of the point
        /// </summary>
        public double x;
        public double y;
        public double z;
        public Unit units;
        public Reference Ref;

        /// <summary>
        /// empty constructor. deprecated
        /// </summary>
        public point()
        {

        }
        /// <summary>
        /// constructs a point.
        /// </summary>
        /// <param name="x">value of the X coordinate</param>
        /// <param name="y">value of the Y coordinate</param>
        /// <param name="z">value of the Z coordinate</param>
        /// <param name="units">units to be used for measurement</param>
        /// <param name="Ref">frame of reference of the point.</param>
        public point(double x, double y, double z, Unit units, Reference Ref)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.units = units;
            this.Ref = Ref;
        }
        /// <summary>
        /// x1+x2=x3
        /// y1+y2=y3
        /// z1+z2=z3
        /// returned in 1's units and reference
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns>the new point in millimeters</returns>
        public static point operator +(point A, point B)
        {
            A.toMillimeters();
            B.toMillimeters();
            return new point(A.x + B.x, A.y + B.y, A.z + B.z, A.units, A.Ref);
        }
        /// <summary>
        /// x1-x2=x3
        /// y1-y2=y3
        /// z1-z2=z3
        /// returned in 1's units and reference
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns>the new point in millimeters</returns>
        public static point operator -(point A, point B)
        {
            A.toMillimeters();
            B.toMillimeters();
            return new point(A.x - B.x, A.y - B.y, A.z - B.z, A.units, A.Ref);
        }
        /// <summary>
        /// returns a string expressing the point
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return "(" + x + "," + y + "," + z + "," + units.ToString() + ")";
        }
        /// <summary>
        /// sets the point to inches mode if it isnt already
        /// conversion factor: 1in = 25.4mm
        /// </summary>
        public void toInches()
        {
            if (units == Unit.millimeters)
            {
                x = x / 25.4;
                y = y / 25.4;
                z = z / 25.4;
                units = Unit.inches;
            }
        }

        /// <summary>
        /// sets the point to millimeters mode if it isnt already.
        /// conversion factor 1in = 25.4mm
        /// </summary>
        public void toMillimeters()
        {
            if (units == Unit.inches)
            {
                x = x * 25.4;
                y = y * 25.4;
                z = z * 25.4;
                units = Unit.millimeters;
            }
        }

        /// <summary>
        /// sets the point to incremental mode if it isnt already.
        /// </summary>
        /// <param name="currentPosition"></param>
        public void toIncremental(point currentPosition)
        {
            if (Ref != Reference.incremental)
            {
                var newPoint = this - currentPosition;
                x = newPoint.x;
                y = newPoint.y;
                z = newPoint.z;
                this.Ref = Reference.incremental;
            }
        }
        /// <summary>
        /// sets the point to absolute mode if it isnt already.
        /// </summary>
        /// <param name="currentPosition"></param>
        public void toAbsolute(point currentPosition)
        {
            if (Ref != Reference.absolute)
            {
                var newPoint = this + currentPosition;
                x = newPoint.x;
                y = newPoint.y;
                z = newPoint.z;
                this.Ref = Reference.absolute;
            }
        }
    }
}
