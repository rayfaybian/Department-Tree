using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentTreeLogic
{
    public class department
    {
        private int _id;
        private string _name;
        private int _parent_id;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public int Parent_Id { get { return _parent_id; } }

        public department(int id, string name, int parentId)
        {
            this._id = id;
            this._name = name;
            this._parent_id = parentId;
        }
        
    }
}
