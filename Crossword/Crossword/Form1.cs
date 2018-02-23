using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
//using DropNet;
using System.IO;
//using System.Runtime.InteropServices;
using System.Reflection;
//using System.Runtime.InteropServices.ComTypes;

namespace Crossword
{
    public partial class Form1 : Form
    {
        private object oDocument;
        private object axWebBrowser1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            button1.Text = "Browse";
            //openFileDialog1.Filter = "Office Documents(*.doc, *.xls, *.ppt)|*.doc;*.xls;*.ppt";
            //openFileDialog1.FilterIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            String filename = "C:\\Subhasree\\CODES\\CODES\\moscato\\Resources\\[MOSCATO] Crossword puzzle.xlsx";
            //Start Excel and get Application object.
            oXL = new Excel.Application();
            oXL.Visible = false;
            oXL.DisplayAlerts = false; //prevents message from popping up

            try
            {
                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(filename,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing));

                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                int nCounter = 1;
                oSheet.Copy(oSheet, Type.Missing);
                Excel._Worksheet oSheetGame = (Excel._Worksheet)oWB.Worksheets["Blank"];
                oSheetGame.Name = "MyNewWorksheetName";

                //do something with worksheet

                //((Excel._Worksheet)oWB.Sheets["MyTemplate"]).Delete(); // delete template
                ((Excel._Worksheet)oWB.Worksheets["MyNewWorksheetName"]).Activate();

            }
            catch (Exception ex)
            {
                //throw e;
                throw ex;
            }
            finally
            {
                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
            }

           // oXL.Save(Type.Missing);
        }

        public void Form1_Closed(object sender, System.EventArgs e)
        {
            oDocument = null;
        }

        public void axWebBrowser1_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        {

            //Note: You can use the reference to the document object to 
            //    automate the document server.

            Object o = e.pDisp;

            oDocument = o.GetType().InvokeMember("Document", BindingFlags.GetProperty, null, o, null);

            Object oApplication = o.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oDocument, null);

            Object oName = o.GetType().InvokeMember("Name", BindingFlags.GetProperty, null, oApplication, null);

            MessageBox.Show("File opened by: " + oName.ToString());
        }
    }
}
