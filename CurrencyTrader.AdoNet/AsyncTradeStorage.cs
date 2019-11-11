using CurrencyTrader.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// @author Derek Shaheen
// @date 11/10/19
// @description Purpose is to create an asyhronous storage method.

namespace CurrencyTrader.AdoNet
{
    public class AsyncTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;
        private ITradeStorage tradeStorage;

        public AsyncTradeStorage(ILogger logger)
        {
            this.logger = logger;
            tradeStorage = new AdoNetTradeStorage(logger);
        }

        // Method is designed to take in the trades and parse them asynchronously.
        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Starting sync trade storage.");
            Task.Run(() => tradeStorage.Persist(trades)); // Call the concrete object method asynchronously
        }
    }
}
