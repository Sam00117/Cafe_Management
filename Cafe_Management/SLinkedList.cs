namespace LinkedList;

public class Node<T>
{
    public T Data;
    public Node<T>? Next;

    public Node(T value)
    {
        Data = value;
    }
}

public class SLinkedList<T>
{
    public Node<T>? Head;
    private Node<T>? Tail;
    public int Count { get; private set; }

    public SLinkedList()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }
    
    public void AddFront(T value)
    {
        Node<T> newNode = new Node<T>(value);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head = newNode;
        }
        Count++;
    }

    public void AddBack(T value)
    {
        Node<T> newNode = new Node<T>(value);
        if (Tail == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            Tail = newNode;
        }
        Count++;
    }

    public void AddAt(T value, int index)
    {
        if (index < 0 || index > Count)
        {
            Console.WriteLine("Invalid index");
            return;
        }

        if (index == 0)
        {
            AddFront(value);
        }
        else if (index == Count)
        {
            AddBack(value);
        }
        else
        {
            Node<T>? temp = Head;
            for (int i = 0; i < index - 1; i++)
            {
                temp = temp.Next;
            }
            Node<T> newNode = new Node<T>(value)
            {
                Next = temp.Next
            };
            temp.Next = newNode;
            Count++;
        }
    }

    public void RemoveFront()
    {
        if (Head == null)
        {
            Console.WriteLine("List is empty.");
            return;
        }
        Head = Head.Next;
        Count--;
        if (Head == null) Tail = null;
    }

    public void RemoveBack()
    {
        if (Head == null)
        {
            Console.WriteLine("List is empty.");
            return;
        }

        if (Head.Next == null)
        {
            Head = null;
            Tail = null;
        }
        else
        {
            Node<T> temp = Head;
            while (temp.Next != Tail)
            {
                temp = temp.Next;
            }
            temp.Next = null;
            Tail = temp;
        }
        Count--;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            Console.WriteLine("Invalid index");
            return;
        }

        if (index == 0)
        {
            RemoveFront();
        }
        else
        {
            Node<T> temp = Head;
            for (int i = 0; i < index - 1; i++)
            {
                temp = temp.Next;
            }
            temp.Next = temp.Next.Next;
            if (temp.Next == null) Tail = temp;
            Count--;
        }
    }
    
    public void Print()
    {
        Node<T>? current = Head;
        int index = 0;
        while (current != null)
        {
            Console.WriteLine($"Index {index}: {current.Data}");
            current = current.Next;
            index++;
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException("Index out of range.");
        }

        Node<T> current = Head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }
        return current.Data;
    }
}

