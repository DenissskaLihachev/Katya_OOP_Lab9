namespace MyList
{
    interface IListQueue
    {
        public void Add(Person person);
        public Person? Remove();
        public void GetMassive();
    }
    public static class Colors
    {
        public static ConsoleColor defaultForeground = Console.ForegroundColor;
        public static ConsoleColor defaultBackground = Console.BackgroundColor;
    }
    public class Person
    {
        private string? Name { get; }
        private string? Surname { get; }
        private string? Patronymic { get; }

        public Person(string? Name, string? Surname, string? Patronymic)
        {
            this.Name = Name ?? this.Name;
            this.Surname = Surname ?? this.Surname;
            this.Patronymic = Patronymic ?? this.Patronymic;
        }
        public Person(Person? person)
        {
            if (person is not null)
            {
                this.Name ??= person.Name;
                this.Surname ??= person.Surname;
                this.Patronymic ??= person.Patronymic;
            }
        }
        public override string ToString()
        {
            return (Name ?? "No name") 
                + " " + (Surname ?? "No surname") 
                + " " + (Patronymic ?? "No patronymic");
        }
    }
    public class Node
    {
        public Person? Data { get; set; }
        public Node? Next { get; set; }
        public Node? Prev { get; set; }
        public override string? ToString()
        {
            return Data?.ToString();
        }
    }
    public class ListQueue : IListQueue
    {
        public delegate void Handler(string message);
        public event Handler? Notify;

        private Node? head = null;
        private Node? tail = null;
        int count = 0;
        public int Count
        {
            get { return count; }
            private set
            {
                if (count < 0) throw new ArgumentNullException("count", "Счётчик не может быть меньше нуля");
                count = value;
            }
        }
        public void Add(Person? person)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < Count; i++)
            {
                if (head is null || tail is null) break;

                if (Get(i) is null)
                {
                    Set(person, i);
                    return;
                }
            }
            Node? node = new()
            {
                Data = person
            };
            if (head is null)
                head = node;
            else if (head.Next is null)
                head.Next = node;

            if (tail is not null)
                tail.Next = node;
            node.Prev = tail;
            tail = node;

            Notify.Invoke($"Добавлен новый элемент [{person}]");

            count++;
        }
        public void Add(Person?[] persons)
        {
            foreach(var person in persons)
            {
                this.Add(person);
            }
        }
        public Person? Remove()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (head is null || tail is null)
            {
                Console.WriteLine("ListQueue is empty");
                tail = null;
                return null;
            }
            var person = head.Data;

            Notify.Invoke($"Удален элемент [{person}]");

            if (person == null)
            {
                Console.WriteLine("ListQueue is empty");
                return null;
            }
            head = head.Next;
            head!.Prev = null;

           
            count--;
            return person;
        }
        public Person? Get(int index)
        {
            if (head is null || tail is null)
            {
                Console.WriteLine("ListQueue is empty");
                return null;
            }
            if (index >= count)
            {
                Console.WriteLine("Out in the range");
                return null;
            }

            Node? cur = head;
            for (int i = 0; i < count; i++)
            {
                if (index == i)
                {
                    return cur?.Data;
                }
                cur = cur?.Next;
            }
            return null;
        }
        public void Set(Person? person, int index)
        {
            //if (person == null)
            //{
            //    Console.WriteLine("Person is empty");
            //    return;
            //}
            if (head is null || tail is null)
            {
                Console.WriteLine("ListQueue is empty");
                return;
            }
            if (index >= count)
            {
                Console.WriteLine("Out in the range");
                return;
            }

            Node? cur = head;
            for (int i = 0; i < count; i++)
            {
                if (index == i)
                {
                    cur!.Data = person;
                    return;
                }
                cur = cur?.Next;
            }
            return;
        }
        public void GetMassive()
        {
            Print();
        }
        public Person? Null(int index)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (head is null || tail is null)
            {
                Console.WriteLine("ListQueue is empty");
                return null;
            }
            if (index >= count)
            {
                Console.WriteLine("Out in the range");
                return null;
            }

            Node? cur = head;
            for (int i = 0; i < count; i++)
            {
                if (index == i)
                {
                    Person? person = cur?.Data;
                    count = person is null ? count: count - 1;

                    Notify.Invoke($"Занулен элемент [{person}]");

                    cur!.Data = null;
                    return person;
                }
                cur = cur?.Next;
            }
            return null;
        }
        public void Sort()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 1; i < count; i++)
            {
                Person? cur = Get(i);

                if (cur is null)
                {
                    Console.Write("");
                }

                for (int j = i; j >= 0; j--)
                {
                    //if (Get(j - 1) is null || Get(j) is null) continue;
                    Set(Get(j - 1), j);
                    if (j == 0)
                    {
                        Set(cur, j);
                        break;
                    }
                    if (cur is null ||
                        cur.ToString()[0] >= Get(j - 1)?.ToString()[0])
                    {
                        Set(cur, j);
                        break;
                    }
                }
            }
            Notify.Invoke("Список был отсортирован");
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Node? cur = head;
            for (int i = 0; i < count; i++)
            {
                if (cur?.Data == null) Console.WriteLine("Пусто");
                else Console.WriteLine(cur?.Data?.ToString());
                cur = cur?.Next;
            }
            Console.WriteLine();
            Notify.Invoke("Был выведен массив");
        }
    }
}