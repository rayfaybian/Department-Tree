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
                departmentNames.Add(item.name);
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

            var parentless = controller.LoadDepartments().Select(x => x).Where(x => x.parent_id == null).OrderBy(x => x.name);

            foreach (var item in parentless)
            {
                var node = new TreeNode(item.name);
                treeViewBox.Nodes.Add(node);
                AddChildDepartment(item.id, ref node);
            }

            treeViewBox.EndUpdate();
        }

        private void AddChildDepartment(int id, ref TreeNode parentNode)
        {
            var childDepartments = controller.LoadDepartments().Select(x => x).Where(x => x.parent_id == id).OrderBy(x => x.name);

            foreach (var item in childDepartments)
            {
                var node = new TreeNode(item.name);
                parentNode.Nodes.Add(node);
                AddChildDepartment(item.id, ref node);
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

