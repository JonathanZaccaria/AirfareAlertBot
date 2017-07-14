using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using AirfareAlertBot.Controllers;

namespace AirfareAlertBot
{
    public class FlightFlowData
    {
    }

    public class Data
    {
        public static ProcessFlight fd = null;
        public static StateClient stateClient = null;
        public static string channelId = string.Empty;
        public static string userId = string.Empty;
        public static Activity initialActivity = null;
        public static ConnectorClient initialConnector = null;
        public static string currentText = string.Empty;
    }

    public partial class FlightFlow
    {
        private static string[] InternalGetIataCodes(object value)
        {
            string find = value.ToString().Trim(); string[] codes = null;

            codes = Data.fd.GetIataCodes(string.Empty, find, string.Empty);

            if (codes.Length == 0) codes = Data.fd.GetIataCodes(find, string.Empty, string.Empty);

            return codes;
        }
        public static void AssignStateToFlightData(ValidateResult result, TravelDetails state)
        {
            if (result.IsValid)
            {
                string userId = Data.fd.FlightDetails.UserId;

                Data.fd.FlightDetails = new Controllers.FlightDetails()
                {
                    OriginIata = state.OriginIata,
                    DestinationIata = state.DestinationIata,
                    OutboundDate = state.OutboundDate,
                    InboundDate = state.InboundDate,
                    NumPassengers = state.NumPassengers,
                    NumResults = "1", Direct = state.Direct,
                    UserId = userId, Follow = result.Value.ToString()
                };
            }
        }

        internal static bool ProcessUnfollow(string text, ref ValidateResult r)
        {
            throw new NotImplementedException();
        }
    }
}