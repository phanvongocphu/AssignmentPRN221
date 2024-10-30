using FStore.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.DAO
{
    public class MemberDAO
    {
        private AppDbContext appDbContext;
        private static MemberDAO instance = null;
        public static MemberDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemberDAO();
                }
                return instance;
            }
        }
        public MemberDAO()
        {
            appDbContext = new AppDbContext();
        }
        public Member GetMember(string email)
        {
            return appDbContext.Members.SingleOrDefault(e => e.Email.Equals(email));
        }
        public Member GetMemberById(int id)
        {
            return appDbContext.Members.SingleOrDefault(m => m.MemberId.Equals(id));
        }
        public bool UpdateMember(Member member)
        {
            bool isSuccess = false;
            try
            {
                Member memberToUpdate = appDbContext.Members.SingleOrDefault(m => m.MemberId == member.MemberId);

                if (memberToUpdate != null)
                {
                    appDbContext.Members.Update(member);
                    appDbContext.SaveChanges();
                    appDbContext.Entry(member).State = EntityState.Detached;
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error updating member: " + ex.Message);
            }
            return isSuccess;
        }
    }
}
