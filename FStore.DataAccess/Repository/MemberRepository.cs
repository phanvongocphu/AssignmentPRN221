using FStore.BusinessObject;
using FStore.DataAccess.DAO;
using FStore.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member GetMember(string email) => MemberDAO.Instance.GetMember(email);
        public Member GetMemberById(int id) => MemberDAO.Instance.GetMemberById(id);
        public bool UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);
    }
}
