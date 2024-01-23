import unittest

from main import Str_Space, ABS_count , brackets,Palindrom
from main2 import FizzBuzz
from StringCalculator import EmptyString


class MyTestCase(unittest.TestCase):
    def test_exemple1(self):
        rezult = Str_Space('')
        self.assertEqual("Пример простого текста", rezult)

class MyTestCase2(unittest.TestCase):
    def test_exemple1(self):
        rezult = ABS_count()
        self.assertCountEqual({'t':2,'e':1,"x":1}, rezult)

class MyTestCase3(unittest.TestCase):
    def test_exemple1(self):
        rezult = brackets("((((()))))()))")
        self.assertEqual(False, rezult)

    def test_exemple2(self):
        rezult = brackets("((()))))((")
        self.assertEqual(False, rezult)

    def test_exemple3(self):
        rezult = brackets("()()()(())")
        self.assertEqual(True, rezult)  

class MyTestCase4(unittest.TestCase):
    def test_exemple1(self):
        rezult = Palindrom("А роза упала на лапу Азора")
        self.assertEqual("Palindrome", rezult)

    def test_exemple2(self):
        rezult = Palindrom("Функция проверяет,")
        self.assertEqual("Not Palindrome", rezult)

class MyTestCase5(unittest.TestCase):
    def test_exemple1(self):
        rezult = FizzBuzz(15)
        self.assertEqual("FizzBuzz", rezult)

    def test_exemple2(self):
        rezult = FizzBuzz(3)
        self.assertEqual("Fizz", rezult)

    def test_exemple3(self):
        rezult = FizzBuzz(5)
        self.assertEqual("Buzz", rezult)

    def test_exemple4(self):
        rezult = FizzBuzz(4)
        self.assertEqual('4', rezult)

class MyTestCase6(unittest.TestCase):
    def test_exemple1(self):
        rezult = EmptyString('')
        self.assertEqual(0,rezult)

    def test_exemple2(self):
        rezult = EmptyString('1')
        self.assertEqual(1,rezult)

    def test_exemple3(self):
        rezult = EmptyString('1,2')
        self.assertEqual(3,rezult)

    def test_exemple4(self):
        rezult = EmptyString('12,3')
        self.assertEqual(15,rezult)

    def test_exemple5(self):
        rezult = EmptyString('1,2,3')
        self.assertEqual(6,rezult)

    def test_exemple6(self):
        rezult = EmptyString('//;\n1;2;3')
        self.assertEqual(6,rezult)

    def test_exemple7(self):
        rezult = EmptyString('//;\n1;12;3')
        self.assertEqual(16,rezult)

    def test_exemple8(self):
        rezult = EmptyString('//,\n1,2,3')
        self.assertEqual(6,rezult)

if __name__ == '__main__':
    unittest.main()
