using MusicIdentificationSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateFingerprint
{
    public partial class StreamStationList : Form
    {
        public StreamStationList()
        {
            InitializeComponent();
        }

        private void StreamStationList_Load(object sender, EventArgs e)
        {
            //foreach (var station in StreamStation.GetStreamStations())
            //{
            //    ListViewItem item = new ListViewItem(station.StationName);
            //    item.SubItems.Add(station.Url);
            //    item.SubItems.Add(station.IsActive.ToString());

            //    lvStations.Items.Add(item);
            //}

            var stations = StreamStation.GetStreamStations();
            dgvStations.DataSource = stations;
        }
    }
}
