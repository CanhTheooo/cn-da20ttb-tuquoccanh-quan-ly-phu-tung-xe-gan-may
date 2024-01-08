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
    public partial class frmProduct : Form
    {
        private ProductService service;
        private CategoryService categoryService;
        private string ProductNameBefore = "";
        private int CategoryBefore = -1;
        private string UnitPriceBefore = "";
        public frmProduct()
        {
            InitializeComponent();
            service = new ProductService();
            categoryService = new CategoryService();
        }
        private void initComboBox()
        {
            var data = categoryService.GetAll();
            BeginInvoke(new Action(() =>
            {
                cbCategory.DataSource = null;
                cbCategory.DataSource = data;
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.ValueMember = "CategoryId";
            }));

        }
        public void SetDataGridView()
        {
            var data = service.GetAll();
            Invoke(new Action(() =>
            {
                dgvData.Columns.Clear();
                dgvData.DataSource = null;
                dgvData.DataSource = data;
                dgvData.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
                dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";
                dgvData.Columns["CategoryId"].Visible = false;
                dgvData.Columns["ProductId"].Visible = false;

                dgvData.Columns["Category"].Visible = false;

                dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
                txtProductName.Clear();
                txtUnitPrice.Clear();

                //Create Combobox Columns
                DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
                cmbCol.HeaderText = "Phân Loại";
                cmbCol.Name = "CBCategoryName";
                cmbCol.DisplayMember = "CategoryName";
                cmbCol.ValueMember = "CategoryId";
                cmbCol.Items.Add("True");
                cmbCol.DataSource = categoryService.GetAll();
                // Add to datagridview
                if (dgvData.Columns.Contains("CBCategoryName") == false)
                {
                    dgvData.Columns.Insert(3, cmbCol);
                }
                else
                {
                    dgvData.Columns["CBCategoryName"].DisplayIndex = 3;
                }
                // Assign value data
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    row.Cells["CBCategoryName"].Value = row.Cells["CategoryId"].Value;
                }

            }));
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDto product = new ProductDto();
            product.ProductName = txtProductName.Text;
            product.UnitPrice = int.Parse( txtUnitPrice.Text);
            product.CategoryId = (int)cbCategory.SelectedValue;

            service.CreateProduct(product);
            SetDataGridView();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            Thread t1 = new Thread(initComboBox);
            Thread t2 = new Thread(SetDataGridView);
            t1.Start();
            t2.Start();            

        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowafter = dgvData.Rows[e.RowIndex];
                int ProductId = (int) rowafter.Cells["ProductId"].Value;
                string ProductNameAfter = rowafter.Cells["ProductName"].Value.ToString();
                string UnitPriceAfter = rowafter.Cells["UnitPrice"].Value.ToString();
                int CategoryAfter = (int) rowafter.Cells["CBCategoryName"].Value;
                if (ProductNameAfter != ProductNameBefore || UnitPriceBefore != UnitPriceAfter || CategoryBefore != CategoryAfter)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Thay đổi giá trị ?", "Question");
                    if (rs == DialogResult.Yes)
                    {
                        //if (service.IsInUse(new List<int> { ProductId }) == false)
                        //{
                            ProductDto entity = new ProductDto();
                            entity.CategoryId = CategoryAfter;
                            entity.ProductId = ProductId;
                            entity.ProductName = ProductNameAfter;
                            entity.UnitPrice =  decimal.Parse(UnitPriceAfter);
                            if (service.EditProduct(entity))
                            {
                                Ultils.Ultils.ShowInfoOKMess();
                            }
                        //}

                    }
                    else
                    {
                        rowafter.Cells["ProductName"].Value = ProductNameBefore;
                        rowafter.Cells["UnitPrice"].Value = UnitPriceBefore;
                        rowafter.Cells["CBCategoryName"].Value = CategoryBefore;                             
                    }
                }

            }
        }
        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            ProductNameBefore = "";
            UnitPriceBefore = "";
            CategoryBefore = -1;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                ProductNameBefore = row.Cells["ProductName"].Value.ToString();
                UnitPriceBefore = row.Cells["UnitPrice"].Value.ToString();
                CategoryBefore = (int)row.Cells["CBCategoryName"].Value;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetDataGridView();
            initComboBox();
        }
        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                List<int> listProductId = new List<int>();
                DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Bạn muốn xoá những sản phẩm này?");
                if (rs == DialogResult.Yes)
                {
                    //add list
                    foreach (DataGridViewRow row in dgvData.SelectedRows)
                    {
                        listProductId.Add(int.Parse(row.Cells["ProductId"].Value.ToString()));
                    }

                    // remove list
                    string result = service.IsInUse(listProductId);
                    if ( result == Constants.OK)
                    {
                        if (service.DeleteProduct(listProductId))
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
                        MessageBox.Show(result, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    SetDataGridView();
                }
            }
        }

    
    }
}
