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
    public partial class frmOrderHeader : Form
    {
        private CustomerService customerService;
        private OrderService orderService;        
        public frmOrderHeader()
        {
            InitializeComponent();
            customerService = new CustomerService();
            orderService = new OrderService();
        }
        private void InitComboBox()
        {
            var data = customerService.GetAll();
            Invoke(new Action(() => 
            {
                cbCustomer.DataSource = null;
                cbCustomer.DataSource = data;
                cbCustomer.DisplayMember = "CustomerName";
                cbCustomer.ValueMember = "CustomerId";
                cbCustomer.SelectedIndex = 0;
            }));

        }
        private void SetDataGridView()
        {
            var data = orderService.GetAllOrderHeader();
            Invoke(new Action(() =>
            {
                dgvData.Columns.Clear();
                dgvData.DataSource = null;
                dgvData.DataSource = data;
                dgvData.Columns["InternalOrderNum"].HeaderText = "Mã Đơn Hàng";
                dgvData.Columns["CustomerName"].HeaderText = "Tên KH";
                dgvData.Columns["OrderDate"].HeaderText = "Ngày Tạo Đơn";
                dgvData.Columns["PaymentDate"].HeaderText = "Ngày Thanh Toán";
                dgvData.Columns["Index"].HeaderText = "STT";
                dgvData.Columns["CustomerId"].HeaderText = "Mã KH";
                dgvData.Columns["Description"].HeaderText = "Ghi Chú";

                dgvData.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvData.Columns["PaymentDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvData.Columns["Customer"].Visible = false;
                
                txtDescription.Clear();

            }));
        }
        private void InitForm()
        {
            InitComboBox();
            SetDataGridView();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderHeaderDto entity = new OrderHeaderDto();
            entity.CustomerId = (int) cbCustomer.SelectedValue;
            entity.CustomerName = cbCustomer.Text;
            entity.Description = txtDescription.Text;

            if (entity.CustomerId == 0 || string.IsNullOrWhiteSpace(entity.CustomerName))
            {
                Ultils.Ultils.ShowErrorOKMess("Vui lòng chọn lại khách hàng!");
                return;
            }


            if (orderService.CreateOrderHeader(entity))
            {
                Ultils.Ultils.ShowInfoOKMess();
                InitForm();

            }
            else
            {
                Ultils.Ultils.ShowErrorOKMess("Vui lòng liên hệ bộ phận IT");
            }
        }

        private void frmOrderHeader_Load(object sender, EventArgs e)
        {
            Thread t1 = new Thread(InitComboBox);
            Thread t2 = new Thread(SetDataGridView);
            t1.Start();
            t2.Start();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitForm();
        }
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                if (row.Cells["PaymentDate"].Value == null)
                {
                    int internalOrderNum = (int)row.Cells["InternalOrderNum"].Value;

                    OrderHeaderDto od = new OrderHeaderDto() 
                    {
                        InternalOrderNum = internalOrderNum,
                        CustomerName = row.Cells["CustomerName"].Value.ToString(),
                        CustomerId = (int) row.Cells["CustomerId"].Value
                    };
                    frmOrderDetail frm = new frmOrderDetail(od);
                    frm.Show();
                    frm.loadGridHeader += InitForm;
                }
                else
                {
                    Ultils.Ultils.ShowErrorOKMess("Đơn hàng đã thanh toán.\nKhông thể thêm sản phẩm");
                }

            }
        }
      
        private void btnPayment_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvData.SelectedRows)
            {
                if (row != null)
                {
                    int InternalOrderNum = (int)row.Cells["InternalOrderNum"].Value;
                    if (InternalOrderNum != -1)
                    {
                        OrderHeaderDto entity = new OrderHeaderDto();
                        entity.InternalOrderNum = InternalOrderNum;

                        if (orderService.EditOrderHeader(entity))
                        {
                            Ultils.Ultils.ShowInfoOKMess();
                           
                        }
                        else
                        {
                            Ultils.Ultils.ShowErrorOKMess();
                        }
                        SetDataGridView();
                    }
                }
            }
        }
    }
}
