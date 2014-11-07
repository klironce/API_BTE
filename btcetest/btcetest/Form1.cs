using System;
using BtcE;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btcetest
{
    public partial class Form1 : Form
    {

        Parse.DataStek DS;
        Timer timer;
        public Form1()
        {
            InitializeComponent();

            /*var depth3 = BtceApiV3.GetDepth(new BtcePair[] { BtcePair.btc_usd });
            var ticker3 = BtceApiV3.GetTicker(new BtcePair[] { BtcePair.btc_usd });
            var trades3 = BtceApiV3.GetTrades(new BtcePair[] { BtcePair.btc_usd });
            var ticker = BtceApi.GetTicker(BtcePair.btc_usd);
            var trades = BtceApi.GetTrades(BtcePair.btc_usd);
            var btcusdDepth = BtceApi.GetDepth(BtcePair.ltc_rur);
            var fee = BtceApi.GetFee(BtcePair.usd_rur);

            var btceApi = new BtceApi("NQ9J6YTZ-SM26V2WM-NZUIQEJK-2ZRAMW0U-2MP5R07I", "d852ca55d2c3e5d32a5c2cca7a10b199671dfaa76afb8166ba9afa7c83c10cb7");
            var info = btceApi.GetInfo();
            var transHistory = btceApi.GetTransHistory();
            var tradeHistory = btceApi.GetTradeHistory(count: 20);
            var orderList = btceApi.GetOrderList();
            var tradeAnswer = btceApi.Trade(BtcePair.btc_usd, TradeType.Sell, 20, 0.1m);
            var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
             */

            comboBox1.Items.Add(BtcePair.btc_usd.ToString());
            comboBox1.Items.Add(BtcePair.btc_rur.ToString());
            comboBox1.Items.Add(BtcePair.btc_eur.ToString());
            comboBox1.Items.Add(BtcePair.ltc_btc.ToString());
            comboBox1.Items.Add(BtcePair.ltc_usd.ToString());
            comboBox1.Items.Add(BtcePair.ltc_rur.ToString());
            comboBox1.Items.Add(BtcePair.nmc_btc.ToString());
            comboBox1.Items.Add(BtcePair.nvc_btc.ToString());
            comboBox1.Items.Add(BtcePair.usd_rur.ToString());
            comboBox1.Items.Add(BtcePair.eur_usd.ToString());
            comboBox1.Items.Add(BtcePair.trc_btc.ToString());
            comboBox1.Items.Add(BtcePair.ppc_btc.ToString());
            comboBox1.Items.Add(BtcePair.ftc_btc.ToString());
            comboBox1.SelectedIndex = 0;
            timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            timer.Start();

           // DS = new Parse.DataStek("a+b*((s-n)*d)-(b:a+c-b*f)");

        }

        private void tableVisual()
        {
            
            var btcusdDepth = BtceApi.GetDepth((BtcePair)comboBox1.SelectedIndex);

            dataGridView1.RowCount = btcusdDepth.Asks.Count;
            dataGridView1.ColumnCount = 3;
            dataGridView2.RowCount = btcusdDepth.Bids.Count;
            dataGridView2.ColumnCount = 3;

            dataGridView1.Columns[0].HeaderText = "prise";
            dataGridView1.Columns[1].HeaderText = Parse.getColumnName(comboBox1.Text, false);
            dataGridView1.Columns[2].HeaderText = Parse.getColumnName(comboBox1.Text, true);

            dataGridView2.Columns[0].HeaderText = "prise";
            dataGridView2.Columns[1].HeaderText = Parse.getColumnName(comboBox1.Text, false);
            dataGridView2.Columns[2].HeaderText = Parse.getColumnName(comboBox1.Text, true);

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                progressBar1.Value++;
                dataGridView1.Rows[i].Cells[0].Value = btcusdDepth.Asks[i].Price;
                dataGridView1.Rows[i].Cells[1].Value = btcusdDepth.Asks[i].Amount;
                dataGridView1.Rows[i].Cells[2].Value = btcusdDepth.Asks[i].Price * btcusdDepth.Asks[i].Amount;

                dataGridView2.Rows[i].Cells[0].Value = btcusdDepth.Bids[i].Price;
                dataGridView2.Rows[i].Cells[1].Value = btcusdDepth.Bids[i].Amount;
                dataGridView2.Rows[i].Cells[2].Value = btcusdDepth.Bids[i].Price * btcusdDepth.Asks[i].Amount;
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            tableVisual();
        }
    }
}
