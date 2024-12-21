using de01.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace de01
{
    public partial class frmsinhvien : Form
    {
        private quanlysv db=new quanlysv();
        public frmsinhvien()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            using (var db = new quanlysv())
            {
                var data = db.Sinhviens.Select(sv => new
                {
                    sv.MaSV,
                    sv.HoTenSV,
                    sv.NgaySinh,
                    TenLop = sv.Lop.TenLop
                }).ToList();

                dgvSinhvien.DataSource = data;

                // Nạp dữ liệu vào ComboBox
                cbolop.DataSource = db.Lops.ToList();
                cbolop.DisplayMember = "TenLop";
                cbolop.ValueMember = "MaLop";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            using (var db = new quanlysv())
            {
                var sinhvien = new Sinhvien
                {
                    MaSV = txtmasv.Text,
                    HoTenSV = txthotensv.Text,
                    NgaySinh = dtNgaysinh.Value,
                    MaLop = cbolop.SelectedValue.ToString()
                };

                db.Sinhviens.Add(sinhvien);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Thêm sinh viên thành công!");
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            using (var db = new quanlysv())
            {
                var sinhvien = db.Sinhviens.Find(txtmasv.Text);
                if (sinhvien != null)
                {
                    sinhvien.HoTenSV = txthotensv.Text;
                    sinhvien.NgaySinh = dtNgaysinh.Value;
                    sinhvien.MaLop = cbolop.SelectedValue.ToString();

                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                }
            }
        }

        private void dgvSinhvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhvien.Rows[e.RowIndex];
                txtmasv.Text = row.Cells["MaSV"].Value.ToString();
                txthotensv.Text = row.Cells["HoTenSV"].Value.ToString();
                dtNgaysinh.Value = DateTime.Parse(row.Cells["NgaySinh"].Value.ToString());
                cbolop.Text = row.Cells["TenLop"].Value.ToString();
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            using (var db = new quanlysv())
            {
                var sinhvien = db.Sinhviens.Find(txtmasv.Text);
                if (sinhvien != null)
                {
                    db.Sinhviens.Remove(sinhvien);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa sinh viên thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                }
            }
        }

        private void txttim_TextChanged(object sender, EventArgs e)
        {

        }

        private void btntim_Click(object sender, EventArgs e)
        {
            using (var db = new quanlysv())
            {
                string searchValue = txttim.Text.ToLower();
                var data = db.Sinhviens
                    .Where(sv => sv.HoTenSV.ToLower().Contains(searchValue))
                    .Select(sv => new
                    {
                        sv.MaSV,
                        sv.HoTenSV,
                        sv.NgaySinh,
                        TenLop = sv.Lop.TenLop
                    })
                    .ToList();

                dgvSinhvien.DataSource = data;
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn thoát không?",
                                                "Xác nhận thoát",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes) // Nếu người dùng nhấn Yes
            {
                this.Close(); // Đóng form
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvSinhvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
