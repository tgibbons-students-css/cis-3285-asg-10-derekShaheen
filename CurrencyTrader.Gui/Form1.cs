
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CurrencyTrader.AdoNet;
using CurrencyTrader.Contracts;

namespace CurrencyTrader.Gui
{
    public partial class Form1 : Form
    {
        TradeProcessor tradeProcessor;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrinciple.trades.txt");
            String tradeUrl = "http://faculty.css.edu/tgibbons/trades100.txt";

            var logger = new ConsoleLogger();
            //var logger = new GuiLogger(lbStatus.Items);

            //var tradeDataProvider = new StreamTradeDataProvider(tradeStream);
            var tradeDataProvider = new UrlTradeDataProvider(tradeUrl);

            var tradeValidator = new SimpleTradeValidator(logger);
            var tradeMapper = new SimpleTradeMapper();
            var tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);

            ITradeStorage tradeStorage = new AsyncTradeStorage(logger);
            tradeProcessor = new TradeProcessor(tradeDataProvider, tradeParser, tradeStorage);
        }

        private void btnReadTrades_Click(object sender, EventArgs e)
        {
            IEnumerable<string> lines = tradeProcessor.ReadTrades();
            foreach (string line in lines)
            {
                lbTradeLines.Items.Add(line);
            }
            txtStoreStatus.Text = "Trades NOT stored in database yet.";
        }

        private void btnParseTrades_Click(object sender, EventArgs e)
        {
            IEnumerable<TradeRecord> trades = tradeProcessor.ParseTrades();
            txtNumTrades.Text = trades.Count().ToString();
        }

        private void btnStoreTrades_Click(object sender, EventArgs e)
        {
            tradeProcessor.StoreTrades();
            txtStoreStatus.Text = "Trades written to database.";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
