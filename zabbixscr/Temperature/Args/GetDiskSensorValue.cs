using OpenHardwareMonitor.Hardware;

namespace zabbixscr.Temperature.Args
{
    class GetDiskSensorValue
    {
        internal static string Value
        {
            get 
            {
                return _value();
            }
            private set
            {
                _value();
            }
        }
        private static string _value()
        {
            string t = string.Empty;
            try
            {
                UpdateVisitor updateVisitor = new UpdateVisitor();
                Computer computer = new Computer();
                computer.Open();
                computer.HDDEnabled = true;
                computer.Accept(updateVisitor);
                for (int i = 0; i < computer.Hardware.Length; i++)
                {
                    if (computer.Hardware[i].HardwareType == HardwareType.HDD)
                    {
                        for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                        {
                            if (computer.Hardware[i].Name == Data.argT && computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            {
                                t = computer.Hardware[i].Sensors[j].Value.ToString();
                            }
                        }
                    }
                }
                computer.Close();
            }
            catch
            {
                t = "101";
            }
            return t;
        }
    }
}
