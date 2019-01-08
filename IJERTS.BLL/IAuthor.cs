﻿using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.BLL
{
    public interface IAuthor
    {
        void Register(Users user);

        void GetAllUsers(List<Users> users);

        int PostPapers(Paper paper);

        List<Paper> TrackMyPaper(int userId);
    }
}