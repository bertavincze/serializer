using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializer
{
    [Serializable]
    class Person : ISerializable, IDeserializationCallback
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateRecorded { get; set; }

        [NonSerialized]
        public int serialNum;

        public Person(string name, string address, string phoneNumber, int serialNum)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            DateRecorded = DateTime.Now;
            this.serialNum = serialNum;
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            PhoneNumber = (string)info.GetValue("Phone Number", typeof(string));
            DateRecorded = (DateTime)info.GetValue("Date Recorded", typeof(DateTime));
        }

        public void Serialize()
        {
            Stream stream = File.Open(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\person" + serialNum + ".dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public Person DeSerialize(int serialNum)
        {
            Stream stream = new FileStream(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\person" + serialNum + ".dat", FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            return (Person)formatter.Deserialize(stream);
        }

        public Person DeSerialize()
        {
            string[] directories = Directory.GetFiles(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer");
            int serialNum = directories.Length;
            Stream stream = new FileStream(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\person" + serialNum + ".dat", FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            return (Person)formatter.Deserialize(stream);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Address", Address);
            info.AddValue("Phone Number", PhoneNumber);
            info.AddValue("Date Recorded", DateRecorded);
        }

        public void OnDeserialization(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
