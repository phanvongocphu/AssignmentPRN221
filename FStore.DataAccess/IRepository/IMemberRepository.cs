using FStore.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.IRepository
{
    public interface IMemberRepository
    {
        public Member GetMember(string email);
        public Member GetMemberById(int id);
        bool UpdateMember(Member member);
    }
}
