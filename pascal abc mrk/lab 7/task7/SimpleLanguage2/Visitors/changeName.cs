/*    
    4. �������� �������, �������� ��� ���������� � ������ �� ������
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class changeName : AutoVisitor
    {
        private string currentName, newName;
        public changeName(string curName, string newName)
        {
            this.currentName = curName;
            this.newName = newName;
        }

        public override void VisitIdNode(IdNode id)
        {
            if (id.Name == currentName)
                id.Name = newName;
            base.VisitIdNode(id);
        }
    }
}
