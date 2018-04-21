using System;

namespace Medusa.Models
{
    public class Gpu
    {
        public enum GpuType { NVidia, ATI };

        public class GpuTickData
        {
            #region private members
            DateTime tickTime;
            double temperature;
            bool tickTemperature;
            double memoryOffset;
            bool tickMemoryOffset;
            uint fanSpeed;
            bool tickFanSpeed;
            #endregion

            #region properties
            public double Temperature
            {
                get
                {
                    return temperature;
                }

                set
                {
                    temperature = value;
                }
            }

            public double MemoryOffset
            {
                get
                {
                    return memoryOffset;
                }

                set
                {
                    memoryOffset = value;
                }
            }

            public uint FanSpeed
            {
                get
                {
                    return fanSpeed;
                }

                set
                {
                    fanSpeed = value;
                }
            }
            #endregion
        }

        GpuTickData[] gpuTickData;
        uint maxFanSpeed;
        string name;
        GpuType gpuType;
    }
}
