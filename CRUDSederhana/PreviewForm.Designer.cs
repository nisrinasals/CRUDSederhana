using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDSederhana
{
    public partial class PreviewForm : Form
    {
        private DataTable dataTable;


        // Fixed method implementation for PreviewForm_Load_1
        private void PreviewForm_Load_1(object sender, EventArgs e)
        {
            // Set the DataGridView's data source
            dgvPreview.DataSource = dataTable;

            // Configure the button click event handler if it's not already set in the designer
            button1.Click += Button1_Click;
        }

        // Implementation for the OK button
        private void Button1_Click(object sender, EventArgs e)
        {
            // Close the preview form
            this.Close();
        }

        // Implementation for the cell content click event
        private void dgvPreview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Add your cell content click logic here if needed
            // This is just a stub implementation to fix the error
        }

        private DataGridView dgvPreview;
        private Button button1;
    }
}