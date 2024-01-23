using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    class MenuStart
    {

        static public void Menu()
        {
            DoublyLinkedList<string> linkedList = new DoublyLinkedList<string>();
            Console.Clear();
            //Выводим меню, его пункты с соответствующими цифрами\символами
            int choose = 0;
            linkedList.Add("Bob");
            linkedList.Add("Bill");
            linkedList.Add("Tom");
            linkedList.Add("Kilua");
            linkedList.Add("Ulba");
            linkedList.Add("Maks");
            while (choose != 6)
            {

                Console.WriteLine("### MENU ###");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Вывести список");
                Console.WriteLine("3. Найти элемент");
                Console.WriteLine("4. Добавить ПОСЛЕ");
                Console.WriteLine("5. Удалить элемент из списка");
                Console.WriteLine("6. Вывести список в обратном порядке");
                Console.WriteLine("7. Очистить список");
                Console.WriteLine("8. Выход");
                Console.Write("\n" + "Введите команду: ");

                char ch = char.Parse(Console.ReadLine()); //Тут желательно сделать проверку, или считывать всю строку, и в switch уже отсеивать

                switch (ch)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Введите элемент для добавления");
                        string Name = Convert.ToString(Console.ReadLine());
                        linkedList.Add(Name);
                        break;
                    case '2':
                        Console.Clear();
                        if (linkedList.Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                        }
                        else
                        {
                            int chislo = 1;
                            foreach (var item in linkedList)
                            {
                                Console.WriteLine(Convert.ToString(chislo) + ": " + item);
                                chislo++;
                            }
                        }
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Введите элемент для поиска");
                        string FindName = Convert.ToString(Console.ReadLine());
                        Console.WriteLine(linkedList.Contains(FindName));
                        break;
                    case '4':
                        Console.Clear();
                        Console.Clear();
                        if (linkedList.Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                        }
                        else
                        {
                            Console.WriteLine("Введите элемент после которого хотите добавить");
                            string AddName = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Введите элемент который хотите добавить");
                            string AddName2 = Convert.ToString(Console.ReadLine());
                            if (linkedList.AddNext(AddName, AddName2) == true)
                            {
                                Console.WriteLine("Добавление произошло успешно");
                            }
                            else
                            {
                                Console.WriteLine("Элемент не был добавлен. Проверьте введенный вами данные или обратитесь в тех.поддержку");
                            }
                        }
                        break;
                    case '5':
                        Console.Clear();
                        if (linkedList.Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                        }
                        else
                        {
                            Console.WriteLine("Введите элемент после которого хотите удалить");
                            string removeName = Convert.ToString(Console.ReadLine());
                            if (linkedList.Remove(removeName) == true)
                            {
                                Console.WriteLine("Удаление произошло успешно");
                            }
                            else
                            {
                                Console.WriteLine("Элемент не был удален. Проверьте введенный вами данные или обратитесь в тех.поддержку");
                            }
                        }
                        break;
                    case '6':
                        Console.Clear();
                        int chislo2 = 1;
                        if (linkedList.Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                        }
                        else
                        {
                            foreach (var item in linkedList.BackEnumerator())
                            {
                                Console.WriteLine(Convert.ToString(chislo2) + ": " + item);
                                chislo2++;
                            }
                        }
                        break;
                    case '7':
                        Console.Clear();
                        linkedList.Clear();
                        break;
                    case '8':
                        choose = 6;
                        break;

                }
            }
        }
    }
}
/*           //Проверка кода
            DoublyLinkedList<string> linkedList = new DoublyLinkedList<string>(); 
            // добавление элементов
            linkedList.Add("Bob");
            linkedList.Add("Bill");
            linkedList.Add("Tom");
            linkedList.Add("Kilua");
            linkedList.Add("Ulba");
            linkedList.Add("Maks");

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);//вывод списка
            }
            // удаление
            Console.WriteLine("/");
            foreach (var i in linkedList.BackEnumerator())
            {
                Console.WriteLine(i);
            }
            Console.WriteLine(linkedList.Contains("Maks"));
            Console.WriteLine("//");
            Console.WriteLine(linkedList.ContainsBack("Bill"));
            Console.WriteLine("//");
            Console.WriteLine(linkedList.AddNext("Kilua","Rook"));
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }*/