using System;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace Yonii.Unity.Utilities
{
    [Serializable]
    public struct SerializableGuid : IEquatable<SerializableGuid>
    {
        [SerializeField, HideInInspector] public uint Part1;
        [SerializeField, HideInInspector] public uint Part2;
        [SerializeField, HideInInspector] public uint Part3;
        [SerializeField, HideInInspector] public uint Part4;
        
        public static SerializableGuid Empty => new(0,0,0,0);
        public static SerializableGuid NewGuid() => Guid.NewGuid().ToSerializableGuid();
        
        public SerializableGuid(uint val1, uint val2, uint val3, uint val4) 
        {
            Part1 = val1;
            Part2 = val2;
            Part3 = val3;
            Part4 = val4;
        }
        
        public SerializableGuid(Guid guid) 
        {
            var bytes = guid.ToByteArray();
            Part1 = BitConverter.ToUInt32(bytes, 0);
            Part2 = BitConverter.ToUInt32(bytes, 4);
            Part3 = BitConverter.ToUInt32(bytes, 8);
            Part4 = BitConverter.ToUInt32(bytes, 12);
        }
        
        public static SerializableGuid FromHexString(string hexString) 
        {
            if (hexString.Length != 32) {
                return Empty;
            }

            return new SerializableGuid
            (
                Convert.ToUInt32(hexString.Substring(0, 8), 16),
                Convert.ToUInt32(hexString.Substring(8, 8), 16),
                Convert.ToUInt32(hexString.Substring(16, 8), 16),
                Convert.ToUInt32(hexString.Substring(24, 8), 16)
            );
        }
        
        public bool Equals(SerializableGuid other)
        {
            return Part1 == other.Part1 &&
                   Part2 == other.Part2 &&
                   Part3 == other.Part3 &&
                   Part4 == other.Part4;
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableGuid other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Part1, Part2, Part3, Part4);
        }
        
        public static bool operator ==(SerializableGuid left, SerializableGuid right) => left.Equals(right);
        public static bool operator !=(SerializableGuid left, SerializableGuid right) => !(left == right); 
    }
}