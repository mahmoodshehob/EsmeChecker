using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.Models.StaticData
{
    public static class SD
    {
        public static class Schema 
        {
			public const string EsmeCheckers = "esmecheckerschema";
		}


		public static class Kannel_SD
		{
			public const string ConnectionType_Transmitter = "Transmitter";
			public const string ConnectionType_Receiver = "Receiver";
			public const string ConnectionType_Transceiver = "Transceiver";


			public const string ConnectionStatus_Online = "online";
			public const string ConnectionStatus_Re_Connecting = "re-connecting";
		}
	}
}
