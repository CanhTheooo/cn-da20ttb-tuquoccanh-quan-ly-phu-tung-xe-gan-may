using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using VehiclePartStore.Dto;
    using VehiclePartStore.Services;

    namespace VehiclePartStore.Views
    {
        public partial class frmStatistics : Form
        {
            CommonFilterDto filter;
            StatisticsService service;
            public frmStatistics()
            {
                InitializeComponent();
                service = new StatisticsService();
            }
            private void GetFilter()
            {
                filter = new CommonFilterDto();

                filter.fromDate = dtFrom.Checked == false ? "2000-01-01" : dtFrom.Value.ToString("yyyy-MM-dd");
                filter.toDate = dtTo.Checked == false ? "4000-12-31" : dtTo.Value.ToString("yyyy-MM-dd");
            }
            public void GetData()
            {
            
            }
            private void btnView_Click(object sender, EventArgs e)
            {
                GetFilter();


                var data = new List<StatisticsDto>();

                Task.Factory.StartNew(() => 
                {
                    data = service.Get(filter);
                    Invoke(new Action(() =>
                    {
                        if (rdAll.Checked)
                        {
                            dgvData.DataSource = null;
                            dgvData.DataSource = data;

                            dgvData.Columns["Index"].HeaderText = "STT";
                            dgvData.Columns["ProductName"].HeaderText = "Sản Phẩm";
                            dgvData.Columns["CategoryName"].HeaderText = "Phân Loại";
                            dgvData.Columns["TotalQty"].HeaderText = "Số Lượng";
                            dgvData.Columns["TotalPrice"].HeaderText = "Tổng";
                            dgvData.Columns["ProductId"].HeaderText = "Mã Sản Phẩm";
                            dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";


                            dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
                            dgvData.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";

                            dgvData.Columns["CategoryId"].Visible = false;
                        }
                        else if (rdTop5.Checked)
                        {
                            var topData = data.Select(x => new { ProductName = x.ProductName, TotalQty = x.TotalQty })
                                .OrderByDescending(x => x.TotalQty)
                                .Take(5)
                                .ToList();

                            dgvData.DataSource = null;
                            dgvData.DataSource = topData;

                            dgvData.Columns["ProductName"].HeaderText = "Sản Phẩm";
                            dgvData.Columns["TotalQty"].HeaderText = "Số Lượng";

                            dgvData.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                    }));
                });

            

            
           
            }

        private void btnthongke_Click(object sender, EventArgs e)
        {
            GetFilter();

            Task.Factory.StartNew(() =>
            {
                List<StatisticsDto> data = service.Get(filter);

                Invoke(new Action(() =>
                {
                    DisplayStatisticsData(data);
                }));
            });
        }

        private void DisplayStatisticsData(List<StatisticsDto> data)
        {
            dgvData.DataSource = null;

            if (rdAll.Checked)
            {
                DisplayAllData(data);
            }
            else if (rdTop5.Checked)
            {
                DisplayTop5Data(data);
            }

            // Add any additional statistics logic here if needed
        }

        private void DisplayAllData(List<StatisticsDto> data)
        {
            dgvData.DataSource = data;

            // Add any additional columns customization if needed
        }

        private void DisplayTop5Data(List<StatisticsDto> data)
        {
            var topData = data
                .OrderByDescending(x => x.TotalQty)
                .Take(5)
                .ToList();

            dgvData.DataSource = topData;

            dgvData.Columns["ProductName"].HeaderText = "Sản Phẩm";
            dgvData.Columns["TotalQty"].HeaderText = "Số Lượng";

            dgvData.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }


        //  private void btnhoadon_Click(object sender, EventArgs e)
        //  {
        // if (dgvData.Rows.Count == 0)
        // {
        // MessageBox.Show("No data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        // return;
        // }

        //  using (var excelPackage = new ExcelPackage())
        //  {
        //     var worksheet = excelPackage.Workbook.Worksheets.Add("HoaDon");

        //   for (int col = 0; col < dgvData.Columns.Count; col++)
        // {
        //  worksheet.Cells[1, col + 1].Value = dgvData.Columns[col].HeaderText;
        //   }

        //for (int row = 0; row < dgvData.Rows.Count; row++)
        // {
        //  for (int col = 0; col < dgvData.Columns.Count; col++)
        // {
        //  worksheet.Cells[row + 2, col + 1].Value = dgvData.Rows[row].Cells[col].Value;
        // }
        // }

        // var saveFileDialog = new SaveFileDialog
        // {
        //  Filter = "Excel Files|*.xlsx|All Files|*.*",
        //  FileName = "HoaDon_Export.xlsx"
        //  };

        // if (saveFileDialog.ShowDialog() == DialogResult.OK)
        // {
        //    FileInfo file = new FileInfo(saveFileDialog.FileName);
        //   excelPackage.SaveAs(file);
        //  MessageBox.Show("Export completed successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //  }
        //}
    }
}
    

