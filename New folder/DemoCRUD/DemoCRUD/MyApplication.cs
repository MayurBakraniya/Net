using DemoCRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoCRUD
{
    public partial class muappform : Form
    {
        public muappform()
        {
            InitializeComponent();
        }

        private void muappform_Load(object sender, EventArgs e)
        {
            DGV.DataSource = RefreshData();
        }
        private List<User> RefreshData()
        {
            Demo1Entities demo1Entities = new Demo1Entities();
            return demo1Entities.Users.ToList();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            Demo1Entities demo1Entities = new Demo1Entities();

            User user = new User();
            user.UserName = textBox1.Text;
            user.UserEmail = textBox2.Text;

            demo1Entities.Users.Add(user);
            demo1Entities.SaveChanges();

            DGV.DataSource = demo1Entities.Users.ToList();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            Demo1Entities demo1Entities = new Demo1Entities();

            int UserID = Convert.ToInt32(Id.Text);
            User users = demo1Entities.Users.Find(UserID);

            users.UserName = textBox1.Text;
            users.UserEmail = textBox2.Text;
            demo1Entities.SaveChanges();

            DGV.DataSource = demo1Entities.Users.ToList();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Demo1Entities demo1Entities = new Demo1Entities();

            int UserID = Convert.ToInt32(Id.Text);
            User users = demo1Entities.Users.Find(UserID);

            demo1Entities.Users.Remove(users);
            demo1Entities.SaveChanges();

            DGV.DataSource = demo1Entities.Users.ToList();

        }

        private void Search_Click(object sender, EventArgs e)
        {
            Demo1Entities demo1Entities = new Demo1Entities();
                     
            String Search = textBox3.Text;
            DGV.DataSource = demo1Entities.Users.Where(a => a.UserName.Contains(Search)).Select(a => new { a.UserName, a.UserEmail,a.UserID }).ToList();
            
        }

       
    }
}
