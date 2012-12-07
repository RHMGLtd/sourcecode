using System;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Services.Interfaces
{
    public interface IDiarise
    {
        Diary Get(DateTime date);
    }
}