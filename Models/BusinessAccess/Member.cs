using System;
using System.Collections.Generic;
using Dapper;
using DINGSOMETHING.Models.DataAccess;
using DINGSOMETHING.Models.Helper;

namespace DINGSOMETHING.Models.BusinessAccess
{
    public class Member : IMemberRepository
    {
        
        private FileHelper fileHelper;

        private readonly Connection _connection;

        public Member() : base() {
            fileHelper = new FileHelper();
            _connection = new Connection();
        }

        #region Property
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public int IsEnable { get; set; }
        #endregion

        public int Create(Member member)
        {
            if(member == null){
                throw new ArgumentNullException();
            }

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Execute(fileHelper.GetScriptFromFile("CreateMember"),
                        new { Name = member.Name , Account = member.Account , Pwd = member.Password });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public Member Validate(Member member)
        {
            if(member == null){
                throw new ArgumentNullException();
            }

            member.Password = Helper.PasswordHelper.EncryptString(member.Password);

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();
                    member = conn.QuerySingle<Member>(fileHelper.GetScriptFromFile("ValidateMember"),
                        new { Account = member.Account , Pwd = member.Password });

                    return member;
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

        public IEnumerable<Member> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public int Update(Member member)
        {
            throw new NotImplementedException();
        }
        
    }
}