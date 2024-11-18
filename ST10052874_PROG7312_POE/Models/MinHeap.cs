namespace ST10052874_PROG7312_POE.Models
{
    public class MinHeap
    {
        private List<Issue> _heap = new List<Issue>();

        public void Add(Issue issue)
        {
            _heap.Add(issue);
            int currentIndex = _heap.Count - 1;

            while (currentIndex > 0)
            {
                int parentIndex = (currentIndex - 1) / 2;
                if (_heap[currentIndex].status.CompareTo(_heap[parentIndex].status) >= 0)
                    break;

                Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }
        public List<Issue> GetAll()
        {
            return new List<Issue>(_heap); // Returns a copy of the heap
        }
    }

}
