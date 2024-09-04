namespace Yonii.Unity.Utilities
{
    public class BasicIdGenerator
    {
        private int _nextId = 1;

        public int GenerateId(int id = 0)
        {
            if (id == 0)
                id = _nextId++;

            return id;
        }
    }
}