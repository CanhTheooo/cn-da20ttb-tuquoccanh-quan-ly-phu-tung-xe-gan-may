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
using VehiclePartStore.Ultils;

namespace VehiclePartStore.Views
{
    public partial class frmCustomer : Form
    {
        private CustomerService service;
        private CustomerDto entityBefore;
        public frmCustomer()
        {
            InitializeComponent();
            service = new CustomerService();
            entityBefore = new CustomerDto();
        }
        public void SetDataGridView()
        {
            var data = service.GetAll();
            Invoke(new Action(() => 
            {
                dgvData.Columns.Clear();
                dgvData.DataSource = null;
                dgvData.DataSource = data;
                dgvData.Columns["CustomerName"].HeaderText = "Tên Khách Hàng";
                dgvData.Columns["Description"].HeaderText = "Ghi Chú";
                dgvData.Columns["CustomerAddress"].HeaderText = "Địa Chỉ";
                dgvData.Columns["CustomerPhone"].HeaderText = "SĐT";
                dgvData.Columns["Index"].HeaderText = "STT";
                dgvData.Columns["CustomerId"].HeaderText = "Mã Khách Hàng";
                dgvData.Columns["CreatedDateTime"].HeaderText = "Ngày Tạo";

                txtCustomerName.Clear();
                txtCustomerAddress.Clear();
                txtCustomerPhone.Clear();
                txtDescription.Clear();
            }));
           
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerDto entity = new CustomerDto();
            entity.CustomerName = txtCustomerName.Text.Trim();
            entity.CustomerPhone = txtCustomerPhone.Text.Trim();
            entity.CustomerAddress = txtCustomerAddress.Text.Trim();
            entity.Description = txtDescription.Text.Trim();
            if (string.IsNullOrWhiteSpace(entity.CustomerName))
            {
                Ultils.Ultils.ShowErrorOKMess();
                return;
            }            
            service.CreateCustomer(entity);
            SetDataGridView();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(SetDataGridView);
            t.Start();           
        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowafter = dgvData.Rows[e.RowIndex];
                int CustId = (int)rowafter.Cells["CustomerId"].Value;

                CustomerDto entityAfter = new CustomerDto();
                entityAfter.CustomerName = rowafter.Cells["CustomerName"].Value.ToString();
                entityAfter.CustomerPhone = rowafter.Cells["CustomerPhone"].Value.ToString();
                entityAfter.CustomerAddress = rowafter.Cells["CustomerAddress"].Value.ToString();
                entityAfter.Description = rowafter.Cells["Description"].Value.ToString();



                if (Ultils.Ultils.IsEqual(entityBefore, entityAfter) == false)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Thay đổi giá trị ?", "Question");
                    if (rs == DialogResult.Yes)
                    {
                        if (service.IsInUse(CustId) == false)
                        {
                            CustomerDto entity = new CustomerDto();
                            entity.CustomerId = CustId;
                            entity.CustomerName = entityAfter.CustomerName;
                            entity.CustomerPhone = entityAfter.CustomerPhone;
                            entity.CustomerAddress = entityAfter.CustomerAddress;
                            entity.Description = entityAfter.Description;

                            if (CustomerValidated(entity) == Constants.OK)
                            {
                                service.EditCustomer(entity);
                                SetDataGridView();
                            }
                        }
                        return;
                    }
                    else
                    {
                        rowafter.Cells["CustomerName"].Value = entityBefore.CustomerName;
                        rowafter.Cells["CustomerAddress"].Value = entityBefore.CustomerAddress;
                        rowafter.Cells["CustomerPhone"].Value = entityBefore.CustomerPhone;
                        rowafter.Cells["Description"].Value = entityBefore.Description;
                    }
                }
            }
        }
        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            entityBefore = new CustomerDto();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                entityBefore.CustomerName = row.Cells["CustomerName"].Value.ToString();
                entityBefore.CustomerAddress = row.Cells["CustomerAddress"].Value.ToString();
                entityBefore.CustomerPhone = row.Cells["CustomerPhone"].Value.ToString();
                entityBefore.Description = row.Cells["Description"].Value.ToString();
            }
        }
        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Bạn muốn xoá khách hàng?");
                    if (rs == DialogResult.Yes)
                    {
                        int CustomerId = int.Parse(row.Cells["CustomerId"].Value.ToString());                        
                        if (service.IsInUse(CustomerId) == false)
                        {
                            if (service.DeleteCustomer(CustomerId))
                            {
                                Ultils.Ultils.ShowInfoOKMess();
                            }
                            else
                            {
                                Ultils.Ultils.ShowErrorOKMess();
                            }
                        }
                        else
                        {
                            MessageBox.Show($"{row.Cells["CustomerName"].Value.ToString()} đang được sử dụng!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }

                        SetDataGridView();
                    }

                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }
        public string CustomerValidated(CustomerDto dto)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(dto.CustomerName))
            {
                error += "Tên khách hàng trống\n";
            }
            else
            {
                error = Constants.OK;
            }
            return error;

        }
    }
}
