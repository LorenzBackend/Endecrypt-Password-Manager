using Endecrypt.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endecrypt.Core
{
    public class Storage : IStorage
    {
        private string PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\explorer";
        private string rulePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\explorer";
        
        string getRule()
        {
            if (File.Exists(rulePath))
            {
                return File.ReadAllText(rulePath);
            }
            else
            {
                string rule = "!#$%()*+,-./0123456789:;<=>@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
                string result = "";
                Random r = new Random();
                for (int x = 0; x < rule.Length; x++)
                {
                    result += rule[r.Next(0, rule.Length)];
                }

                File.WriteAllText(rulePath, result);
                return result;
            }
        }
        public void RefreshData()
        {
            try
            {
                IEncryptor encryptor = new Encryptor();
                File.WriteAllText(PATH, encryptor.Encrypt(JsonConvert.SerializeObject(CurrentData.currentData), getRule()));
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }     
    }
        public void AddPassword(string title, string accountName, string password, string image)
        {
            try
            {
                IEncryptor encryptor = new Encryptor();
                CurrentData.currentData.Add(new PasswordObject { Title = title, AccountName = accountName, Password = password, ImageSrc = image});
                File.WriteAllText(PATH, encryptor.Encrypt(JsonConvert.SerializeObject(CurrentData.currentData), getRule()));
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }
       
        }

       List<PasswordObject> IStorage.LoadPasswords()
        {
            try
            {
                if (!File.Exists(PATH)) return null;
                IEncryptor encryptor = new Encryptor();
                string data = encryptor.Decrypt(File.ReadAllText(PATH), getRule());
                List<PasswordObject> dataList = JsonConvert.DeserializeObject<List<PasswordObject>>(data);
                CurrentData.currentData = dataList;
                return dataList;
            }
            catch
            {
                return null;
            }
          
        }

        public void ChangePassword(string title, string accountName, string password, string image, int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePassword(int id)
        {
            throw new NotImplementedException();
        }

    }
}
