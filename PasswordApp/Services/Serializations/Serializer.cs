using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordApp.Services.Serializations
{
    public static class Serializer
    {
        public static List<T> Deserialize<T>(Stream stream)
        {
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(List<T>));
                return (List<T>)serializer.ReadObject(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<T>();
            }


        }

        public static void Serialize<T>(Stream stream, List<T> objectList)
        {
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(List<T>));
                serializer.WriteObject(stream, objectList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
