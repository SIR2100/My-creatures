/*    
    4. Написать визитор, меняющий имя переменной с одного на другое
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
