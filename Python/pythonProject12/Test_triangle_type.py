import unittest

from Solve_Type import solve
from main import triangle_type


class TriangleTypeTest(unittest.TestCase):
    def test_example1(self):
        result = triangle_type(3, 4, 5)
        self.assertEqual('Разносторонний', result)  # add assertion here

    def test_example2(self):
        result = triangle_type(4, 4, 5)
        self.assertEqual('Равнобедренный', result)

    def test_example3(self):
        result = triangle_type(4, 5, 4)
        self.assertEqual('Равнобедренный', result)

    def test_example4(self):
        result = triangle_type(4, 4, 4)
        self.assertEqual('Равносторонний', result)

    def test_example5(self):
        result = triangle_type(1, 1, 3)
        self.assertEqual('Неправильный треугольник', result)

    def test_example6(self):
        result = triangle_type(1, 1, -1)
        self.assertEqual('Неправильный треугольник: отрицательная сторона', result)

    def test_example7(self):
        result = triangle_type(-1, 1, 2)
        self.assertEqual('Неправильный треугольник: отрицательная сторона', result)

    def test_example8(self):
        result = triangle_type(1, -1, 2)
        self.assertEqual('Неправильный треугольник: отрицательная сторона', result)


class SolveTest(unittest.TestCase):
    def test_example1(self):
        result = solve(-1, -1, -1)
        self.assertEqual('Решений нет', result)

    def test_example2(self):
        result = solve(1, -11, -152)
        self.assertCountEqual((-8, 19), result)
        # self.assertEqual((19, -8), result)


    def test_example3(self):
        result = solve(0, 12, 3)
        self.assertAlmostEqual(-0.25, result)

    def test_example4(self):
        result = solve(4, 0, 0)
        self.assertEqual(0, result)

    def test_example5(self):
        result = solve(12, 6, 0)
        self.assertCountEqual((0, -0.5), result)

    def test_example6(self):
        result = solve(12, 0, 6)
        self.assertEqual('Решений нет', result)


if __name__ == '__main__':
    unittest.main()
