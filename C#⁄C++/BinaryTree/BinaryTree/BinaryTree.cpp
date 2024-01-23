// BinaryTree.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <ctime>
using namespace std;

class tree_elem
{
    public:
        int m_data;//хранилище
        tree_elem* m_left;//ссылка на левый элемент
        tree_elem* m_right;//ссылка на правый элемент
        tree_elem(int val)
        {
            m_left = nullptr; 
            m_right = nullptr;
            m_data = val;
        }
};
class binary_tree
{
public:
        tree_elem* m_root;// указатель на начало дерева
        int m_size; // число всех элементов 
        void print_tree(tree_elem*);
        void delete_tree(tree_elem*);
        binary_tree(int);
        ~binary_tree();
        void print();
        bool find(int);
        void insert(int);
        void erase(int);
};

binary_tree::binary_tree(int key)//создание дерева с одним элементом
{
    m_root = new tree_elem(key);
    m_size = 1;
}

binary_tree::~binary_tree()
{
    delete_tree(m_root);
}

void binary_tree::delete_tree(tree_elem* curr)// удаление дерева
{
    if (curr)
    {
        delete_tree(curr->m_left);
        delete_tree(curr->m_right);
        delete curr;
    }
}

void binary_tree::print()
{
    print_tree(m_root);
    cout << endl;
}

void binary_tree::print_tree(tree_elem* curr)
{
    if (curr) // Проверка на ненулевой указатель
    {
        print_tree(curr->m_left);
        cout << curr->m_data << " ";
        print_tree(curr->m_right);
    }
}

bool binary_tree::find(int key)//возвращяет true or false
{
    tree_elem* curr = m_root;
    while (curr && curr->m_data != key)
    {
        if (curr->m_data > key)
            curr = curr->m_left;
        else
            curr = curr->m_right;
    }
    return curr != NULL;
}

void binary_tree::insert(int key)
{
    tree_elem* curr = m_root;
    while (curr && curr->m_data != key)
    {
        if (curr->m_data > key && curr->m_left == NULL)
        {
            curr->m_left = new tree_elem(key);
            ++m_size;
            return;
        }
        if (curr->m_data < key && curr->m_right == NULL)
        {
            curr->m_right = new tree_elem(key);
            ++m_size;
            return;
        }
        if (curr->m_data > key)
            curr = curr->m_left;
        else
            curr = curr->m_right;
    }
}

void binary_tree::erase(int key)
{
    tree_elem* curr = m_root;
    tree_elem* parent = NULL;
    while (curr && curr->m_data != key)
    {
        parent = curr;
        if (curr->m_data > key)
        {
            curr = curr->m_left;
        }
        else
        {
            curr = curr->m_right;
        }
    }
    if (!curr)
        return;
    if (curr->m_left == NULL)
    {
        
        if (parent && parent->m_left == curr)
            parent->m_left = curr->m_right;
        if (parent && parent->m_right == curr)
            parent->m_right = curr->m_right;
        --m_size;
        delete curr;
        return;
    }
    if (curr->m_right == NULL)
    {
       
        if (parent && parent->m_left == curr)
            parent->m_left = curr->m_left;
        if (parent && parent->m_right == curr)
            parent->m_right = curr->m_left;
        --m_size;
        delete curr;
        return;
    }
    
    tree_elem* replace = curr->m_right;
    while (replace->m_left)
        replace = replace->m_left;
    int replace_value = replace->m_data;
    erase(replace_value);
    curr->m_data = replace_value;
}
int main()
{
    srand(time(NULL));
    binary_tree tree(20);
    for (int i = 0; i < 100; i++)
    {
        
        tree.insert(rand()%1000);
    }
    if (true == tree.find(100))
    {
        cout << "Есть" << endl;
    }

    tree.print();
}