using lab4.khanhtoan;
using lab4.lab4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();

        }

        private void FillFacultyCombobox(List<Faculty> listFaculties)
        {
            cmbKhoa.DataSource = listFaculties;
            cmbKhoa.DisplayMember = "FacultyName";
            cmbKhoa.ValueMember = "FacultyID";
        }

        private void BindGrid (List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (Student student in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = student.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = student.StudentName;
                dataGridView1.Rows[index].Cells[2].Value = student.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = student.AverageScore;
            } 
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Model2 model2 = new Model2();
                List<Faculty> listFaculties = model2.Faculties.ToList();
                List<Student> listStudents = model2.Students.ToList();
                FillFacultyCombobox(listFaculties);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void loadDaTa()
        {
            using (Model2 model2 = new Model2())
            {
                List<Student> listStudent = model2.Students.ToList();
                BindGrid (listStudent);
            }
        }


        private void ResetFom()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtDiem.Clear();
        }



        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Đang muốn thoát","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning); 
            if (result == DialogResult.Yes)
            {
                Close();
            } 
                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbKhoa.SelectedIndex = 0;
        }
        private int GetSelectedRow(string StudentID)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null &&
                    dataGridView1.Rows[i].Cells[0].Value.ToString() == StudentID)
                {
                    return i;
                }

            }
            return -1;
        }
        private void InsertUpdate(int selectedRow)
        {
            dataGridView1.Rows[selectedRow].Cells[0].Value = txtMa.Text;
            dataGridView1.Rows[selectedRow].Cells[1].Value = txtTen.Text;
            dataGridView1.Rows[selectedRow].Cells[2].Value = cmbKhoa.Text;
            dataGridView1.Rows[selectedRow].Cells[3].Value = float.Parse(txtDiem.Text).ToString();
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDiem.Text == "" || txtTen.Text == "" || txtMa.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sinh viên !");
                int selectedRow = GetSelectedRow(txtMa.Text);
                if (selectedRow == -1)
                {
                    selectedRow = dataGridView1.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm dữ liệu thành công", "Thông Báo !", MessageBoxButtons.OK);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int slecterRow = GetSelectedRow(txtMa.Text);
                if (slecterRow == -1)
                {
                    throw new Exception("Không tìm thấy sinh viên cần xóa");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa sinh viên hay không", "Thông Báo !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dataGridView1.Rows.RemoveAt(slecterRow);
                        MessageBox.Show("Xóa thông tin sinh viên thành công","Thông Báo !",MessageBoxButtons.OK);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn cập nhật thông tin sinh viên không","Thông Báo !",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);     
            if (result == DialogResult.OK)
            {
                int slecterRow = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows[slecterRow].Cells[0].Value = txtMa.Text;
                dataGridView1.Rows[slecterRow].Cells[1].Value = txtTen.Text ;
                dataGridView1.Rows[slecterRow].Cells[2].Value = cmbKhoa.SelectedItem+"";
                dataGridView1.Rows[slecterRow].Cells[3].Value = txtDiem.Text;
                MessageBox.Show("Cập nhật dữ liệu thành công !", "Thông Báo !", MessageBoxButtons.OK);
            } 
            else
            {
                MessageBox.Show("Lỗi Không thể xóa","Thông Báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
                
        }

        
    }
}
