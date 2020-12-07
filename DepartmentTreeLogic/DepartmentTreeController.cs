using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentTreeData;

namespace DepartmentTreeLogic
{
    public class DepartmentTreeController
    {
        departmentsEntities entity;
        public DepartmentTreeController()
        {
            entity = new departmentsEntities();
        }

        public List<department> LoadDepartments()
        {
            List<department> departmentList = new List<department>();

            foreach (var element in entity.departments)
            {
                var newDepartment = new department(element.id, element.name, element.parent_id);

                departmentList.Add(newDepartment);
            }
            return departmentList;
        }

        public void AddNewDepartdment(string name, string parent)
        {
            int parentId;

            if (parent.Equals("_Neu"))
            {
                parentId = 0;
            }
            else
            {
                parentId = FindID(parent);
            }

            departments department = new departments() { name = name, parent_id = parentId };
            entity.departments.Add(department);
            entity.SaveChanges();
        }

        public void DeleteSelection(string name)
        {
            var id = FindID(name);
            int childId = id;

            foreach (var item in entity.departments)
            {
                if (item.id.Equals(id))
                {
                    entity.departments.Remove(item);
                }
                if (item.parent_id.Equals(id))
                {
                    childId = item.id;
                    entity.departments.Remove(item);
                }
                if (childId.Equals(item.id))
                {
                    entity.departments.Remove(item);
                }

            }
            entity.SaveChanges();
        }

        private int FindID(string name)
        {
            var parentDepartment = entity.departments.FirstOrDefault(x => x.name == name);
            var id = parentDepartment.id;

            return id;
        }
    }
}
