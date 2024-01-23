from Solve_Type import solve


def triangle_type(a,b,c):
    if a <= 0 or b <= 0 or c <= 0:
        return "Неправильный треугольник: отрицательная сторона"
    elif a + b < c or a + c < b or b + c < a:
        return "Неправильный треугольник"
    elif a == b == c:
        return 'Равносторонний'
    elif a == b or a == c or c == b:
        return 'Равнобедренный'
    else:
        return 'Разносторонний'
