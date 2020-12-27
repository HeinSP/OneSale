using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Mall : Element
    {
        public Mall() { }
        public Mall(SqlDataReader reader)
        {
            GetThis(reader);
        }
        public override T Load<T>(SqlDataReader reader)
        {
            return GetThis(reader) as T;
        }
        private Mall GetThis(SqlDataReader reader)
        {
            this.num = long.Parse(reader[0].ToString());
            caption = reader[1].ToString();
            mallPCAddress = reader[2].ToString();
            mallMobileAddress = reader[3].ToString();
            address = mallPCAddress;
            string imageAddress = "ms-appx:///Assets/Malls/" + num.ToString("00") + ".png";
            imageUri = new Uri(imageAddress);
            return this;
        }
        string mallPCAddress, mallMobileAddress;
        string address;
        Uri imageUri;
        public string MallPCAddress { get => mallPCAddress; set => mallPCAddress = value; }
        public string MallMobileAddress { get => mallMobileAddress; set => mallMobileAddress = value; }
        public string Address { get => address; set => address = value; }
        public Uri ImageUri { get => imageUri; set => imageUri = value; }
    }
}
