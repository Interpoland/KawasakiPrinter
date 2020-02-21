using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmClient
{
    /// <summary>
    /// Status class
    /// The Status class is used to provide updates 
    /// for the position and feedrate of the robot and extruder
    /// </summary>
    public class Status
    {

        public point m_setPosition;
        public double m_feedRate;

        /// <summary>
        /// constructor for status objects
        /// takes a position point and feedrate as parameters
        /// </summary>
        /// <param name="setPosition">a point containing an (x,y,z) coordinate</param>
        /// <param name="feedRate"the feedrate of the extruder (default units mm/min)></param>
        public Status(point setPosition, double feedRate)
        {
            m_setPosition = setPosition;
            m_feedRate = feedRate; 
        }

        /// <summary>
        /// returns a point with the current position that can be read or modified
        /// use getSetPosition().X to get the x coordinate, etc. for Y & Z
        /// </summary>
        /// <returns></returns>
        public point getSetPosition()
        {
            return m_setPosition; 
        }

        /// <summary>
        /// returns the current feedrate
        /// </summary>
        /// <returns></returns>
        public double getFeedRate()
        {
            return m_feedRate;
        }
            
    }
}
