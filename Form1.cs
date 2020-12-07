using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DepartmentTreeLogic;

namespace DepartmentTree
{
    public partial class Form1 : Form
    {
        DepartmentTreeController controller;
        List<string> departmentNames;
        public Form1()
        {
            InitializeComponent();
            controller = new DepartmentTreeController();
            departmentNames = new List<string>();

            UpdateWindow();
        }

        private void UpdateWindow()
        {
            LoadDepartmentTree();
            UpdateComboBox();
            treeViewBox.ExpandAll();
        }



        private List<string> UpdateNameList()
        {
            departmentNames.Clear();
            var departments = controller.LoadDepartments();

            foreach (var item in departments)
            {
                departmentNames.Add(item.Name);
            }

            departmentNames.Add("_Neu");

            departmentNames.Sort();

            return departmentNames;
        }

        private void UpdateComboBox()
        {
            comboBox.DataSource = null;
            comboBox.DataSource = UpdateNameList();
        }


        private void button_Click(object sender, EventArgs e)
        {
            var name = textBox.Text;
            var parent = comboBox.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(name))
            {
                controller.AddNewDepartdment(name, parent);
            }
            textBox.Text = "";
            UpdateWindow();

        }

        private void LoadDepartmentTree()
        {
            treeViewBox.Nodes.Clear();
            treeViewBox.BeginUpdate();

            var parentless = controller.LoadDepartments().Select(x => x).Where(x => x.Parent_Id == 0).OrderBy(x => x.Name);

            foreach (var item in parentless)
            {
                var node = new TreeNode(item.Name);
                treeViewBox.Nodes.Add(node);
                AddChildDepartment(item.Id, ref node);
            }

            treeViewBox.EndUpdate();
        }

        private void AddChildDepartment(int id, ref TreeNode parentNode)
        {
            var childDepartments = controller.LoadDepartments().Select(x => x).Where(x => x.Parent_Id == id).OrderBy(x => x.Name);

            foreach (var item in childDepartments)
            {
                var node = new TreeNode(item.Name);
                parentNode.Nodes.Add(node);
                AddChildDepartment(item.Id, ref node);
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            var selection = treeViewBox.SelectedNode.Text;

            if (!string.IsNullOrEmpty(selection)) { }
            controller.DeleteSelection(selection);

            UpdateWindow();
        }
    }
}

