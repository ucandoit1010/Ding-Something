using System;
using System.Collections.Generic;


namespace DINGSOMETHING.Models.BusinessAccess
{
    public interface IMemberRepository
    {
        int Create(Member member);
        
        IEnumerable<Member> GetById(string id);

        int Update(Member member);

        Member Validate(Member member);
        
    }

}