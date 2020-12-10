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

        public List<departments> LoadDepartments()
        {
            List<departments> departmentList = new List<departments>();

            foreach (var element in entity.departments)
            {                
                var newDepartment = new departments() { id = element.id, name = element.name, parent_id = element.parent_id };

                departmentList.Add(newDepartment);
            }
            return departmentList;
        }

        public void AddNewDepartdment(string name, string parent)
        {
            int? parentId;

            if (parent.Equals("_Neu"))
            {

                parentId = null;
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
            var depToDelete = entity.departments.FirstOrDefault(x => x.name == name);
            DeleteDep(depToDelete);

            entity.SaveChanges();
        }

        private void DeleteDep(departments dep)
        {
            var childDeps = dep.departments1.ToList();

            foreach(var childDep in childDeps)
            {
                DeleteDep(childDep);
            }

            entity.departments.Remove(dep);
        }

        private int FindID(string name)
        {
            var parentDepartment = entity.departments.FirstOrDefault(x => x.name == name);
            var id = parentDepartment.id;

            return id;
        }
    }
}
