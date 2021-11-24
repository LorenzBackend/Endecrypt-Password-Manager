using Endecrypt.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endecrypt.Core
{
    public interface IStorage
    {
        List<PasswordObject> LoadPasswords();
        void RefreshData();
        void AddPassword(string title, string accountName, string password, string image);
        void ChangePassword(string title, string accountName, string password, string image, int id);
        void DeletePassword(int id);
    }
}
