using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGisOne
{
    public partial class Form1 : Form
    {
        public Form1() 
        {
            InitializeComponent();
        }

        private void axMap1_MouseDownEvent(object sender, AxMapWinGIS._DMapEvents_MouseDownEvent e)
        {

        }

        // открытие карты
        private void button1_Click(object sender, EventArgs e)
        {
            axMap1.TileProvider = MapWinGIS.tkTileProvider.GoogleMaps;
        }

        //перемещение
        private void button7_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = MapWinGIS.tkCursorMode.cmPan;
        }

        //масштабирование
        private void button8_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut;
        }

        //расстояние
        private void button9_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = MapWinGIS.tkCursorMode.cmMeasure;
        }

        //площадь
        private void button2_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = MapWinGIS.tkCursorMode.cmPan;
            axMap1.Measuring.MeasuringType = MapWinGIS.tkMeasuringType.MeasureArea;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Вы действительно хотите выйти из программы?", "Завершение программы",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
               if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // свернуть
        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // развернуть
        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
