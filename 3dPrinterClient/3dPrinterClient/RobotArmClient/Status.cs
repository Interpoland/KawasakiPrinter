using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmClient
{
    public class Status
    {

        public point m_setPosition;
        public double m_feedRate;
        public Status(point setPosition, double feedRate)
        {
            m_setPosition = setPosition;
            m_feedRate = feedRate; 
        }
        public point getSetPosition()
        {
            return m_setPosition; 
        }
        public double getFeedRate()
        {
            return m_feedRate;
        }
            
    }
}
