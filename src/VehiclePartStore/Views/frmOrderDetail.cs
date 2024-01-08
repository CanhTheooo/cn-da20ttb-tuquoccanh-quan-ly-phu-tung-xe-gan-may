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
    public partial class frmOrderDetail : Form
    {
        private ProductService productService;
        private CategoryService categoryService;
        private OrderService orderService;
        private OrderHeaderDto od;                
        private OrderDetailDto entityBefore;
        public event LoadGridHeader loadGridHeader;
        public delegate void LoadGridHeader();
        public frmOrderDetail(OrderHeaderDto od)
        {
            InitializeComponent();
            this.od = od;
            productService = new ProductService();
            orderService = new OrderService();
            categoryService = new CategoryService();
            this.Text += $" - Khách hàng: {od.CustomerName} - Mã đơn hàng: {od.InternalOrderNum}";
        }
        private void InitComboBox()
        {
            var data = categoryService.GetAll();
            var prods = productService.GetAll();

            Invoke(new Action(() =>
            {
                cbCategory.DataSource = null;
                cbCategory.DataSource = data;
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.ValueMember = "CategoryId";
                cbCategory.SelectedIndex = 0;


                cbProduct.DataSource = null;
                cbProduct.DataSource = prods;
                cbProduct.DisplayMember = "ProductName";
                cbProduct.ValueMember = "ProductId";
                cbProduct.SelectedIndex = 0;
            }));
            cbCategory_SelectionChangeCommitted(null, null);
        }      
        private void SetDataGridView()
        {
            var data = orderService.GetOrderDetail(od.InternalOrderNum);
            Invoke(new Action(() =>
            {
                dgvData.Columns.Clear();
                dgvData.DataSource = null;
                dgvData.DataSource = data;
                dgvData.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
                dgvData.Columns["ProductId"].HeaderText = "Mã Sản Phẩm";
                dgvData.Columns["CategoryName"].HeaderText = "Phân Loại";
                dgvData.Columns["OrderQty"].HeaderText = "Số Lượng";
                dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";
                dgvData.Columns["Totalprice"].HeaderText = "Tổng";

                dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
                dgvData.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";

                dgvData.Columns["InternalOrderNum"].Visible = false;
                dgvData.Columns["InternalOrderLineNum"].Visible = false;
                dgvData.Columns["Category"].Visible = false;
                dgvData.Columns["CategoryId"].Visible = false;
                dgvData.Columns["Product"].Visible = false;

                dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dgvData.AutoResizeColumns();

                txtOrderQty.Clear();

            }));
        }
        private void InitForm()
        {
            InitComboBox();
            SetDataGridView();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ((int)cbCategory.SelectedValue == -1)
            {
                return;
            }
            if ((int)cbProduct.SelectedValue == -1)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOrderQty.Text.Trim()))
            {
                return;
            }
            
            OrderDetailDto entity = new OrderDetailDto()
            {
                InternalOrderNum = od.InternalOrderNum,
                ProductId = (int)cbProduct.SelectedValue,
                ProductName = cbProduct.Text,
                CategoryId = (int)cbCategory.SelectedValue,
                CategoryName = cbCategory.Text,
                OrderQty = int.Parse(txtOrderQty.Text.ToString()),
                UnitPrice = ((ProductDto)cbProduct.SelectedItem).UnitPrice,
                TotalPrice = ((ProductDto)cbProduct.SelectedItem).UnitPrice * int.Parse(txtOrderQty.Text.ToString())

            };

            if (orderService.CreateOrderDetail(entity))
            {
                Ultils.Ultils.ShowInfoOKMess();
                InitForm();

            }
            else
            {
                
            }
        }

        private void frmOrderDetail_Load(object sender, EventArgs e)
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
        

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (orderService.EditOrderHeader(od))
            {
                Ultils.Ultils.ShowInfoOKMess("Thanh toán thành công!", "Information");
                loadGridHeader();
                this.Close();
            }
        }
        private void cbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            Invoke(new Action(() =>
           {
               cbProduct.DataSource = null;
               cbProduct.DataSource = productService.Get(new CommonFilterDto { CategoryId = (int)cbCategory.SelectedValue });
               cbProduct.DisplayMember = "ProductName";
               cbProduct.ValueMember = "ProductId";
           }));
            
        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowafter = dgvData.Rows[e.RowIndex];
                int InternalOrderLineNum = (int)rowafter.Cells["InternalOrderLineNum"].Value;

                OrderDetailDto entityAfter = new OrderDetailDto();
                entityAfter.OrderQty = (int)rowafter.Cells["OrderQty"].Value;


                if (Ultils.Ultils.IsEqual(entityBefore, entityAfter) == false)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Thay đổi số lượng ?");
                    if (rs == DialogResult.Yes)
                    {
                        OrderDetailDto entity = new OrderDetailDto();
                        entity.InternalOrderLineNum = InternalOrderLineNum;
                        entity.Product = (ProductDto)rowafter.Cells["Product"].Value;
                        entity.OrderQty = entityAfter.OrderQty;
                        orderService.EditOrderDetail(entity);
                        Ultils.Ultils.ShowInfoOKMess();
                        SetDataGridView();
                    }
                    else
                    {
                        rowafter.Cells["OrderQty"].Value = entityBefore.OrderQty;
                    }
                }
            }
        }
        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            entityBefore = new OrderDetailDto();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                entityBefore.OrderQty = (int) row.Cells["OrderQty"].Value;
            }
        }
        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Bạn muốn xoá dòng này?");
                    if (rs == DialogResult.Yes)
                    {
                        int InternalLineNum = (int) row.Cells["InternalOrderLineNum"].Value;
                        if (orderService.DeleteOrderDetail(new OrderDetailDto { InternalOrderLineNum = InternalLineNum }))
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
