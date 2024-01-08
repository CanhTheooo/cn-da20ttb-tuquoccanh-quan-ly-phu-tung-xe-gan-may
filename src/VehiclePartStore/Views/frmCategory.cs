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
    public partial class frmCategory : Form
    {
        private CategoryService service;
        private string CatNameBefore = "";
        private string DescriptionBefore = "";
        public frmCategory()
        {
            InitializeComponent();
            service = new CategoryService();
        }
        public void SetDataGridView()
        {
            var data = service.GetAll();
            Invoke(new Action(() => 
            {
                dgvData.Columns.Clear();
                dgvData.DataSource = null;
                dgvData.DataSource = data;
                dgvData.Columns["CategoryName"].HeaderText = "Tên Loại";
                dgvData.Columns["CategoryDescription"].HeaderText = "Ghi Chú";
                dgvData.Columns["CategoryId"].Visible = false;

                txtCategoryName.Clear();
                txtCategoryDescription.Clear();
            }));
           
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryDto entity = new CategoryDto();
            entity.CategoryName = txtCategoryName.Text;
            entity.CategoryDescription = txtCategoryDescription.Text;
            service.CreateCategory(entity);
            SetDataGridView();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(SetDataGridView);
            t.Start();           
        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowafter = dgvData.Rows[e.RowIndex];
                int CatId = (int) rowafter.Cells["CategoryId"].Value;
                string CatNameAfer = rowafter.Cells["CategoryName"].Value.ToString();
                string DescAfter = rowafter.Cells["CategoryDescription"].Value.ToString();
                if (CatNameAfer != CatNameBefore || DescriptionBefore != DescAfter)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Thay đổi giá trị ?", "Question");
                    if (rs == DialogResult.Yes)
                    {
                        if (service.IsInUse(CatId) == false)
                        {
                            CategoryDto entity = new CategoryDto();
                            entity.CategoryId = CatId;
                            entity.CategoryName = CatNameAfer;
                            entity.CategoryDescription = DescAfter;
                            service.EditCategory(entity);
                            SetDataGridView();
                        }
                        return;
                    }
                    else
                    {
                        rowafter.Cells["CategoryName"].Value = CatNameBefore;
                        rowafter.Cells["CategoryDescription"].Value = DescriptionBefore;
                    }
                }

            }
        }
        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CatNameBefore = "";
            DescriptionBefore = "";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                CatNameBefore = row.Cells["CategoryName"].Value.ToString();
                DescriptionBefore = row.Cells["CategoryDescription"].Value.ToString();
            }
        }
        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    DialogResult rs = Ultils.Ultils.ShowInfoYesNoMess("Bạn muốn xoá phân loại này?");
                    if (rs == DialogResult.Yes)
                    {
                        int CategoryId = (int) row.Cells["CategoryId"].Value;

                        if (service.IsInUse(CategoryId) == false)
                        {
                            if (service.DeleteCategory(CategoryId))
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
                            MessageBox.Show($"Phân loại: {row.Cells["CategoryName"].Value.ToString()} đang được sử dụng!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }

                        SetDataGridView();
                    }

                }
            }
        }
    }
}
