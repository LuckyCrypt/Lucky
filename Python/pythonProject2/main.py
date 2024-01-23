from matplotlib import pyplot as plt
from celluloid import Camera
import numpy as np

# Создание одномерного массива, значения от 0 до 1, количество точек - 1000 (параметр кривой Безье)
tau = np.linspace(0, 1, 1000)
print('Введите х координату:')
x_end = input()
print('Введите y координату:')
y_end = input()
P = np.array([[0, 0],  # Координаты контрольны точек записанные в виде двумерного массива
              [2, 4],
              [x_end, y_end],
              ])
P = P.astype(float)


# Алгоритм Безье
def Bezier(t, P):
    # Базисная матрица Безье (коэффициенты при t^2,t^1,t^0)
    Mb = np.matrix([
        [1, -2, 1],
        [-2, 2, 0],
        [1, 0, 0],
    ])
    # Т матрица (матрица степеней вектора времени)
    xx, yy = np.meshgrid(np.arange(2, -1, -1),t)  # Степени, в которые будет возводится t (На входе - два вектора, на выходе две матрицы-сетки)
    xx = np.matrix(xx)
    yy = np.matrix(yy)
    T_mat = np.power(yy, xx)  # Возведение элемента массива yy в степень элемента массива xx
    B = T_mat * Mb * P  # Получаем координаты точки
    return B  # Возвращаем координаты точки

# Открытие всплывающего окна (GUI)

fig = plt.figure()  # Создание объекта-фигуры без осей
plt.xlim(-2, 6)  # Создание и задание пределов оси Х
plt.ylim(-2, 6)  # Создание и задание пределов оси Y
X, Y = Bezier(tau, P)[:, 0], Bezier(tau, P)[:,
                             1]  # Получаем два вектора координат X и Y из вектор tau - для статичного графика
camera = Camera(fig)

for i in range(100):
    coord = Bezier(i / 100, P)
    plt.plot(X, Y, color='#ff0000')  # Отображение координатной плоскости
    plt.scatter(P[:, 0], P[:, 1], color='#0000FF')  # Отображение контрольных точек
    plt.scatter(coord[0, 0], coord[0, 1], color='#00ff00', marker='o',
                s=100)  # Координаты точки кривой, соответствующие параметру i/100 - центр кружка.
    camera.snap()  # Снимок камеры

animation = camera.animate(interval=20, repeat=True, repeat_delay=0)  # Создание анимации
animation.save('1.mp4')  # Сохранение анимации