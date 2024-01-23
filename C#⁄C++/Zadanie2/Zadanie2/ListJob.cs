using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class DoublyLinkedList<T> : IEnumerable<T>  // двусвязный список
     {
            DoublyNode<T> head; // головной/первый элемент
            int count;  // количество элементов в списке

            // добавление элемента
            public void Add(T data)
            {
                DoublyNode<T> node = new DoublyNode<T>(data);

                if (head == null)
                {
                    head = node; 
                    head.Next = node;
                    head.Previous = node;
                }
                else  // переустановка пребедущего элемента по отношению к head
                { 
                    node.Previous = head.Previous;
                    node.Next = head;
                    head.Previous.Next = node;
                    head.Previous = node;
                }
                count++;
            }
            // удаление
            public bool Remove(T data)
            {
                DoublyNode<T> current = head;

                DoublyNode<T> removedItem = null;
                if (count == 0) return false;

                // поиск удаляемого узла
                do
                {
                    if (current.Data.Equals(data))
                    {
                        removedItem = current;
                        break;
                    }
                    current = current.Next;
                }
                while (current != head);

                if (removedItem != null)
                {
                    // если удаляется единственный элемент списка
                    if (count == 1)
                        head = null;
                    else
                    {
                        // если удаляется первый элемент
                        if (removedItem == head)
                        {
                            head = head.Next;
                        }
                        removedItem.Previous.Next = removedItem.Next;
                        removedItem.Next.Previous = removedItem.Previous;
                    }
                    count--;
                    return true;
                }
                return false;
            }

            public int Count { get { return count; } }
            public bool IsEmpty { get { return count == 0; } }

            public void Clear()
            {
                head = null ;
                count = 0;
            }
            public bool AddNext(T data,T info)
            {
                DoublyNode<T> into = new DoublyNode<T>(info);
                DoublyNode<T> current = head;

                if (current == null) return false;// идем по списку
                do
                {
                    if (current.Data.Equals(data))
                    {
                        into.Previous = current;// добавление элемента
                        into.Next = current.Next;
                        current.Next = into;
                        count++;
                        return true;
                    }
                    current = current.Next;
                }
                while (current != head);
                return false;
                
            }
            public bool Contains(T data) // поиск в прямом направлении
            {
                DoublyNode<T> current = head;
                if (current == null) return false;
                do
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Next;
                }
                while (current != head);
                return false;
            }
            public bool ContainsBack(T data) // поиск в обратном направлении
            {
                DoublyNode<T> current = head;
                if (current == null) return false;
                do
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Previous;
                }
                while (current != head);
                return false;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()//Вывод по часовой
        {
                DoublyNode<T> current = head;
                do
                {
                    if (current != null)
                    {
                        yield return current.Data;
                        current = current.Next;
                    }
                }
                while (current != head);
            }
            public IEnumerable<T> BackEnumerator()// Вывод против часовой
            {
                DoublyNode<T> current = head;
                do
                {

                    if (current != null)
                    { 
                        yield return current.Data;

                        current = current.Previous;
                    }
                }
                while (current != head);
            }
        }
  
}   
