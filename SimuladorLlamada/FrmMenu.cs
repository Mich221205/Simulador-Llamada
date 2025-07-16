using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorLlamadas
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void TELEFONO1_Click(object sender, EventArgs e)
        {
            FrmSimuladorLlamadas tel1 = new FrmSimuladorLlamadas();
            tel1.Show();
        }

        private void TELEFONO2_Click(object sender, EventArgs e)
        {
            FrmSimuladorLlamadas2 tel2 = new FrmSimuladorLlamadas2();
            tel2.Show();
        }

        private void TELEFONO3_Click(object sender, EventArgs e)
        {
            FrmSimuladorLlamadas3 tel3 = new FrmSimuladorLlamadas3();
            tel3.Show();
        }

        private void TELEFONO4_Click(object sender, EventArgs e)
        {
            FrmSimuladorLlamadas4 tel4 = new FrmSimuladorLlamadas4();
            tel4.Show();
        }

        private void TELEFONO5_Click(object sender, EventArgs e)
        {
            FrmSimuladorLlamadas5 tel5 = new FrmSimuladorLlamadas5();
            tel5.Show();
        }
    }
}